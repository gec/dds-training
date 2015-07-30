#include "generated/cow_DCPS.hpp"
#include "unistd.h"
#include <ctime>
#include "math.h"

#define ID "battery1"

bool isSv = false;
float voltage = 480.0;
float current = 0.0;
float watts = 250000.0;
float vars = 0.0;
float frequency = 60.0;
bool isOpen = true;
float stateOfCharge = 10.0;

class DataHandler {
public:
	DataHandler(){};
	void operator()(dds::sub::DataReader<cow::SetWatts>& dr){
		dds::sub::LoanedSamples<cow::SetWatts> samples = dr.take();
		typename dds::sub::LoanedSamples<cow::SetWatts>::const_iterator reading = samples.begin();

		if( reading->data().name() == ID ){
			isSv = reading->data().watts() >= 0.0;
			std::cout << "[battery] new watts: " << watts << std::endl;
		}
	}

	void operator()(dds::sub::DataReader<cow::OpenClose>& dr){
		dds::sub::LoanedSamples<cow::OpenClose> samples = dr.take();
		typename dds::sub::LoanedSamples<cow::OpenClose>::const_iterator reading = samples.begin();
		
		if( reading->data().name() == ID ){
			isOpen = reading->data().isOpen();
			std::cout << "[battery] new isOpen: " << isOpen << std::endl;
		}
	}
};

int main(int argc, char *arv[])
{
	dds::domain::DomainParticipant dp( 0 );
	
	dds::topic::qos::TopicQos qos = 
		dp.default_topic_qos()
		<< dds::core::policy::Durability::Transient()
		<< dds::core::policy::Reliability::Reliable();

	dds::topic::Topic<cow::Reading> batteryTopic( dp, "Battery", qos );
	dds::pub::DataWriter<cow::Reading> pvw( dp, batteryTopic, batteryTopic.qos() );

	dds::topic::Topic<cow::SetWatts> swTopic( dp, "SetWatts", qos );	
	dds::sub::DataReader<cow::SetWatts> swr( dp, swTopic, swTopic.qos() );
	dds::sub::cond::ReadCondition swrc( swr, dds::sub::status::DataState(), DataHandler() );

	dds::topic::Topic<cow::OpenClose> ocTopic( dp, "OpenClose", qos );	
	dds::sub::DataReader<cow::OpenClose> ocr( dp, ocTopic, ocTopic.qos() );
	dds::sub::cond::ReadCondition ocrc( ocr, dds::sub::status::DataState(), DataHandler() );

	dds::core::cond::WaitSet ws;
	ws += swrc;
	ws += ocrc;

	while( true ) {
		try {
			ws.dispatch( dds::core::Duration(1, 0) );
		} catch (dds::core::TimeoutError e1) {

		}

		//THIS IS AN OVER-SIMPLIFICATION OF BATTERY BEHAVIOR
		// we are assuming SV -> always discharge and SC -> always charge
		if( isOpen ){
			pvw << cow::Reading( ID, 0, 0, 0, 0, 0, std::time(0), isOpen, stateOfCharge );
		}
		else{
			watts = 250000.0 * stateOfCharge / 100;
			current = watts / sqrt(3) / voltage;
			if( isSv ){
				stateOfCharge -= 1.0;
				if (stateOfCharge < 0)
					stateOfCharge = 0.0;
			}
			else{
				watts *= -1.0;
				stateOfCharge += 1.0;
				if (stateOfCharge > 100.0)
					stateOfCharge = 100.0;
			}
			pvw << cow::Reading( ID, voltage, current, watts, vars, frequency, std::time(0), isOpen, stateOfCharge );
		}
    };

    return 0;
}