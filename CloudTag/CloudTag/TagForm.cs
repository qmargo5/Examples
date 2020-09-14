using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudTag
{
    public partial class TagForm : Form
    {
        Dictionary<string, int> tagCloud = new Dictionary<string, int>();

        public TagForm(Dictionary<String, int> TagCloud)
        {
            InitializeComponent();
            tagCloud = new Dictionary<string, int>(TagCloud);
            panelGraph.Refresh();

        }
        private void panelGraph_Paint(object sender, PaintEventArgs e)
        {
            int same = 0;
            int count = 1;
            int positionY = 0;
            int positionX = 0;
            int lastvalx = 0;
            int lastvaly = 0;
            foreach (KeyValuePair<string, int> pair in tagCloud.OrderByDescending(pair => pair.Value))
            {
                int size = (pair.Value) * 10;
                if (same == 0)
                    same = pair.Value;
                if (count != 1 && pair.Value == same)
                    positionX += ((lastvalx+4) * pair.Value * 10);
                else
                {
                    count = 1;
                    positionX = 0;
                    positionY += (12*lastvaly);
                    lastvalx = 0;
                }
                if (positionX > panelGraph.Width - ((lastvalx) * pair.Value * 10))
                {
                    count = 1;
                    positionX = 0;
                    positionY += (12 * lastvaly);
                    lastvalx = 0;
                }
                same = pair.Value;
                count++;
                e.Graphics.DrawString(pair.Key, new System.Drawing.Font("Courier", size), new SolidBrush(Color.Black), positionX, positionY);
                lastvalx = pair.Key.Count();
                lastvaly = pair.Value;
               // positionY += (int)(size + 10);
                //listBox1.Items.Add(string.Format("{0} - {1}", pair.Key, pair.Value));
            }

        }
    }
}
