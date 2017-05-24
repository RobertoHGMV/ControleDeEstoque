using DAL;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// Classe que ficará responsável por interagir com a interface
    /// pega os dados, verifica as regras de  negócio, se estiver ok
    /// chama a classe DAL e executa as alterações no banco
    /// </summary>
    public class BLLCategoria
    {
        private DALConexao conexao;

        public BLLCategoria(DALConexao cx)
        {
            conexao = cx;
        }

        public void Incluir(ModeloCategoria modelo)
        {
            if (modelo.CatNome.Trim().Length == 0)
                throw new Exception("O nome da categoria é obrigatório");
            //modelo.CatNome = modelo.CatNome.ToUpper();

            DALCategoria DALobj = new DALCategoria(conexao);
            DALobj.Incluir(modelo);
        }

        public void Alterar(ModeloCategoria modelo)
        {
            if (modelo.CatCod <= 0)
                throw new Exception("O código da categoria é obrigatório");
            if (modelo.CatNome.Trim().Length == 0)
                throw new Exception("O nome da categoria é obrigatório");

            DALCategoria DALobj = new DALCategoria(conexao);
            DALobj.Alterar(modelo);
        }

        public void Excluir(int codigo)
        {
            DALCategoria DALobj = new DALCategoria(conexao);
            DALobj.Excluir(codigo);
        }

        public DataTable Localizar(string valor)
        {
            DALCategoria DALobj = new DALCategoria(conexao);
            return DALobj.Localizar(valor);
        }

        public ModeloCategoria CarregaModeloCategoria(int codigo)
        {
            DALCategoria DALobj = new DALCategoria(conexao);
            return DALobj.CarregaModeloCategoria(codigo);
        }
    }
}
