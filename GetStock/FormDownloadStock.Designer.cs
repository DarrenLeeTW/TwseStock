
namespace GetStock
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnDownloadStock = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(17, 45);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(212, 23);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // btnDownloadStock
            // 
            this.btnDownloadStock.Location = new System.Drawing.Point(17, 87);
            this.btnDownloadStock.Name = "btnDownloadStock";
            this.btnDownloadStock.Size = new System.Drawing.Size(160, 39);
            this.btnDownloadStock.TabIndex = 1;
            this.btnDownloadStock.Text = "Download Stock Data";
            this.btnDownloadStock.UseVisualStyleBackColor = true;
            this.btnDownloadStock.Click += new System.EventHandler(this.btnDownloadStock_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select the date to download stock information";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 157);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDownloadStock);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "Form1";
            this.Text = "Tool.GetStockData";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btnDownloadStock;
        private System.Windows.Forms.Label label1;
    }
}

