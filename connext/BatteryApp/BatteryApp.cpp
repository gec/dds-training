
/* Include file for convenience, to hide generated include file details */
#include "COWDataTypesCpp11.h"

//#include "unistd.h"
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

int main(int argc, char *arv[])
{
    dds::domain::DomainParticipant dp( 0 );

    dds::topic::qos::TopicQos topicQos;
    dds::sub::qos::DataReaderQos datareaderQos;
    dds::pub::qos::DataWriterQos datawriterQos;

    topicQos = dp.default_topic_qos()
        << dds::core::policy::Reliability::Reliable();
    datawriterQos = topicQos;

    dds::topic::Topic<cow::Reading> batteryTopic( dp, "Battery", topicQos);
    dds::pub::DataWriter<cow::Reading> pvw(dds::pub::Publisher(dp), batteryTopic, datawriterQos);

    topicQos << dds::core::policy::Durability::TransientLocal();
    datareaderQos = topicQos;

    dds::topic::Topic<cow::SetWatts> swTopic( dp, "SetWatts", topicQos );	
    dds::sub::DataReader<cow::SetWatts> swr(dds::sub::Subscriber(dp), swTopic, datareaderQos);
    dds::sub::cond::ReadCondition swrc(swr, dds::sub::status::DataState(), [&swr](){
        dds::sub::LoanedSamples<cow::SetWatts> samples = swr.take();
        dds::sub::LoanedSamples<cow::SetWatts>::const_iterator reading = samples.begin();

        if (reading->info()->valid()) {
            dds::core::string name = reading->data().name();
            std::cout << "Received SetWatts message for " << name;
            if (name == ID) {
                isSv = reading->data().watts() >= 0.0;
                std::cout << "[battery] new watts: " << watts << std::endl;
            }
        }
    }
    );

    dds::topic::Topic<cow::OpenClose> ocTopic( dp, "OpenClose", topicQos );	
    dds::sub::DataReader<cow::OpenClose> ocr(dds::sub::Subscriber(dp), ocTopic, datareaderQos);
    dds::sub::cond::ReadCondition ocrc(ocr, dds::sub::status::DataState(), [&ocr]() {
        dds::sub::LoanedSamples<cow::OpenClose> samples = ocr.take();
        /* typename */ dds::sub::LoanedSamples<cow::OpenClose>::const_iterator reading = samples.begin();

        if (reading->info()->valid()) {
            dds::core::string name = reading->data().name();
            std::cout << "Received OpenClose message for " << name;
            if (reading->data().name() == ID){
                isOpen = reading->data().isOpen();
                std::cout << "[battery] new isOpen: " << isOpen << std::endl;
            }
        }
    });

    dds::core::cond::WaitSet ws;
    ws += swrc;
    ws += ocrc;

    std::cout << "Application is publishing readings on the Battery topic, for battery \"" ID "\"" << std::endl;
    std::cout << "  and listening to the OpenClose and SetWatts topics to adjust behavior" << std::endl;
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
