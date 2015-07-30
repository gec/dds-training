#include "generated/cow_DCPS.hpp"
#include "unistd.h"
#include <ctime>
#include "math.h"

float voltage = 480.0;
float current = 0.0;
float watts = 100000.0;
float vars = 0.0;
float frequency = 60.0;
bool isOpen = true;
float stateOfCharge = -1.0;

class DataHandler {
public:
	DataHandler(){};
	void operator()(dds::sub::DataReader<cow::SetWatts>& dr){
		dds::sub::LoanedSamples<cow::SetWatts> samples = dr.take();
		typename dds::sub::LoanedSamples<cow::SetWatts>::const_iterator reading = samples.begin();

		if( reading->data().name() == "pv1" ){
			watts = reading->data().watts();
			std::cout << "new watts: " << watts << std::endl;
		}
	}

	void operator()(dds::sub::DataReader<cow::OpenClose>& dr){
		dds::sub::LoanedSamples<cow::OpenClose> samples = dr.take();
		typename dds::sub::LoanedSamples<cow::OpenClose>::const_iterator reading = samples.begin();
		
		if( reading->data().name() == "pv1" ){
			isOpen = reading->data().isOpen();
			std::cout << "new isOpen: " << isOpen << std::endl;
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

	dds::topic::Topic<cow::Reading> pvTopic( dp, "PV", qos );
	dds::pub::DataWriter<cow::Reading> pvw( dp, pvTopic, pvTopic.qos() );

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
		current = watts / sqrt(3) / voltage;
		vars = watts * 0.05;

		if (!isOpen) {
			pvw << cow::Reading( "pv1", voltage, current, watts, vars, frequency, std::time(0), isOpen, stateOfCharge );
		} else {
			pvw << cow::Reading( "pv1", 0, 0, 0, 0, 0, std::time(0), isOpen, -1 );
		}
    };

    return 0;
}