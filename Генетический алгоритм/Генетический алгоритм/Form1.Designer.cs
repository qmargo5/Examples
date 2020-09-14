namespace Генетический_алгоритм
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.PopulationNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_sizeTourney = new System.Windows.Forms.TextBox();
            this.button_win = new System.Windows.Forms.Button();
            this.listBox_number = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.listView2 = new System.Windows.Forms.ListView();
            this.listView3 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // PopulationNumber
            // 
            this.PopulationNumber.Location = new System.Drawing.Point(241, 25);
            this.PopulationNumber.Name = "PopulationNumber";
            this.PopulationNumber.Size = new System.Drawing.Size(60, 22);
            this.PopulationNumber.TabIndex = 0;
            this.PopulationNumber.Text = "20";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Количество особей в популяции";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(404, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Размер турнира";
            // 
            // textBox_sizeTourney
            // 
            this.textBox_sizeTourney.Location = new System.Drawing.Point(525, 25);
            this.textBox_sizeTourney.Name = "textBox_sizeTourney";
            this.textBox_sizeTourney.Size = new System.Drawing.Size(60, 22);
            this.textBox_sizeTourney.TabIndex = 3;
            this.textBox_sizeTourney.Text = "2";
            // 
            // button_win
            // 
            this.button_win.Location = new System.Drawing.Point(891, 24);
            this.button_win.Name = "button_win";
            this.button_win.Size = new System.Drawing.Size(134, 23);
            this.button_win.TabIndex = 4;
            this.button_win.Text = "Получить";
            this.button_win.UseVisualStyleBackColor = true;
            this.button_win.Click += new System.EventHandler(this.button_win_Click);
            // 
            // listBox_number
            // 
            this.listBox_number.FormattingEnabled = true;
            this.listBox_number.ItemHeight = 16;
            this.listBox_number.Location = new System.Drawing.Point(15, 109);
            this.listBox_number.Name = "listBox_number";
            this.listBox_number.Size = new System.Drawing.Size(95, 500);
            this.listBox_number.TabIndex = 5;
            this.listBox_number.SelectedIndexChanged += new System.EventHandler(this.listBox_number_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Скрещивание";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(116, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Родители";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(418, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(192, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Родители для скрещивания";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(723, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 17);
            this.label6.TabIndex = 12;
            this.label6.Text = "Потомство";
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(726, 109);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(299, 500);
            this.listView1.TabIndex = 13;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // listView2
            // 
            this.listView2.Location = new System.Drawing.Point(421, 109);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(299, 500);
            this.listView2.TabIndex = 14;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.List;
            // 
            // listView3
            // 
            this.listView3.Location = new System.Drawing.Point(116, 109);
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(299, 500);
            this.listView3.TabIndex = 15;
            this.listView3.UseCompatibleStateImageBehavior = false;
            this.listView3.View = System.Windows.Forms.View.List;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 621);
            this.Controls.Add(this.listView3);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBox_number);
            this.Controls.Add(this.button_win);
            this.Controls.Add(this.textBox_sizeTourney);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PopulationNumber);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox PopulationNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_sizeTourney;
        private System.Windows.Forms.Button button_win;
        private System.Windows.Forms.ListBox listBox_number;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ListView listView3;
    }
}

