using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConsultaReceita;
namespace Exemplos
{
    public partial class FrmConsultaCpf : Form
    {
        ConsultarCpf consultarCpf;
        public FrmConsultaCpf()
        {
            InitializeComponent();
        }

        private void FrmConsultaCpf_Load(object sender, EventArgs e)
        {
            this.consultarCpf = new ConsultarCpf();
            pbxCaptcha.Image = this.consultarCpf.GetCaptcha();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Cpf cpf = this.consultarCpf.Consultar(txtCpf.Text, txtCaptcha.Text);
            txtResult.Clear();
            txtResult.AppendText("Nome: "+cpf.Nome);
            txtResult.AppendText("\r\nNúmero: "+cpf.Numero);
            txtResult.AppendText("\r\nDigito Verificador: "+cpf.DigitoVerificador);
            txtResult.AppendText("\r\nSituação Cadastral: "+cpf.SituacaoCadastral);
            txtCpf.Clear();
            txtCaptcha.Clear();
            pbxCaptcha.Image = this.consultarCpf.GetCaptcha();
            txtCpf.Focus();
        }

        private void btnCarregarCaptcha_Click(object sender, EventArgs e)
        {
            pbxCaptcha.Image = this.consultarCpf.GetCaptcha();
        }
    }
}
