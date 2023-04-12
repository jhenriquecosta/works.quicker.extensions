using DevExpress.XtraWaitForm;

namespace Quicker.Ui.DevExpressEx.Components
{

    partial class QuickerWaitForm
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
            this.prgPanel = new DevExpress.XtraWaitForm.ProgressPanel();
            this.SuspendLayout();
            // 
            // prgPanel
            // 
            this.prgPanel.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.prgPanel.Appearance.Options.UseBackColor = true;
            this.prgPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prgPanel.Location = new System.Drawing.Point(0, 0);
            this.prgPanel.Name = "prgPanel";
            this.prgPanel.Size = new System.Drawing.Size(297, 101);
            this.prgPanel.TabIndex = 0;
            this.prgPanel.Text = "progressPanel1";
            // 
            // QuickerWaitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 101);
            this.Controls.Add(this.prgPanel);
            this.Name = "QuickerWaitForm";
            this.Text = "XTWaitForm";
            this.ResumeLayout(false);

        }

        #endregion

        private ProgressPanel prgPanel;
    }
}
