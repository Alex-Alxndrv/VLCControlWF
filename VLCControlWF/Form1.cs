using System;
using System.Windows.Forms;
using VLCControlWF.Properties;
//using System;
using System.IO.Ports;
namespace VLCControlWF
{
    public partial class Form1 : Form
    {
        string str;
        public Form1()
        {
            InitializeComponent();
           
        }

        void SendCommand(string cmd)
        {
            HttpSender.Post(
                $"http://{Settings.Default.Ip}:{Settings.Default.Port}/requests/status.xml{cmd}", null);
        }


        private void buttonPlay_Click(object sender, EventArgs e)
        {
            SendCommand(Commands.Play);

        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            SendCommand(Commands.Pause);
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            SendCommand(Commands.Stop);
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            SendCommand(Commands.Next);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true) { 
            str = serialPort1.ReadLine();
            serialPort1.ReadExisting();
            Application.DoEvents();
                if (label1.Visible == true)
                {
                    label1.Visible = false;
                }
                else
                {
                    label1.Visible = true;
                }
            if (str == "1")
            {
                SendCommand(Commands.Play);
                    label1.Text = "PLAY";
            }
            if (str == "0")
            {
                SendCommand(Commands.Stop);
                    label1.Text = "STOP";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                open_button.Text = "Открыть";
                listBox1.Enabled =true;
                timer1.Stop();
                serialPort1.Close();

            }
            else
            {
                open_button.Text = "Закрыть";
                listBox1.Enabled = false;
                serialPort1.NewLine = "\r";
            serialPort1.PortName = listBox1.SelectedItem.ToString();
                
            serialPort1.Open();
            timer1.Start();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();

        
            // Display each port name to the console.
            foreach (string port in ports)
            {
                Console.WriteLine(port);
                listBox1.Items.Add(port);
            }
            listBox1.SelectedIndex = 0;
           
        }
    }
}
