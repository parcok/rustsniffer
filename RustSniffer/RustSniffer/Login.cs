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
    public partial class Login : Form {
        public Login() {
            InitializeComponent();
        }

        string uID = "";

        private void Login_Load(object sender, EventArgs e) {
            uID = Global.getUniqueID();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (textBox1.Text != "" && textBox2.Text != "") {
                if (textBox1.Text.Contains("'") || textBox2.Text.Contains("'")) {
                    MessageBox.Show("Inputs are sanitized, go away.");
                    return;
                }
                string username = textBox1.Text;
                string password = getHashPass(textBox2.Text);

                //Check against webserver logic, need to code the PHP page first.
                using (var objClient = new System.Net.WebClient()) {
                    string loginAddress = "http://arcticms.xyz/Rusty/authentication.php?username=" + username + "&password=" + password + "&uid=" + uID;
                    var strFile = objClient.DownloadString(loginAddress);
                    if (strFile.Contains("Authenticated.")) {
                        Global.authenticated = true;
                        MessageBox.Show("Successfully logged in.");
                        Form Form1 = new Form1();
                        this.Hide();
                        Form1.Show();
                    } else if (strFile.Contains("Expired.")) {
                        MessageBox.Show("Your subscription has expired, contact me to purchase again.");
                    } else if (strFile.Contains("Wrong UID.")) {
                        MessageBox.Show("Hardware incorrect, please contact me if you are the original owner.");
                    } else if (strFile.Contains("Invalid.")) {
                        MessageBox.Show("Those are not valid credentials.");
                    }
                }
            } else {
                MessageBox.Show("Please fill in both fields.");
            }
        }

        // Register form
        private void label3_MouseDoubleClick(object sender, MouseEventArgs e) {
            Form Register = new Register();
            Register.Show();
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
