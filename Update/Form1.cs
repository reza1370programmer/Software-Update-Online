using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.Compression;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Xml.Linq;
using System.Net;
using System.Threading;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Data.SqlClient;

namespace Update
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int _countUpdate;
        int _count = 0;
        public bool CheckInternetConnection()
        {
            try
            {
                bool check = true;
                string[] sitesList = new string[3] { "www.google.com", "www.microsoft.com", "www.yahoo.com" };
                Ping ping = new Ping();
                PingReply reply;
                int count = 0;
                for (int i = 0; i < sitesList.Length; i++)
                {
                    reply = ping.Send(sitesList[i]);
                    if (reply.Status == IPStatus.Success)
                        count += 1;
                }
                if (count > 0)
                    check = true;
                else
                    check = false;
                return check;
            }
            catch (Exception)
            {
                return false;
            }

        }

        private void UpdateApp()
        {
            //خارج کردن از حالت فشرده
            string[] subFileEntries = Directory.GetFiles(Path.GetTempPath() + "\\Dotnetyar");
            foreach (string file in subFileEntries)
                ZipFile.ExtractToDirectory(file, Path.GetTempPath() + "\\Dotnetyar");



            //جدا کردن فایل های نصبی از فایل برنامه
            if (!Directory.Exists(Path.GetTempPath() + "\\Dotnetyar\\Install"))
            {
                Directory.CreateDirectory(Path.GetTempPath() + "\\Dotnetyar\\Install");
            }

            string[] subdirectoryEntries = Directory.GetDirectories(Path.GetTempPath() + "\\Dotnetyar");
            foreach (string subdirectory in subdirectoryEntries)
            {
                if (Directory.Exists(subdirectory + "\\Install"))
                {
                    string[] subFile = Directory.GetFiles(subdirectory + "\\Install");
                    foreach (string file in subFile)
                    {
                        int count = 1;
                        string fileNameOnly = Path.GetFileNameWithoutExtension(file);
                        string extension = Path.GetExtension(file);
                        string newFullPath = Path.GetTempPath() + "\\Dotnetyar\\Install\\" + fileNameOnly + extension;
                        while (File.Exists(newFullPath))
                        {
                            string tempFileName = fileNameOnly + "(" + count++ + ")";
                            newFullPath = Path.GetTempPath() + "\\Dotnetyar\\Install\\" + tempFileName + extension;
                        }

                        File.Move(file, newFullPath);
                    }
                    Directory.Delete(subdirectory + "\\Install");
                }
            }



            //کپی فایل های برنامه کنار نرم افزار
            foreach (string subdirectory in subdirectoryEntries)
            {

                if (!subdirectory.Contains("Install"))
                {
                    string sourceFolder = subdirectory;
                    string updateFolder = Application.StartupPath;
                    string[] originalFiles = Directory.GetFiles(sourceFolder, "*", SearchOption.AllDirectories);
                    Array.ForEach(originalFiles, (originalFileLocation) =>
                    {
                        FileInfo originalFile = new FileInfo(originalFileLocation);
                        FileInfo destFile = new FileInfo(originalFileLocation.Replace(sourceFolder, updateFolder));
                        if (destFile.Exists)
                        {
                            originalFile.CopyTo(destFile.FullName, true);
                        }
                        else
                        {
                            Directory.CreateDirectory(destFile.DirectoryName);
                            originalFile.CopyTo(destFile.FullName, false);
                        }
                    });

                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CheckInternetConnection())
            {
                var versionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(Application.StartupPath + "\\App.exe");
                string currentVer = versionInfo.ProductVersion;

                string xmlUrl = "http://dl.dotnetyar.com/Update/update.xml";
                XDocument xml = XDocument.Load(xmlUrl);
                string newVer = string.Empty;


                foreach (XElement element in xml.Descendants("Version"))
                {
                    newVer = element.Value.ToString();
                }

                if (currentVer.Trim() == newVer.Trim())
                    lblCheck.Text = "نسخه جدید موجود نیست";
                else
                {
                    button2.Enabled = false;
                    lblCheck.Text = "نسخه جدید موجود است،در حال بروز رسانی . . .";
                    lblCurentVer.Text = currentVer;
                    lblNewVer.Text = newVer.Trim();

                    if (!Directory.Exists(Path.GetTempPath() + "\\Dotnetyar"))
                    {
                        Directory.CreateDirectory(Path.GetTempPath() + "\\Dotnetyar");
                    }

                    int cVer = int.Parse(currentVer.Replace(".", ""));
                    int nVer = int.Parse(newVer.Trim().Replace(".", ""));

                    _countUpdate = nVer - cVer;

                    _countUpdate++;

                    lblCount.Text = "0" + "/" + _countUpdate;

                    for (int i = 1; i <= _countUpdate; i++)
                    {

                        WebClient webClient = new WebClient();
                        webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                        if (i == _countUpdate)
                            webClient.DownloadFileAsync(new Uri("http://dl.dotnetyar.com/Update/App/App.zip"), Path.GetTempPath() + "\\Dotnetyar\\App.rar");
                        else
                        {
                            string fileName = nVer + ".zip";
                            webClient.DownloadFileAsync(new Uri("http://dl.dotnetyar.com/Update/Files/" + fileName), Path.GetTempPath() + "\\Dotnetyar\\" + fileName);
                        }

                        nVer--;

                    }
                }
            }
            else
                MessageBox.Show("اتصال به اینرتنت را برقرار کنید");
        }

        private void Completed(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            _count++;
            lblCount.Text = _count + "/" + _countUpdate;
            if (_count == _countUpdate)
            {
                Thread.Sleep(9000);
                UpdateApp();

                //اجرای اسکریپت برای بروزرسانی دیتابیس
                string[] subFiles = Directory.GetFiles(Path.GetTempPath() + "\\Dotnetyar\\Install");
                SqlConnection ConnectionStr = new SqlConnection(@"Data Source =.; Initial Catalog = DBApp ; Integrated Security=True");
                foreach (string file in subFiles)
                {
                    if (Path.GetExtension(file) == ".sql")
                    {

                        string script = string.Empty;
                        using (FileStream s = File.Open(file, FileMode.Open))
                        {
                            using (StreamReader st = new StreamReader(s))
                            {
                                script = st.ReadToEnd();
                            }
                        }

                        Server server = new Server(new ServerConnection(ConnectionStr));
                        server.ConnectionContext.ExecuteNonQuery(script);
                        File.Delete(file);
                    }
                }


                //بررسی موجود بودن فایل برای نصب
                string[] subFiles2 = Directory.GetFiles(Path.GetTempPath() + "\\Dotnetyar\\Install");
                if (subFiles2.Length > 0)
                {

                    button1.Enabled = true;
                    this.Height = 210;
                    lblCheck.Text = "بروزرسانی انجام شد.برای تکمیل بروزرسانی لازم است موارد زیر روی سیستم شما " +
        "نصب گردند.برای نصب هر کدام از ابزارها،آنها را ذخیره و نصب کنید.";
                }
                else
                {
                    //حذف کلیه محتویات پوشه دات نت یار
                    DirectoryInfo di = new DirectoryInfo(Path.GetTempPath() + "\\Dotnetyar");
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        dir.Delete(true);
                    }

                    //اجرای نرم افزار اصلی و خروج از برنامه بروزرسانی
                    System.Diagnostics.Process.Start(Application.StartupPath + "\\App.exe");
                    Application.Exit();
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] subFiles = Directory.GetFiles(Path.GetTempPath() + "\\Dotnetyar\\Install");
            FolderBrowserDialog saveFile = new FolderBrowserDialog();
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                //کپی فایل های نیاز به نصب در محل انتخابی کاربر
                foreach (string file in subFiles)
                {
                    string newFullPath = saveFile.SelectedPath + "\\" + Path.GetFileName(file);
                    File.Move(file, newFullPath);
                }

                //حذف کلیه محتویات پوشه دات نت یار
                DirectoryInfo di = new DirectoryInfo(Path.GetTempPath() + "\\Dotnetyar");
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }

                //اجرای نرم افزار اصلی و خروج از برنامه بروزرسانی
                System.Diagnostics.Process.Start(Application.StartupPath + "\\App.exe");
                Application.Exit();
            }

        }

       
    }
}
 
