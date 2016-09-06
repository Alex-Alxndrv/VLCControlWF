using System;
using System.Windows.Forms;
using System.IO.Ports;
namespace VLCControlWF
{
    public partial class Form1 : Form
    {
        string str;
        public Form1()
        {
            InitializeComponent();

            string[] ports = SerialPort.GetPortNames();

            // Display each port name to the console.
            foreach (string port in ports)
            {
                Console.WriteLine(port);
                listBox1.Items.Add(port);
            }
            listBox1.SelectedIndex = 0;
        }

        void SendCommand(string cmd)
        {
            HttpSender.Post(
                $"http://{Properties.Settings.Default.Ip}:{Properties.Settings.Default.Port}/requests/status.xml{cmd}");
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
                //timer1.Stop();
                serialPort1.Close();
                serialPort1.DataReceived -= SerialPort1_DataReceived;

            }
            else
            {
                open_button.Text = "Закрыть";
                listBox1.Enabled = false;
                serialPort1.NewLine = "\r";
            serialPort1.PortName = listBox1.SelectedItem.ToString();
                
            serialPort1.Open();
                //timer1.Start();
                serialPort1.DataReceived += SerialPort1_DataReceived;
            }
        }

        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            str=serialPort1.ReadExisting();

            //if (label1.InvokeRequired)
            //{
            //    label1.BeginInvoke((MethodInvoker)delegate () { label1.Visible = !label1.Visible; });
            //}
            //else
            //{
            //    label1.Visible = !label1.Visible;
            //}

            switch (str)
            {
                case "1":
                    SendCommand(Commands.Play);
                    SetLabelValue("PLAY");
                    break;
                case "0":
                    SendCommand(Commands.Stop);
                    SetLabelValue("STOP");
                    break;
            }
        }

        void SetLabelValue(string text)
        {
            if (label1.InvokeRequired)
            {
                label1.BeginInvoke((MethodInvoker)delegate () { label1.Text=text; });
            }
            else
            {
                label1.Text = text;
            }
        }
    }
}
