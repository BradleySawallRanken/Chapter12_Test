using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chapter12_Test
{
    public partial class PasswordCracker : Form
    {
        private string[,] passwordData = {
            { "123456", "e10adc3949ba59abbe56e057f20f883e" },
            { "123456789", "25f9e794323b453885f5181f1b624d0b" },
            { "qwerty", "d8578edf8458ce06fbc5bb76a58c5ca4" },
            { "111111", "96e79218965eb72c92a549dd5a330112" },
            { "password", "5f4dcc3b5aa765d61d8327deb882cf99" },
            { "qwertyuiop", "6eea9b7ef19179a06954edd0f6c05ceb" },
            { "123321", "c8837b23ff8aaa8a2dde915473ce0991" },
            { "google", "c822c1b63853ed273b89687ac505f9fa" },
            { "P@ssw0rd", "161ebd7d45089b3446ee4e0d86dbcf92" },
            { "Tr0ub4dor&3", "4ece57a61323b52ccffdbef021956754" }
        };
        public PasswordCracker()
        {
            InitializeComponent();
        }
        private string CalculateMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();

                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
        private string CrackPassword(string hash)
        {
            for (int i = 0; i < passwordData.GetLength(0); i++)
            {
                if (passwordData[i, 1] == hash)
                {
                    return passwordData[i, 0];
                }
            }

            return "Fail";
        }
        private void btnCrack_Click(object sender, EventArgs e)
        {
            string hashToCrack = txtInput.Text.Trim();
            string crackedPassword = CrackPassword(hashToCrack);
            lblOutput.Text = crackedPassword;
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
    }
}
