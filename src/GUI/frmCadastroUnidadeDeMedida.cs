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
    public partial class frmCadastroUnidadeDeMedida : GUI.FrmModeloDeFormularioDeCadastro
    {
        public frmCadastroUnidadeDeMedida()
        {
            InitializeComponent();
        }

        private void btInserir_Click(object sender, EventArgs e)
        {
            operacao = "inserir";
            alterarBotoes(2);
        }

        private void btAlterar_Click(object sender, EventArgs e)
        {
            operacao = "alterar";
            alterarBotoes(2);
        }

        private void LimpaTela()
        {
            txtCod.Clear();
            txtUnidadeMedida.Clear();
        }

        private void btExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult d = MessageBox.Show("Deseja excluir o registro?", "Aviso", MessageBoxButtons.YesNo);
                if (d.ToString() == "Yes")
                {
                    DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                    BLLUnidadeDeMedida bll = new BLLUnidadeDeMedida(cx);
                    bll.Excluir(Convert.ToInt32(txtCod.Text));
                    LimpaTela();
                    alterarBotoes(1);
                    MessageBox.Show("Unidade de medida excluída");
                }
            }
            catch
            {
                MessageBox.Show("Impossível excluir o registro. \n O registro está sendo utilizado em outro local.");
                alterarBotoes(3);
            }
        }

        private void btSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                //leitura dos dados
                ModeloUnidadeDeMedida modelo = new ModeloUnidadeDeMedida();
                modelo.UmedNome = txtUnidadeMedida.Text;

                //obj para gravar os dados no banco
                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLUnidadeDeMedida bll = new BLLUnidadeDeMedida(cx);
                if (operacao == "inserir")
                {
                    //cadastrar uma categoria
                    bll.Incluir(modelo);
                    MessageBox.Show("Cadastro efetuado Código " + modelo.UmedCod.ToString());
                }
                else
                {
                    //alterar uma categoria
                    modelo.UmedCod = Convert.ToInt32(txtCod.Text);
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

        private void btLocalizar_Click(object sender, EventArgs e)
        {
            //frmConsultaCategoria f = new frmConsultaCategoria();
            //f.ShowDialog();
            //if (f.codigo != 0)
            //{
            //    DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
            //    BLLUnidadeDeMedida bll = new BLLUnidadeDeMedida(cx);
            //    ModeloUnidadeDeMedida modelo = bll.ModeloUnidadeDeMedida(f.codigo);
            //    txtCod.Text = modelo.UmedCod.ToString();
            //    txtUnidadeMedida.Text = modelo.UmedNome;
            //    alterarBotoes(3);
            //}
            //else
            //{
            //    LimpaTela();
            //    alterarBotoes(1);
            //}
            //f.Dispose();
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            LimpaTela();
            alterarBotoes(1);
        }

        private void txtUnidadeMedida_Leave(object sender, EventArgs e)
        {
            if(this.operacao == "inserir")
            {
                int r = 0;
                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLUnidadeDeMedida bll = new BLLUnidadeDeMedida(cx);
                r = bll.VerificaUnidadeDeMedida(txtUnidadeMedida.Text);
                if(r > 0)
                {
                    DialogResult d = MessageBox.Show("Já existe um registro com esse valor. Deseja alterar o registro?", "Aviso", MessageBoxButtons.YesNo);
                    if (d.ToString() == "Yes")
                    {
                        this.operacao = "alterar";
                        ModeloUnidadeDeMedida modelo = bll.CarregaModeloUnidadeDeMedida(r);
                        txtCod.Text = modelo.UmedCod.ToString();
                        txtUnidadeMedida.Text = modelo.UmedNome;
                    }
                }
            }
        }
    }
}
