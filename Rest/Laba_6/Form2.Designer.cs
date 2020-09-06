namespace Laba_6
{
    partial class Form2
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
            this.showGroup = new System.Windows.Forms.Button();
            this.addGroup = new System.Windows.Forms.Button();
            this.listDroup = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // showGroup
            // 
            this.showGroup.Location = new System.Drawing.Point(13, 497);
            this.showGroup.Name = "showGroup";
            this.showGroup.Size = new System.Drawing.Size(134, 66);
            this.showGroup.TabIndex = 0;
            this.showGroup.Text = "Показать группы";
            this.showGroup.UseVisualStyleBackColor = true;
            this.showGroup.Click += new System.EventHandler(this.showGroup_Click);
            // 
            // addGroup
            // 
            this.addGroup.Location = new System.Drawing.Point(163, 497);
            this.addGroup.Name = "addGroup";
            this.addGroup.Size = new System.Drawing.Size(134, 66);
            this.addGroup.TabIndex = 1;
            this.addGroup.Text = "Добавить группу в закладки";
            this.addGroup.UseVisualStyleBackColor = true;
            this.addGroup.Click += new System.EventHandler(this.addGroup_Click);
            // 
            // listDroup
            // 
            this.listDroup.FormattingEnabled = true;
            this.listDroup.ItemHeight = 16;
            this.listDroup.Location = new System.Drawing.Point(12, 16);
            this.listDroup.Name = "listDroup";
            this.listDroup.Size = new System.Drawing.Size(469, 468);
            this.listDroup.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(315, 497);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 17);
            this.label1.TabIndex = 3;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 575);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listDroup);
            this.Controls.Add(this.addGroup);
            this.Controls.Add(this.showGroup);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button showGroup;
        private System.Windows.Forms.Button addGroup;
        private System.Windows.Forms.ListBox listDroup;
        private System.Windows.Forms.Label label1;
    }
}