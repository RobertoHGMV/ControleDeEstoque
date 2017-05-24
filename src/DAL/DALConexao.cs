using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// Classe para conexão
    /// </summary>
    public class DALConexao
    {
        private string _stringConexao;
        private SqlConnection _conexao;

        public DALConexao(string dadosConexao)
        {
            _conexao = new SqlConnection();
            _stringConexao = dadosConexao;
            _conexao.ConnectionString = dadosConexao;
        }

        public string StringConexao
        {
            get { return _stringConexao; }
            set { _stringConexao = value; }
        }

        public SqlConnection ObjetoConexao
        {
            get { return _conexao; }
            set { _conexao = value; }
        }

        public void Conectar()
        {
            _conexao.Open();
        }

        public void Desconectar()
        {
            _conexao.Close();
        }
    }
}
