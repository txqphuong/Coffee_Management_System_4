using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class BackupRestore : Form
    {
        public BackupRestore()
        {
            InitializeComponent();
        }
        //Sao lưu database 
        private void BackupDatabase()
        {
            try
            {
                string cmdCommand = "mongodump --db Quanlycafe --out D:/backup/";

                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.FileName = "cmd.exe";
                processInfo.Arguments = "/c " + cmdCommand;
                processInfo.CreateNoWindow = true;
                processInfo.UseShellExecute = false;
                processInfo.RedirectStandardOutput = true;
                processInfo.RedirectStandardError = true;
                processInfo.RedirectStandardInput = true;

                Process process = new Process();
                process.StartInfo = processInfo;
                process.EnableRaisingEvents = true;

                process.OutputDataReceived += (sender, e) =>
                {
                    if (!String.IsNullOrEmpty(e.Data))
                    {
                        show.Invoke((MethodInvoker)delegate
                        {
                            show.AppendText(e.Data + Environment.NewLine);
                        });
                    }
                };

                process.ErrorDataReceived += (sender, e) =>
                {
                    if (!String.IsNullOrEmpty(e.Data))
                    {
                        show.Invoke((MethodInvoker)delegate
                        {
                            show.AppendText(e.Data + Environment.NewLine);
                        });
                    }
                };

                process.Exited += (sender, e) =>
                {
                    show.Invoke((MethodInvoker)delegate
                    {
                        show.AppendText("Backup process completed!" + Environment.NewLine);
                    });
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        //Phục hồi
        private void RestoreDatabase()
        {
            try
            {
                string cmdCommand = "mongorestore --db Quanlycafe D:/backup/Quanlycafe/";

                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.FileName = "cmd.exe";
                processInfo.Arguments = "/c " + cmdCommand;
                processInfo.CreateNoWindow = true;
                processInfo.UseShellExecute = false;
                processInfo.RedirectStandardOutput = true;
                processInfo.RedirectStandardError = true;
                processInfo.RedirectStandardInput = true;

                Process process = new Process();
                process.StartInfo = processInfo;
                process.EnableRaisingEvents = true;

                process.OutputDataReceived += (sender, e) =>
                {
                    if (!String.IsNullOrEmpty(e.Data))
                    {
                        show1.Invoke((MethodInvoker)delegate
                        {
                            show1.AppendText(e.Data + Environment.NewLine);
                        });
                    }
                };

                process.ErrorDataReceived += (sender, e) =>
                {
                    if (!String.IsNullOrEmpty(e.Data))
                    {
                        show1.Invoke((MethodInvoker)delegate
                        {
                            show1.AppendText(e.Data + Environment.NewLine);
                        });
                    }
                };

                process.Exited += (sender, e) =>
                {
                    show1.Invoke((MethodInvoker)delegate
                    {
                        show1.AppendText("Restore process completed!" + Environment.NewLine);
                    });
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            BackupDatabase();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RestoreDatabase();
        }
    }
}
