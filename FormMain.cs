using System;
using System.Windows.Forms;

namespace RPxSocket
{
    public partial class FormMain : Form
    {
        
        public FormMain()
        {
            InitializeComponent();
            initUI();
        }

        void initUI()
        {
            this.Load += FormMain_Load;
            this.FormClosing += FormMain_FormClosing;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            txtIP.Text = Properties.Settings.Default["ip"] as string;
            txtZPL.Text = Properties.Settings.Default["zpl"] as string;
            txtData.Text = Properties.Settings.Default["data"] as string;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveData();
        }

        void saveData()
        {
            Properties.Settings.Default["ip"] = txtIP.Text;
            Properties.Settings.Default["zpl"] = txtZPL.Text;
            Properties.Settings.Default["data"] = txtData.Text;
            Properties.Settings.Default.Save();
            MyLib.log("saved Data");
        }


        string getZPL()
        {
            string data = string.Format(txtZPL.Text, txtData.Text);
            MyLib.log(data);
            return data;
        }
        

        private void btnClick(object sender, EventArgs e)
        {
            var btnName = ((Button)sender).Name;
            var ip = txtIP.Text;
            switch (btnName)
            {
                case "btnPing":
                    var db = string.Format("Ping to {0} = {1}", ip, MyLib.PingHost(ip));
                    MyLib.showDlgMessage(db);
                    break;

                case "btnPrint":
                    var zpl = getZPL();
                    MyLib.log(zpl);
                    MyLib.sendData(zpl, ip);
                    break;
                
                case "btnSaveExit":
                    Close();
                    break;

                default:
                    MyLib.log(btnName);
                    break;
            }    
        }


        
    }
}
