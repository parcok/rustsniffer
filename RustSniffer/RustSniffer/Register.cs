using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RustSniffer {
    public partial class Register : Form {
        public Register() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (textBox1.Text.Contains("'") || textBox2.Text.Contains("'") || textBox3.Text.Contains("'")) {
                MessageBox.Show("Inputs are sanitized, go away.");
                return;
            }
            string username = textBox1.Text;
            string password = getHashPass(textBox2.Text);
            string confirm = getHashPass(textBox4.Text);
            if (password != confirm) {
                MessageBox.Show("Your passwords do not match.");
                return;
            }
            string key = textBox3.Text;
            string uid = Global.getUniqueID();

            using (var objClient = new System.Net.WebClient()) {
                string loginAddress = "http://arcticms.xyz/Rusty/register.php?key=" + key + "&username=" + username + "&password=" + password + "&uid=" + uid;
                var strFile = objClient.DownloadString(loginAddress);
                if (strFile.Contains("Successfully Registered.")) {
                    MessageBox.Show("Successfully Registered.");
                    this.Close();
                } else if (strFile.Contains("Username taken.")) {
                    MessageBox.Show("That username is in use, try another or ask me for a password reset if you are the owner.");
                } else if (strFile.Contains("Invalid.")) {
                    MessageBox.Show("That product key has been redeemed or is invalid. Contact me for further assistance.");
                }
            }
        }

        static string getHashPass(string cleartextPassword) {
            System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(cleartextPassword), 0, Encoding.UTF8.GetByteCount(cleartextPassword));
            foreach (byte theByte in crypto) {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
