using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Survive
{
    partial class ViewForms
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
        private void ColorLabels(object sender, EventArgs e)
        {
            foreach(Label label in labels)
            {
                label.BackColor = Color.Red;
            }
        }
        private void ShowMap(List<GameObject>[,] twoDArray)
        {
            int labelNumber = 0;
            this.ClientSize = new System.Drawing.Size(twoDArray.GetLength(0)*50, twoDArray.GetLength(1)*50);
            for (int y = 0; y < twoDArray.GetLength(0); y++)
            {
                for (int x = 0; x < twoDArray.GetLength(1); x++)
                {
                    Label l = new Label();
                    labels.Add(l);
                    //Node node = twoDArray[y, x];
                    //node.label = l;
                    this.Controls.Add(l);
                    l.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    l.Font = new Font("Arial", 16, FontStyle.Bold);
                    l.Location = new System.Drawing.Point(y * 50, x * 50);
                    l.Size = new System.Drawing.Size(50, 50);
                    l.Tag = labelNumber;
                    l.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    //SetNumberLabelColor(l, node.numberOfMinesAround);
                    //l.Click += new EventHandler((sender, e) => label1_Click(sender, (MouseEventArgs)e));
                    labelNumber++;
                }
            }
            this.Show();
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // view
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 450);
            this.Name = "view";
            this.Text = "view";
            this.Load += new System.EventHandler(this.view_Load);
            this.ResumeLayout(false);

        }
        #endregion
        private List<Label> labels = new List<Label>();
    }
}