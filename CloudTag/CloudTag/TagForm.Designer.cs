﻿namespace CloudTag
{
    partial class TagForm
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
            this.panelGraph = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelGraph
            // 
            this.panelGraph.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelGraph.Location = new System.Drawing.Point(13, 13);
            this.panelGraph.Margin = new System.Windows.Forms.Padding(4);
            this.panelGraph.Name = "panelGraph";
            this.panelGraph.Size = new System.Drawing.Size(1508, 611);
            this.panelGraph.TabIndex = 3;
            this.panelGraph.Paint += new System.Windows.Forms.PaintEventHandler(this.panelGraph_Paint);
            // 
            // TagForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1534, 637);
            this.Controls.Add(this.panelGraph);
            this.Name = "TagForm";
            this.Text = "TagForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelGraph;
    }
}