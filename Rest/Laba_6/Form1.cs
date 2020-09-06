using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;

namespace Laba_6
{
    public partial class Form1 : Form
    {
        static public string Access_token;
        static public string UserID;
        public Form1()
        {
            InitializeComponent();
            webBrowser1.Navigated += BrowserOnNavigated;
        }

        private void Auto_Click(object sender, EventArgs e)
        {
            string appId = "6760378";
            var uriStr = @"https://oauth.vk.com/authorize?client_id=" + appId +
                         @"&scope=offline friends groups&redirect_uri=https://oauth.vk.com/blank.html&display=page&v=5.6&response_type=token";
            webBrowser1.Navigate(new Uri(uriStr));

        }
        private void BrowserOnNavigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (e.Url.AbsoluteUri.Contains(@"access_token"))
            {

                string url = e.Url.Fragment;
                url = url.Trim('#');
                Access_token = HttpUtility.ParseQueryString(url).Get("access_token");
                UserID = HttpUtility.ParseQueryString(url).Get("user_id");
                Form2 f = new Form2();
                f.Show();
                f.token = Access_token;
                this.Hide();

            }
        }
    }
}
