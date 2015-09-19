using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace NotifyMe
{
    public partial class Dashboard : Form
    {

        //Global variable declaration
        private string sStartMessage = "Listioning to the folder {0} ...";


        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = this.folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
                txtFolderPath.Text = this.folderBrowserDialog1.SelectedPath;
            else
                txtFolderPath.Text = string.Empty;
        }

        private void btnMoniter_Click(object sender, EventArgs e)
        {
            if (MoniterFolder(txtFolderPath.Text.Trim()))
                txtRealTimeStatus.Text = String.Format(sStartMessage, txtFolderPath.Text.Trim());
        }

        private bool MoniterFolder(string FolderPath)
        {
            if (String.IsNullOrEmpty(FolderPath))
                return false;
            if (!Directory.Exists(FolderPath))
                return false;

            fileSystemWatcher1.Path = FolderPath;
            return true;
        }

        private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            txtRealTimeStatus.AppendText(Environment.NewLine);
            txtRealTimeStatus.AppendText(String.Format("{0} {1}", e.Name, e.FullPath, e.ChangeType));
            txtRealTimeStatus.AppendText(Environment.NewLine);
            string fileContent = null;
            Thread oThread = new Thread(() =>
            {
                fileContent = HelperClass.ProcessCSVFile(e.FullPath);
            });
            oThread.Start();
            oThread.Join();
            txtRealTimeStatus.AppendText(fileContent);
        }

        private void btnGrap_Click(object sender, EventArgs e)
        {
            string sGrapID = txtGrapID.Text.Trim();

        }        
    }
}
