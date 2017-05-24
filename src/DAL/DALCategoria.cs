using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// Classe para (interagir com o banco de dados) implementar os métodos para
    /// acessar o banco e realizar operações no mesmo
    /// </summary>
    public class DALCategoria
    {
        private DALConexao conexao;

        public DALCategoria(DALConexao cx)
        {
            conexao = cx;
        }

        public void Incluir(ModeloCategoria modelo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao.ObjetoConexao;
                cmd.CommandText = "INSERT INTO categoria (cat_nome) VALUES (@nome); SELECT @@IDENTITY";
                cmd.Parameters.AddWithValue("@nome", modelo.CatNome);
                conexao.Conectar();
                modelo.CatCod = Convert.ToInt32(cmd.ExecuteScalar()); //executado quando quer obter pouca informação do banco de dados
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Desconectar();
            }
        }

        public void Alterar(ModeloCategoria modelo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao.ObjetoConexao;
                cmd.CommandText = "UPDATE categoria SET cat_nome = @nome WHERE cat_cod = @codigo";
                cmd.Parameters.AddWithValue("@nome", modelo.CatNome);
                cmd.Parameters.AddWithValue("@codigo", modelo.CatCod);
                conexao.Conectar();
                cmd.ExecuteNonQuery(); //quando não deseja obter informação alguma de retorno
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Desconectar();
            }
        }

        public void Excluir(int codigo)
        {

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao.ObjetoConexao;
                cmd.CommandText = "DELETE FROM categoria WHERE cat_cod = @codigo";
                cmd.Parameters.AddWithValue("@codigo", codigo);
                conexao.Conectar();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Desconectar();
            }
        }

        //DataTable - uma tabela em memória
        //o DataAdapter já possui um SqlCommand e um SqlConnection
        public DataTable Localizar(string valor)
        {
            try
            {
                DataTable tabela = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM categoria WHERE cat_nome LIKE '%" +
                    valor + "%'", conexao.StringConexao);
                da.Fill(tabela);
                return tabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Desconectar();
            }
        }

        //está executando o comando e salvando seu retorno dentro de um DataReader
        //DataReader - objeto criado para poder ler e acessar as informãções retornados por um comando
        public ModeloCategoria CarregaModeloCategoria(int codigo)
        {
            try
            {
                ModeloCategoria modelo = new ModeloCategoria();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao.ObjetoConexao;
                cmd.CommandText = "SELECT * FROM categoria WHERE cat_cod = @codigo";
                cmd.Parameters.AddWithValue("@codigo", codigo);
                conexao.Conectar();
                SqlDataReader registro = cmd.ExecuteReader(); //quando deseja obter várias informações, vários registros do banco
                if (registro.HasRows)
                {
                    registro.Read(); //se existir alguma linha, lê a linha
                    modelo.CatCod = Convert.ToInt32(registro["cat_cod"]);
                    modelo.CatNome = Convert.ToString(registro["cat_nome"]);
                }
                return modelo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Desconectar();
            }
        }
    }
}
