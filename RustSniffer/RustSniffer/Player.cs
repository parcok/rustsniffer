using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Collections;

namespace RustSniffer {
    class Player {

        public string ip { get; set; }
        public string loc { get; set; }
        public string isp { get; set; }
        public bool bootStatus { get; set; }
        public System.Timers.Timer timer = new System.Timers.Timer();
        public DataGridView dgv;
        public Form1 form;

        public Player(string ip, DataGridView dgv, Form1 form) {
            this.ip = ip;
            this.loc = "";
            this.isp = "";
            this.bootStatus = false;
            timer.Elapsed += new ElapsedEventHandler(purgeplayer);
            this.timer.Interval = 120000;
            locateAndISP(ip);
            this.dgv = dgv;
            timer.Start();
            this.form = form;
        }

        public void locateAndISP(string ip) {
            using (var objClient = new System.Net.WebClient()) {
                var strFile = objClient.DownloadString("http://ip-api.com/csv/" + ip);
                string[] splitted = strFile.Split(',');
                this.isp = splitted[10].Substring(1, splitted[10].Length - 2);
                this.loc = splitted[5] + ", " + splitted[2];
            }
        }

        private void purgeplayer(object sender, ElapsedEventArgs e) {
            // Only run if auto purge is enabled by the global settings
            if (Global.autoPurgeEnabled) {
                // Only purge the players that don't have an active boot
                if (!this.bootStatus) {
                    timer.Enabled = false;
                    Global.ipAddresses.Remove(ip);
                    Global.players.Remove(this);
                }
                updateGridview(this.form, dgv);
            }
        }

        private void updateGridview(Form form, DataGridView dataGridView1) {
            if (form.InvokeRequired) {
                // We're on a thread other than the GUI thread
                form.Invoke(new MethodInvoker(() => updateGridview(form, dataGridView1)));
                return;
            }

            // Do what you need to do to the form here
            dataGridView1.Rows.Clear();
            foreach (Player p in Global.players) {
                dataGridView1.Rows.Add(p.ip, p.loc, p.isp, p.bootStatus);
            }
        }

        private string toString() {
            return "IP: " + this.ip + "\tLocation: " + this.loc;
        }
    }
}
