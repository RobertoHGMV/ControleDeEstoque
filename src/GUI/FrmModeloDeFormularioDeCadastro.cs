using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class FrmModeloDeFormularioDeCadastro : Form
    {
        public string operacao;
        public FrmModeloDeFormularioDeCadastro()
        {
            InitializeComponent();
        }

        public void alterarBotoes(int op)
        {
            //op = operação que serão feitas com os botões
            //1 = preparar os botões para inserir e localizar
            //2 = preparar os botões para inserir/alterar um registro
            //3 = preparar a tela para excluir ou alterar

            pnDados.Enabled = false;
            btInserir.Enabled = false;
            btAlterar.Enabled = false;
            btLocalizar.Enabled = false;
            btExcluir.Enabled = false;
            btCancelar.Enabled = false;
            btSalvar.Enabled = false;

            if (op == 1)
            {
                btInserir.Enabled = true;
                btLocalizar.Enabled = true;
            }
            if (op == 2)
            {
                pnDados.Enabled = true;
                btSalvar.Enabled = true;
                btCancelar.Enabled = true;
            }
            if (op == 3)
            {
                btAlterar.Enabled = true;
                btExcluir.Enabled = true;
                btCancelar.Enabled = true;
            }
        }

        private void FrmModeloDeFormularioDeCadastro_Load(object sender, EventArgs e)
        {
            alterarBotoes(1);
        }

        private void FrmModeloDeFormularioDeCadastro_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
        }
    }
}
