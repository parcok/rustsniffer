using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace RustSniffer {
    public partial class APISettings : Form {
        public APISettings() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            IPAddress test;
            int time;
            if (IPAddress.TryParse(textBox1.Text, out test) && textBox2.Text != "" && textBox3.Text != "" && Int32.TryParse(textBox3.Text,out time)) {
                Global.apiServer = textBox1.Text;
                Global.apiKey = textBox2.Text;
                Global.apiTime = Int32.Parse(textBox3.Text);
                Global.apiThreads = (int)numericUpDown1.Value;
                Global.apiSettings = true;
                this.Close();
            } else {
                MessageBox.Show("Please enter all values are valid.");
                Global.apiSettings = false;
            }
        }
    }
}
