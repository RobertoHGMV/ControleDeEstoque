using BLL;
using DAL;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmCadastroSubCategoria : GUI.FrmModeloDeFormularioDeCadastro
    {
        public frmCadastroSubCategoria()
        {
            InitializeComponent();
        }

        public void LimpaTela()
        {
            txtScatCod.Clear();
            txtScatNome.Clear();
        }

        private void frmCadastroSubCategoria_Load(object sender, EventArgs e)
        {
            alterarBotoes(1);
            DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
            BLLCategoria bll = new BLLCategoria(cx);
            cbCatCod.DataSource = bll.Localizar(string.Empty);
            cbCatCod.DisplayMember = "cat_nome"; //informa qual campo presente no dataSource vai mostrar na tela
            cbCatCod.ValueMember = "cat_cod"; //vai guardar o valor 
        }

        private void btInserir_Click(object sender, EventArgs e)
        {
            alterarBotoes(2);
            operacao = "inserir";
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            alterarBotoes(1);
            LimpaTela();
        }

        private void btSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                ModeloSubCategoria modelo = new ModeloSubCategoria();
                modelo.ScatNome = txtScatNome.Text;
                modelo.CatCod = Convert.ToInt32(cbCatCod.SelectedValue);

                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLSubCategoria bll = new BLLSubCategoria(cx);
                
                if(operacao == "inserir")
                {
                    bll.Incluir(modelo);
                    MessageBox.Show("Cadastro efetuado: Código " + modelo.ScatCod.ToString());
                }
                else
                {
                    modelo.ScatCod = Convert.ToInt32(txtScatCod.Text);
                    bll.Alterar(modelo);
                    MessageBox.Show("Cadastro alterado");
                }
                LimpaTela();
                alterarBotoes(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult d = MessageBox.Show("Deseja excluir o registro?", "Aviso", MessageBoxButtons.YesNo);
                if(d.ToString() == "Yes")
                {
                    DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                    BLLSubCategoria bll = new BLLSubCategoria(cx);
                    bll.Excluir(Convert.ToInt32(txtScatCod.Text));
                    LimpaTela();
                    alterarBotoes(1);
                }
            }
            catch
            {
                MessageBox.Show("Impossível excluir o registro. \n O registro está sendo utilizado em outro local.");
                alterarBotoes(3);
            }
        }

        private void btAlterar_Click(object sender, EventArgs e)
        {
            operacao = "alterar";
            alterarBotoes(2);
        }

        private void btLocalizar_Click(object sender, EventArgs e)
        {
            frmConsultaSubCategoria f = new frmConsultaSubCategoria();
            f.ShowDialog();
            if (f.codigo != 0)
            {
                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLSubCategoria bll = new BLLSubCategoria(cx);
                ModeloSubCategoria modelo = bll.CarregaModeloSubCategoria(f.codigo);
                txtScatCod.Text = modelo.ScatCod.ToString();
                txtScatNome.Text = modelo.ScatNome;
                cbCatCod.SelectedValue = modelo.CatCod;
                alterarBotoes(3);
            }
            else
            {
                LimpaTela();
                alterarBotoes(1);
            }
            f.Dispose();
        }
    }
}
