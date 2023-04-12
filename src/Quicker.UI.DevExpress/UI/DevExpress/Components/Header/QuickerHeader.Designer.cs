using DevExtra = DevExpress.XtraEditors;
namespace Quicker.UI.DevExpress.Components.Header
{
    partial class QuickerHeader
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuickerHeader));
            this.panelControl1 = new DevExtra.PanelControl();
            this.lblRelogio = new DevExtra.LabelControl();
            this.picEdit = new DevExtra.PictureEdit();
            this.lblDescricao = new DevExtra.LabelControl();
            this.tmrRelogio = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblRelogio);
            this.panelControl1.Controls.Add(this.picEdit);
            this.panelControl1.Controls.Add(this.lblDescricao);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(584, 99);
            this.panelControl1.TabIndex = 0;
            // 
            // lblRelogio
            // 
            this.lblRelogio.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblRelogio.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblRelogio.Appearance.Options.UseFont = true;
            this.lblRelogio.Appearance.Options.UseForeColor = true;
            this.lblRelogio.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblRelogio.Location = new System.Drawing.Point(572, 2);
            this.lblRelogio.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.lblRelogio.Name = "lblRelogio";
            this.lblRelogio.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.lblRelogio.Size = new System.Drawing.Size(10, 25);
            this.lblRelogio.TabIndex = 3;
            // 
            // picEdit
            // 
            this.picEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.picEdit.EditValue = ((object)(resources.GetObject("picEdit.EditValue")));
            this.picEdit.Location = new System.Drawing.Point(2, 2);
            this.picEdit.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.picEdit.Name = "picEdit";
            this.picEdit.Properties.ShowCameraMenuItem = DevExtra.Controls.CameraMenuItemVisibility.Auto;
            this.picEdit.Size = new System.Drawing.Size(105, 95);
            this.picEdit.TabIndex = 2;
            // 
            // lblDescricao
            // 
            this.lblDescricao.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.lblDescricao.Appearance.Options.UseFont = true;
            this.lblDescricao.Location = new System.Drawing.Point(117, 3);
            this.lblDescricao.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.lblDescricao.Size = new System.Drawing.Size(83, 25);
            this.lblDescricao.TabIndex = 1;
            this.lblDescricao.Text = "Cadastros";
            // 
            // tmrRelogio
            // 
            this.tmrRelogio.Enabled = true;
            this.tmrRelogio.Interval = 1000;
            this.tmrRelogio.Tick += new System.EventHandler(this.tmrRelogio_Tick);
            // 
            // QuickerHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "QuickerHeader";
            this.Size = new System.Drawing.Size(584, 99);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExtra.PanelControl panelControl1;
        private DevExtra.LabelControl lblDescricao;
        private DevExtra.PictureEdit picEdit;
        private DevExtra.LabelControl lblRelogio;
        private System.Windows.Forms.Timer tmrRelogio;
    }
}
