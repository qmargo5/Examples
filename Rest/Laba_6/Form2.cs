using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;


namespace Laba_6
{
    public partial class Form2 : Form
    {
        public string token;
        public Form2()
        {
            InitializeComponent();
        }
        private string GET(string Url, string Method, string Token, string par)
        {
            WebRequest req = WebRequest.Create(String.Format(Url, Method, Token,par));
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string Out = sr.ReadToEnd();
            return Out;
        }
        public class ResponseGroup
        {
            [JsonProperty("response")]
            public Group groups { get; set; }
        }
        public class Items
        {
            [JsonProperty("id")]
            public int ID { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("screen_name")]
            public string Screen_name { get; set; }
            [JsonProperty("is_closed")]
            public int Is_closed { get; set; }
            [JsonProperty("type")]
            public string Type { get; set; }
        }
        public class Group
        {
            [JsonProperty("count")]
            public int Count { get; set; }
            [JsonProperty("items")]
            public List<Items> items { get; set; }
            
        }
        ResponseGroup GroupArray = new ResponseGroup();
        private void showGroup_Click(object sender, EventArgs e)
        {
            listDroup.Items.Clear();
            string reqStrTemplate = "https://api.vk.com/method/{0}?access_token={1}&{2}v=5.92";
            string method = "groups.get";
            string par = "extended=1&";
            var f = GET(reqStrTemplate, method, token,par);
            var test = JsonConvert.DeserializeObject(f) as JObject;
            GroupArray = JsonConvert.DeserializeObject<ResponseGroup>(f);
            for (int i = 0; i < GroupArray.groups.items.Count(); i++)
            {
                string info = "";
                info = GroupArray.groups.items[i].ID + "\t" + GroupArray.groups.items[i].Name; /* + "\t\t" +
                    GroupArray.groups.items[i].Type;*/
                listDroup.Items.Add(info);
            }
        }

        private void addGroup_Click(object sender, EventArgs e)
        {
            string reqStrTemplate = "https://api.vk.com/method/{0}?access_token={1}&{2}v=5.92";
            string method = "fave.addGroup";
            int c = GroupArray.groups.items[listDroup.SelectedIndex].ID;
            string par = "group_id="+c+"&";
            var f = GET(reqStrTemplate, method, token,par);
            label1.Text = "Успешно!!!";
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Text = "";
        }
    }
}
