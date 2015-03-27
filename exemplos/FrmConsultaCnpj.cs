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
    public partial class FrmConsultaCnpj : Form
    {
        ConsultarCnpj consultarCnpj;
        public FrmConsultaCnpj()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Cnpj cnpj = this.consultarCnpj.Consultar(txtCnpj.Text, txtCaptcha.Text);
            txtResult.Clear();
            txtResult.AppendText("NÚMERO DE INSCRIÇÃO: "+cnpj.NumeroDeInscricao);
            txtResult.AppendText("\r\nDATA DE ABERTURA: " + cnpj.DataDeAbertura);
            txtResult.AppendText("\r\nNOME EMPRESARIAL: " + cnpj.NomeEmpresarial);
            txtResult.AppendText("\r\nTÍTULO DO ESTABELECIMENTO (NOME DE FANTASIA): " + cnpj.NomeFantasia);
            txtResult.AppendText("\r\nCÓDIGO E DESCRIÇÃO DA ATIVIDADE ECONÔMICA PRINCIPAL: " + cnpj.AtividadeEconomicaPrincipal);
            txtResult.AppendText("\r\nCÓDIGO E DESCRIÇÃO DAS ATIVIDADES ECONÔMICAS SECUNDÁRIAS: " + cnpj.AtividadeEconomicaSecundaria);
            txtResult.AppendText("\r\nCÓDIGO E DESCRIÇÃO DA NATUREZA JURÍDICA: " + cnpj.CodigoDescricaoDaNaturezaJuridica);
            txtResult.AppendText("\r\nLOGRADOURO: " + cnpj.Logradouro);
            txtResult.AppendText("\r\nNÚMERO: " + cnpj.NumeroEndereco);
            txtResult.AppendText("\r\nCOMPLEMENTO: " + cnpj.Complemento);
            txtResult.AppendText("\r\nCEP: " + cnpj.Cep);
            txtResult.AppendText("\r\nBAIRRO/DISTRITO: " + cnpj.BairroDistrito);
            txtResult.AppendText("\r\nMUNICÍPIO: " + cnpj.Municipio);
            txtResult.AppendText("\r\nUF " + cnpj.UF);
            txtResult.AppendText("\r\nENDEREÇO ELETRÔNICO: " + cnpj.Email);
            txtResult.AppendText("\r\nTELEFONE: " + cnpj.Telefone);
            txtResult.AppendText("\r\nENTE FEDERATIVO RESPONSÁVEL (EFR): " + cnpj.EFR);
            txtResult.AppendText("\r\nSITUAÇÃO CADASTRAL: " + cnpj.SituacaoCadastral);
            txtResult.AppendText("\r\nDATA DA SITUAÇÃO CADASTRAL: " + cnpj.DataDaSituacaoCadastral);
            txtResult.AppendText("\r\nMOTIVO DE SITUAÇÃO CADASTRAL: " + cnpj.MotivoSituacaoCadastral);
            txtResult.AppendText("\r\nSITUAÇÃO ESPECIAL: " + cnpj.SituacaoEspecial);
            txtResult.AppendText("\r\nDATA DA SITUAÇÃO ESPECIAL: " + cnpj.DataDaSituacaoEspecial);
            txtCaptcha.Clear();
            txtCnpj.Clear();
            pbxCaptcha.Image = this.consultarCnpj.GetCaptcha();
        }

        private void btnCarregarCaptcha_Click(object sender, EventArgs e)
        {
            pbxCaptcha.Image = this.consultarCnpj.GetCaptcha();
        }

        private void FrmConsultaCnpj_Load(object sender, EventArgs e)
        {
            this.consultarCnpj = new ConsultarCnpj();
            pbxCaptcha.Image = this.consultarCnpj.GetCaptcha();
        }
    }
}
