using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;


namespace War3Fix
{
    public partial class Form1 : Form
    {
        RegistryKey ip;
        RegistryKey res;

        public Form1()
        {
            InitializeComponent();

            ip = Registry.CurrentUser.OpenSubKey("Software\\Blizzard Entertainment\\Warcraft III\\", true);
            if (ip == null) { 
                Registry.CurrentUser.CreateSubKey("Software\\Blizzard Entertainment\\Warcraft III\\"); 
                ip = Registry.CurrentUser.OpenSubKey("Software\\Blizzard Entertainment\\Warcraft III\\", true);}

            res = Registry.CurrentUser.OpenSubKey("Software\\Blizzard Entertainment\\Warcraft III\\Video\\", true);
            if (res == null){
                Registry.CurrentUser.CreateSubKey("Software\\Blizzard Entertainment\\Warcraft III\\Video\\");
                res = Registry.CurrentUser.OpenSubKey("Software\\Blizzard Entertainment\\Warcraft III\\Video\\", true);}

            if (ip.GetValue("InstallPath") != null) { textBox1.Text = ip.GetValue("InstallPath").ToString(); } 
            else
            { 
                textBox1.Text = "Browse ->";
            }
            if (res.GetValue("reswidth") != null) 
            { 
                textBox2.Text = res.GetValue("reswidth").ToString();
                textBox3.Text = res.GetValue("resheight").ToString();
            } 
            else 
            { 
                textBox2.Text = "0";
                textBox3.Text = "0";
                res.CreateSubKey("Software\\Blizzard Entertainment\\Warcraft III\\Video\\");
                res = Registry.CurrentUser.OpenSubKey("Software\\Blizzard Entertainment\\Warcraft III\\Video\\", true);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog browse =
            new System.Windows.Forms.FolderBrowserDialog();
            browse.ShowNewFolderButton = false;
            browse.Description="Select WarcraftIII Folder";
            browse.RootFolder=System.Environment.SpecialFolder .DesktopDirectory;
            browse.SelectedPath = "apps";
            if (browse.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = browse.SelectedPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ip.SetValue("InstallPath", textBox1.Text);
            res.SetValue("reswidth", textBox2.Text);
            res.SetValue("resheight", textBox3.Text);
            button1.Text = "Fix'd";
        }
    }
}
