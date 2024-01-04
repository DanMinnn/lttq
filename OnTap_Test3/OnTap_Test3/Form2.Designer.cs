
namespace OnTap_Test3
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
            this.rpt = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.bc1 = new OnTap_Test3.bc();
            this.SuspendLayout();
            // 
            // rpt
            // 
            this.rpt.ActiveViewIndex = 0;
            this.rpt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rpt.Cursor = System.Windows.Forms.Cursors.Default;
            this.rpt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpt.Location = new System.Drawing.Point(0, 0);
            this.rpt.Name = "rpt";
            this.rpt.ReportSource = this.bc1;
            this.rpt.Size = new System.Drawing.Size(1082, 649);
            this.rpt.TabIndex = 0;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 649);
            this.Controls.Add(this.rpt);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion
        private bc bc1;
        public CrystalDecisions.Windows.Forms.CrystalReportViewer rpt;
    }
}