namespace Exemplos
{
    partial class FrmPrincipal
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
            this.btnConsultaCpf = new System.Windows.Forms.Button();
            this.btnConsultaCnpj = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConsultaCpf
            // 
            this.btnConsultaCpf.Location = new System.Drawing.Point(42, 27);
            this.btnConsultaCpf.Name = "btnConsultaCpf";
            this.btnConsultaCpf.Size = new System.Drawing.Size(144, 53);
            this.btnConsultaCpf.TabIndex = 0;
            this.btnConsultaCpf.Text = "Consulta Cpf";
            this.btnConsultaCpf.UseVisualStyleBackColor = true;
            this.btnConsultaCpf.Click += new System.EventHandler(this.btnConsultaCpf_Click);
            // 
            // btnConsultaCnpj
            // 
            this.btnConsultaCnpj.Location = new System.Drawing.Point(238, 27);
            this.btnConsultaCnpj.Name = "btnConsultaCnpj";
            this.btnConsultaCnpj.Size = new System.Drawing.Size(144, 53);
            this.btnConsultaCnpj.TabIndex = 1;
            this.btnConsultaCnpj.Text = "Consulta Cnpj";
            this.btnConsultaCnpj.UseVisualStyleBackColor = true;
            this.btnConsultaCnpj.Click += new System.EventHandler(this.btnConsultaCnpj_Click);
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 105);
            this.Controls.Add(this.btnConsultaCnpj);
            this.Controls.Add(this.btnConsultaCpf);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta Receita";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConsultaCpf;
        private System.Windows.Forms.Button btnConsultaCnpj;
    }
}