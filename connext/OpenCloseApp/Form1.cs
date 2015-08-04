using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCloseApp
{
    public partial class Form1 : Form
    {
        private DDS.DomainParticipant participant;
        private cow.OpenCloseDataWriter openCloseWriter;
        private cow.SetWattsDataWriter setWattsWriter;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonPublishOpenClose_Click(object sender, EventArgs e)
        {
            DDS.InstanceHandle_t instance_handle = DDS.InstanceHandle_t.HANDLE_NIL;
            cow.OpenClose instance = cow.OpenCloseTypeSupport.create_data();
            if (instance == null) {
                shutdown(participant);
                throw new ApplicationException(
                    "SvScTypeSupport.create_data error");
            }
            instance.name = textBoxName.Text;
            instance.isOpen = checkBoxOpen.Checked;

            openCloseWriter.write(instance, ref instance_handle);
            MessageBox.Show("Published Name is " + instance.name + ", open is " + instance.isOpen);
        }

        private void buttonPublishSetWatts_Click(object sender, EventArgs e)
        {
            DDS.InstanceHandle_t instance_handle = DDS.InstanceHandle_t.HANDLE_NIL;
            cow.SetWatts instance = cow.SetWattsTypeSupport.create_data();
            if (instance == null)
            {
                shutdown(participant);
                throw new ApplicationException(
                    "SvScTypeSupport.create_data error");
            }
            instance.name = textBoxName.Text;
            instance.watts = (float)numericUpDownWatts.Value;

            setWattsWriter.write(instance, ref instance_handle);
            MessageBox.Show("Published Name is " + instance.name + ", watts is " + instance.watts);
        }

        static void shutdown(
            DDS.DomainParticipant participant)
        {

            /* Delete all entities */

            if (participant != null)
            {
                participant.delete_contained_entities();
                DDS.DomainParticipantFactory.get_instance().delete_participant(
                    ref participant);
            }

            /* RTI Connext provides finalize_instance() method on
            domain participant factory for people who want to release memory
            used by the participant factory. Uncomment the following block of
            code for clean destruction of the singleton. */
            
            try {
                DDS.DomainParticipantFactory.finalize_instance();
            } catch (DDS.Exception e) {
                Console.WriteLine("finalize_instance error: {0}", e);
                throw e;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            participant = DDS.DomainParticipantFactory.get_instance().create_participant(
                0, DDS.DomainParticipantFactory.PARTICIPANT_QOS_DEFAULT,
                null /* listener */, DDS.StatusMask.STATUS_MASK_NONE);

            if (participant == null)
            {
                shutdown(participant);
                throw new ApplicationException("create_participant error");
            }

            // --- Create publisher --- //

            /* To customize publisher QoS, use 
            the configuration file USER_QOS_PROFILES.xml */
            DDS.Publisher publisher = participant.create_publisher(
                DDS.DomainParticipant.PUBLISHER_QOS_DEFAULT,
                null /* listener */,
                DDS.StatusMask.STATUS_MASK_NONE);
            if (publisher == null)
            {
                shutdown(participant);
                throw new ApplicationException("create_publisher error");
            }
            // --- Create topics --- //

            /* OpenClose */

            /* Register type before creating topic */
            System.String type_name = cow.OpenCloseTypeSupport.get_type_name();
            try
            {
                cow.OpenCloseTypeSupport.register_type(
                    participant, type_name);
            }
            catch (DDS.Exception ex)
            {
                MessageBox.Show("register_type error " + ex.Message);
                shutdown(participant);
                throw ex;
            }

            /* Customize QoS settings */
            DDS.TopicQos topicQos = new DDS.TopicQos();
            participant.get_default_topic_qos(topicQos);
            topicQos.reliability.kind = DDS.ReliabilityQosPolicyKind.RELIABLE_RELIABILITY_QOS;
            topicQos.durability.kind = DDS.DurabilityQosPolicyKind.TRANSIENT_LOCAL_DURABILITY_QOS;
            DDS.Topic openCloseTopic = participant.create_topic(
                "OpenClose", type_name, topicQos,
                null /* listener */, DDS.StatusMask.STATUS_MASK_NONE);
            if (openCloseTopic == null)
            {
                shutdown(participant);
                throw new ApplicationException("create_topic error");
            }

            // --- Create writer --- //

            /* To customize data writer QoS, use 
            the configuration file USER_QOS_PROFILES.xml */
            DDS.DataWriterQos datawriterQos = new DDS.DataWriterQos();
            publisher.get_default_datawriter_qos(datawriterQos);
            publisher.copy_from_topic_qos(datawriterQos, topicQos);
            DDS.DataWriter writer = publisher.create_datawriter(
                openCloseTopic,  datawriterQos,
                null /* listener */, DDS.StatusMask.STATUS_MASK_NONE);
            if (writer == null)
            {
                shutdown(participant);
                throw new ApplicationException("create_datawriter error");
            }
            openCloseWriter = (cow.OpenCloseDataWriter)writer;

            /* SetWatts */

            /* Register type before creating topic */
            type_name = cow.SetWattsTypeSupport.get_type_name();
            try
            {
                cow.SetWattsTypeSupport.register_type(
                    participant, type_name);
            }
            catch (DDS.Exception ex)
            {
                MessageBox.Show("register_type error " + ex.Message);
                shutdown(participant);
                throw ex;
            }

            /* To customize topic QoS, use 
            the configuration file USER_QOS_PROFILES.xml */
            DDS.Topic setWattsTopic = participant.create_topic(
                "SetWatts", type_name, topicQos,
                null /* listener */, DDS.StatusMask.STATUS_MASK_NONE);
            if (setWattsTopic == null)
            {
                shutdown(participant);
                throw new ApplicationException("create_topic error");
            }

            // --- Create writer --- //

            /* To customize data writer QoS, use 
            the configuration file USER_QOS_PROFILES.xml */
            writer = publisher.create_datawriter(
                setWattsTopic, datawriterQos,
                null /* listener */, DDS.StatusMask.STATUS_MASK_NONE);
            if (writer == null)
            {
                shutdown(participant);
                throw new ApplicationException("create_datawriter error");
            }
            setWattsWriter = (cow.SetWattsDataWriter)writer;

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            shutdown(participant);
        }

    }
}
