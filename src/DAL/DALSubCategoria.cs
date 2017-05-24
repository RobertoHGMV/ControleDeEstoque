using DAL;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DALSubCategoria
    {
        private DALConexao conexao;

        public DALSubCategoria(DALConexao cx)
        {
            conexao = cx;
        }

        public void Incluir(ModeloSubCategoria modelo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao.ObjetoConexao;
                cmd.CommandText = "INSERT INTO subcategoria (scat_nome, cat_cod) VALUES (@nome, @catcod); SELECT @@IDENTITY";
                cmd.Parameters.AddWithValue("@nome", modelo.ScatNome);
                cmd.Parameters.AddWithValue("@catcod", modelo.CatCod);
                conexao.Conectar();
                modelo.ScatCod = Convert.ToInt32(cmd.ExecuteScalar());
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

        public void Alterar(ModeloSubCategoria modelo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao.ObjetoConexao;
                cmd.CommandText = "UPDATE subcategoria SET scat_nome = @catnome, cat_cod = @catcod WHERE scat_cod = @scatcod";
                cmd.Parameters.AddWithValue("@catnome", modelo.ScatNome);
                cmd.Parameters.AddWithValue("@catcod", modelo.CatCod);
                cmd.Parameters.AddWithValue("@scatcod", modelo.ScatCod);
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

        public void Excluir(int codigo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao.ObjetoConexao;
                cmd.CommandText = "DELETE FROM subcategoria WHERE scat_cod = @codigo";
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

        public DataTable Localizar(string valor)
        {
            try
            {
                DataTable tabela = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SELECT sc.scat_cod, sc.scat_nome, sc.cat_cod, c.cat_nome FROM subcategoria sc " +
                    "INNER JOIN categoria c ON c.cat_cod = sc.cat_cod WHERE scat_nome LIKE '%" +
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

        public ModeloSubCategoria CarregaModeloSubCategoria(int codigo)
        {
            try
            {
                ModeloSubCategoria modelo = new ModeloSubCategoria();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao.ObjetoConexao;
                cmd.CommandText = "SELECT * FROM categoria WHERE cat_cod = @codigo";
                cmd.Parameters.AddWithValue("@codigo", codigo);
                conexao.Conectar();
                SqlDataReader registro = cmd.ExecuteReader();
                if (registro.HasRows)
                {
                    registro.Read();
                    modelo.ScatCod = Convert.ToInt32(registro["scat_cod"]);
                    modelo.ScatNome = Convert.ToString(registro["scat_nome"]);
                    modelo.CatCod = Convert.ToInt32(registro["cat_cod"]);
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
