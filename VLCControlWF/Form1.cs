using System;
using System.Windows.Forms;
using VLCControlWF.Properties;

namespace VLCControlWF
{
    public partial class Form1 : Form
    {
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
    }
}
