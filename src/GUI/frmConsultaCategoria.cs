using BLL;
using DAL;
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
    public partial class frmConsultaCategoria : Form
    {
        public int codigo = 0;  //determina se o usuário selecionou alguma categoria para ser alterada na tela de cadastro categoria

        public frmConsultaCategoria()
        {
            InitializeComponent();
        }

        private void btLocalizar_Click(object sender, EventArgs e)
        {
            DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
            BLLCategoria bll = new BLLCategoria(cx);
            dgvDados.DataSource = bll.Localizar(txtValor.Text);
        }

        private void frmConsultaCategoria_Load(object sender, EventArgs e)
        {
            btLocalizar_Click(sender, e);  //carrega a grid ao carregar a tela
            dgvDados.Columns[0].HeaderText = "Código"; //muda o nome das colunas do DataGridView
            dgvDados.Columns[0].Width = 70;            //muda a largura
            dgvDados.Columns[1].HeaderText = "Categoria";
            dgvDados.Columns[1].Width = 646;
        }

        //se clicar duas vezes, armazena o código e fecha o formulário
        private void dgvDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) //se for maior que 0, clicou em uma linha existente
            {
                //converte para inteiro na linha selecionada pelo usuário, da célula 0
                codigo = Convert.ToInt32(dgvDados.Rows[e.RowIndex].Cells[0].Value); //armazena o código
                this.Close();  //tem que deixar a propriedade SelectionMode em FullRowSelect
            }
        }
    }
}
