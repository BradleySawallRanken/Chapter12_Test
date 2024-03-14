using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkScanner
{
    public partial class NetworkScanner : Form
    {
        private Computer[] computers = {
            new Computer("My-Desktop", "127.0.0.1", new int[] {20, 21, 23, 25, 53, 80, 123, 389, 443}),
            new Computer("google-public-dns-a", "8.8.8.8", new int[] {53}),
            new Computer("ranken.edu", "47.44.246.80", new int[] {25, 80, 443})
        };
        public NetworkScanner()
        {
            InitializeComponent();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            string ipAddress = txtInput.Text.Trim();
            Computer computer = ScanIPAddress(ipAddress);
            ShowComputer(computer);
        }
        private Computer ScanIPAddress(string ipAddress)
        {
            foreach (var comp in computers)
            {
                if (comp.IpAddress == ipAddress)
                {
                    return comp;
                }
            }
            return null;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtInput.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void ShowComputer(Computer computer)
        {
            if (computer != null)
            {
                lblOutput.Text = $"Name: {computer.Name}\nIP Address: {computer.IpAddress}\nServices: {string.Join(", ", computer.Services)}";
            }
            else
            {
                lblOutput.Text = "IP Address not found";
            }
        }
    }
    public class Computer
        {
            public string Name { get; }
            public string IpAddress { get; }
            public int[] Services { get; }

            public Computer(string name, string ipAddress, int[] services)
            {
                Name = name;
                IpAddress = ipAddress;
                Services = services;
            }
        }
    }
