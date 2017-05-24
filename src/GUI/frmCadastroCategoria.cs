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
    public partial class frmCadastroCategoria : GUI.FrmModeloDeFormularioDeCadastro
    {
        public frmCadastroCategoria()
        {
            InitializeComponent();
        }

        public void LimpaTela()
        {
            txtCodigo.Clear();
            txtNome.Clear();
        }

        private void btInserir_Click(object sender, EventArgs e)
        {
            operacao = "inserir";
            alterarBotoes(2);
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            LimpaTela();
            alterarBotoes(1);
        }

        private void btSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                //leitura dos dados
                ModeloCategoria modelo = new ModeloCategoria();
                modelo.CatNome = txtNome.Text;

                //obj para gravar os dados no banco
                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLCategoria bll = new BLLCategoria(cx);
                if (operacao == "inserir")
                {
                    //cadastrar uma categoria
                    bll.Incluir(modelo);
                    MessageBox.Show("Cadastro efetuado Código " + modelo.CatCod.ToString());
                }
                else
                {
                    //alterar uma categoria
                    modelo.CatCod = Convert.ToInt32(txtCodigo.Text);
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

        private void btAlterar_Click(object sender, EventArgs e)
        {
            operacao = "alterar";
            alterarBotoes(2);
        }

        private void btExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult d = MessageBox.Show("Deseja excluir o registro?", "Aviso", MessageBoxButtons.YesNo);
                if(d.ToString() == "Yes")
                {
                    DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                    BLLCategoria bll = new BLLCategoria(cx);
                    bll.Excluir(Convert.ToInt32(txtCodigo.Text));
                    LimpaTela();
                    alterarBotoes(1);
                    MessageBox.Show("Categoria excluída");
                }
            }
            catch
            {
                MessageBox.Show("Impossível excluir o registro. \n O registro está sendo utilizado em outro local.");
                alterarBotoes(3);
            }
        }

        private void btLocalizar_Click(object sender, EventArgs e)
        {
            frmConsultaCategoria f = new frmConsultaCategoria();
            f.ShowDialog();
            if (f.codigo != 0)
            {
                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLCategoria bll = new BLLCategoria(cx);
                ModeloCategoria modelo = bll.CarregaModeloCategoria(f.codigo);
                txtCodigo.Text = modelo.CatCod.ToString();
                txtNome.Text = modelo.CatNome;
                alterarBotoes(3);
            }
            else
            {
                LimpaTela();
                alterarBotoes(1);
            }
            f.Dispose(); //destrói o formulário de consulta para que ele não fique ocupando espaço na memória
        }
    }
}
