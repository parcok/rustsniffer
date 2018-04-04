using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Net;

namespace RustSniffer {
    public partial class Whitelist : Form {

        public Whitelist() {
            InitializeComponent();
        }
        
        private void Whitelist_Load(object sender, EventArgs e) {
            reloadList();
        }

        private void removeButton_Click(object sender, EventArgs e) {
            string selectedIP = listBox1.SelectedItem.ToString();
            Global.whitelist.Remove(selectedIP);
            reloadList();
        }

        private void reloadList() {
            listBox1.Items.Clear();
            foreach (string ip in Global.whitelist) {
                listBox1.Items.Add(ip);
            }
        }

        private void addButton_Click(object sender, EventArgs e) {
            IPAddress test;
            if (IPAddress.TryParse(textBox1.Text, out test)) {
                Global.whitelist.Add(textBox1.Text);
                textBox1.Text = "";
                reloadList();
            } else {
                MessageBox.Show("That's not a valid IP address.");
            }
        }
    }
}
