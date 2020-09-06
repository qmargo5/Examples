namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AddQ = new System.Windows.Forms.Button();
            this.DelQ = new System.Windows.Forms.Button();
            this.Queue = new System.Windows.Forms.Label();
            this.LableData = new System.Windows.Forms.Label();
            this.DataQ = new System.Windows.Forms.TextBox();
            this.DataFirst = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // AddQ
            // 
            this.AddQ.Location = new System.Drawing.Point(244, 36);
            this.AddQ.Name = "AddQ";
            this.AddQ.Size = new System.Drawing.Size(107, 23);
            this.AddQ.TabIndex = 1;
            this.AddQ.Text = "Добавить";
            this.AddQ.UseVisualStyleBackColor = true;
            this.AddQ.Click += new System.EventHandler(this.AddQ_Click);
            // 
            // DelQ
            // 
            this.DelQ.Location = new System.Drawing.Point(27, 85);
            this.DelQ.Name = "DelQ";
            this.DelQ.Size = new System.Drawing.Size(107, 23);
            this.DelQ.TabIndex = 2;
            this.DelQ.Text = "Взять";
            this.DelQ.UseVisualStyleBackColor = true;
            this.DelQ.Click += new System.EventHandler(this.DelQ_Click);
            // 
            // Queue
            // 
            this.Queue.AutoSize = true;
            this.Queue.Location = new System.Drawing.Point(24, 131);
            this.Queue.Name = "Queue";
            this.Queue.Size = new System.Drawing.Size(70, 17);
            this.Queue.TabIndex = 3;
            this.Queue.Text = "Очередь:";
            // 
            // LableData
            // 
            this.LableData.AutoSize = true;
            this.LableData.Location = new System.Drawing.Point(24, 39);
            this.LableData.Name = "LableData";
            this.LableData.Size = new System.Drawing.Size(73, 17);
            this.LableData.TabIndex = 4;
            this.LableData.Text = "Значение";
            // 
            // DataQ
            // 
            this.DataQ.Location = new System.Drawing.Point(122, 39);
            this.DataQ.Name = "DataQ";
            this.DataQ.Size = new System.Drawing.Size(91, 22);
            this.DataQ.TabIndex = 5;
            // 
            // DataFirst
            // 
            this.DataFirst.Location = new System.Drawing.Point(260, 86);
            this.DataFirst.Name = "DataFirst";
            this.DataFirst.Size = new System.Drawing.Size(91, 22);
            this.DataFirst.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(181, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Значение";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(122, 131);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(252, 22);
            this.textBox1.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 366);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.DataFirst);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DataQ);
            this.Controls.Add(this.LableData);
            this.Controls.Add(this.Queue);
            this.Controls.Add(this.DelQ);
            this.Controls.Add(this.AddQ);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button AddQ;
        private System.Windows.Forms.Button DelQ;
        private System.Windows.Forms.Label Queue;
        private System.Windows.Forms.Label LableData;
        private System.Windows.Forms.TextBox DataQ;
        private System.Windows.Forms.TextBox DataFirst;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
    }
}

