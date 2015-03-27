using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Exemplos
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void btnConsultaCpf_Click(object sender, EventArgs e)
        {
            FrmConsultaCpf frmConsultaCpf = new FrmConsultaCpf();
            frmConsultaCpf.Show();
        }

        private void btnConsultaCnpj_Click(object sender, EventArgs e)
        {
            FrmConsultaCnpj consultaCnpj = new FrmConsultaCnpj();
            consultaCnpj.Show();
        }
    }
}
