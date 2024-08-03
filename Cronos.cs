using Cronos.dto;
using Cronos.service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Cronos
{
    public partial class Chronos : Form
    {
        public Chronos()
        {
            InitializeComponent();

            executionTimer.Interval = 5000;
            executionTimer.Enabled = true;

            timeLabel.Text = DateTime.Now.ToString();


        }

        private void executionTimer_Tick(object sender, EventArgs e)
        {
            timeLabel.Text = DateTime.Now.ToString();
            statusLabel.Text = "Executing";


            executionTimer.Enabled = false;


            checkBox1.Checked = !checkBox1.Checked;

            
            //healthCheckTextBox.Text = cronosService.healthChecks();

            List<RichText> richTexts = new List<RichText>();
            richTexts  = cronosService.healthChecks();
            richTextBox1.Clear();
            foreach (RichText richText in richTexts) {
                richTextBox1.SelectionColor = richText.color;
                richTextBox1.AppendText(richText.text);
            }

            


            timerSync();
        }

        private void timerSync() {
            DateTime nextMinute = DateTime.Parse(DateTime.Now.ToString("HH:mm")).AddMinutes(1);
            TimeSpan offsetSeconds = nextMinute - DateTime.Now;
            int newInterval = offsetSeconds.Seconds * 1000 + offsetSeconds.Milliseconds;
            if (newInterval == 0) { newInterval = 60000; };
            

            executionTimer.Interval = newInterval;
            executionTimer.Enabled = true;
            statusLabel.Text = "Awaiting";
        }


    }
}
