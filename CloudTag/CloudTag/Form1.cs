using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CloudTag
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Dictionary<string, int> TagCloud = new Dictionary<string, int>();
        List<string> dictonary = new List<string>();
        public static string LevenshteinDistance(string string1, List<string> dictonary)
        {
            string string2 = "";
            int min_metrica = 999;
            string min_word = "";
            int metrica;
            if (string1.Count() > 5)
                metrica = 3;
            else metrica = 2;
            for (int h = 0; h < dictonary.Count(); h++)
            {
                string2 = dictonary[h];
                int diff;
                int[,] m = new int[string1.Length + 1, string2.Length + 1];

                for (int i = 0; i <= string1.Length; i++) { m[i, 0] = i; }
                for (int j = 0; j <= string2.Length; j++) { m[0, j] = j; }

                for (int i = 1; i <= string1.Length; i++)
                {
                    for (int j = 1; j <= string2.Length; j++)
                    {
                        diff = (string1[i - 1] == string2[j - 1]) ? 0 : 1;

                        m[i, j] = Math.Min(Math.Min(m[i - 1, j] + 1,
                                                 m[i, j - 1] + 1),
                                                 m[i - 1, j - 1] + diff);
                        if (m[string1.Length, string2.Length] > min_metrica)
                            goto Fin;
                    }
                }
                if (m[string1.Length, string2.Length] < min_metrica && (m[string1.Length, string2.Length] <= metrica))
                {
                    min_metrica = m[string1.Length, string2.Length];
                    min_word = dictonary[h];
                }
            Fin:;
            }
            return min_word;
        }

        public static string onlyfirst(string string1, List<string> dictonary)
        {
            string string2 = "";
            int min_metrica = 999;
            string min_word = "";
            int metrica;
            if (string1.Count() > 5)
                metrica = 2;
            else metrica = 1;


            for (int i = 0; i < dictonary.Count(); i++)
            {
                string2 = dictonary[i];
                //string2 = "рекорд";
                if (Math.Abs(string1.Length - string2.Length) >= metrica)
                    goto Finish;
                bool found = true;
                int Leng = Math.Min(string1.Length - metrica, string2.Length);
                for (int k = 0; k < Leng; k++)
                {
                    if (string1[k] != string2[k])
                    {
                        found = false;
                        goto Finish;
                    }
                }

                if (found)
                {
                    min_word = dictonary[i];
                    break;
                }
            Finish:;
            }
            return min_word;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string path = @"C:\Users\User\Desktop\Учеба\6 семестр\ТОИ\CloudTag\alternative_dictionary.txt";
            List<string> dictonary = new List<string>();
            try
            {
                using (StreamReader SR = new StreamReader(path))
                {
                    String dict = SR.ReadToEnd();
                    dictonary = dict.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                }

            }
            catch (IOException ex)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
            }

            OpenFileDialog openTextDictonary = new OpenFileDialog();
            openTextDictonary.Title = "Open file";
            openTextDictonary.Filter = "TXT|*.txt";
            List<string> text = new List<string>();
            String line = "";
            if (openTextDictonary.ShowDialog() == DialogResult.OK)
            {
                string pathText = openTextDictonary.FileName;
                try
                {
                    using (StreamReader SR = new StreamReader(pathText))
                    {
                        line = SR.ReadToEnd();
                        textBox1.Text = line;
                        listBox1.Items.Clear();
                    }

                }
                catch (IOException ex)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(ex.Message);
                }
                char[] separate_symbols = new char[] { ',', '.', ':', '\n', '\r', '(', ')', '!', '?', '"', ' ' };
                string[] temptext = line.Split(separate_symbols, StringSplitOptions.RemoveEmptyEntries);
                TagCloud = new Dictionary<string, int>();
                List<string> Endings = new List<string>()
            {
                "ось", "ась", "ся", "ет", "ют", "ат", "ят", "ит", "ать", "уть", "ут", "еть", "ять", "ить",
                "ый", "ий", "ая", "ое", "ого", "ую", "ой", "ым", "ому", "ва", "ав", "вши", "ув", "ел", "ёл",
                "ла", "ло", "ли", "ей", "ему", "им", "их", "ых", "его", "ые"
                };
                for (int i = 0; i < temptext.Count(); i++)
                {
                    bool flag = true;
                    foreach (string word in Endings)
                    {
                        if (temptext[i].EndsWith(word))
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag && temptext[i].Count() > 3)
                    {
                        text.Add(temptext[i].ToLower());
                    }
                }
                for (int i = 0; i < text.Count(); i++)
                {
                    string newword = LevenshteinDistance(text[i], dictonary);
                    string newword1 = onlyfirst(text[i], dictonary);
                    if (newword != newword1)
                        goto Fin;
                    if (newword != "")
                    {
                        if (!TagCloud.ContainsKey(newword))
                        {
                            TagCloud.Add(newword, 1);
                        }
                        else
                        {
                            TagCloud[newword] += 1;
                        }
                    }
                Fin:;
                }
                List<string> Remove = new List<string>();
                foreach (KeyValuePair<string, int> pair in TagCloud)
                {
                    if (pair.Value == 1)
                        Remove.Add(pair.Key);
                }
                for (int i = 0; i < Remove.Count; i++)
                {
                    TagCloud.Remove(Remove[i]);
                }
                foreach (KeyValuePair<string, int> pair in TagCloud.OrderByDescending(pair => pair.Value))
                {
                    listBox1.Items.Add(string.Format("{0} - {1}", pair.Key, pair.Value));
                }
            }


        }
        private void button1_Click(object sender, EventArgs e)
        {
            TagForm tagForm = new TagForm(TagCloud);
            tagForm.ShowDialog();
        }
     
    }
}
