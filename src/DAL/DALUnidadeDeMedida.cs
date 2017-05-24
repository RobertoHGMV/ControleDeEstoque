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
    public class DALUnidadeDeMedida
    {
        private DALConexao conexao;

        public DALUnidadeDeMedida(DALConexao cx)
        {
            conexao = cx;
        }

        public void Inserir(ModeloUnidadeDeMedida modelo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO undmedida (umed_nome) VALUES (@nome); SELECT @@IDENTITY";
                cmd.Connection = conexao.ObjetoConexao;
                cmd.Parameters.AddWithValue("@nome", modelo.UmedNome);
                conexao.Conectar();
                modelo.UmedCod = Convert.ToInt32(cmd.ExecuteScalar());
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

        public void Alterar(ModeloUnidadeDeMedida modelo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UPDATE undmedida SET umed_nome = @nome WHERE umed_cod = @cod";
                cmd.Connection = conexao.ObjetoConexao;
                cmd.Parameters.AddWithValue("@nome", modelo.UmedNome);
                cmd.Parameters.AddWithValue("@cod", modelo.UmedCod);
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
                cmd.CommandText = "DELETE FROM undmedida WHERE umed_cod = @codigo";
                cmd.Connection = conexao.ObjetoConexao;
                cmd.Parameters.AddWithValue("@codigo", codigo);
                conexao.Desconectar();
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
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM undmedida WHERE umed_nome LIKE '%" + 
                    valor + "%'", conexao.ObjetoConexao);
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

        public int VerificaUnidadeDeMedida(string valor)
        {
            try
            {
                int r = 0; //0 - não existe
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao.ObjetoConexao;
                cmd.CommandText = "SELECT * FROM undmedida WHERE umed_nome = @nome";
                cmd.Parameters.AddWithValue("@nome", valor);
                conexao.Conectar();
                SqlDataReader registro = cmd.ExecuteReader();
                if (registro.HasRows)
                {
                    registro.Read();
                    r = Convert.ToInt32(registro["umed_cod"]);
                }
                return r;
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

        public ModeloUnidadeDeMedida CarregaModeloUnidadeDeMedida(int codigo)
        {
            try
            {
                ModeloUnidadeDeMedida modelo = new ModeloUnidadeDeMedida();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM undmedida WHERE umed_cod = @codigo";
                cmd.Connection = conexao.ObjetoConexao;
                cmd.Parameters.AddWithValue("@codigo", codigo);
                conexao.Conectar();
                SqlDataReader registro = cmd.ExecuteReader();
                if (registro.HasRows)
                {
                    registro.Read();
                    modelo.UmedCod = Convert.ToInt32(registro["umed_cod"]);
                    modelo.UmedNome = registro["umed_nome"].ToString();
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