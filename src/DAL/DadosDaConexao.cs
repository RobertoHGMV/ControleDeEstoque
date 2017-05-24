using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// Classe que possui a string de conexão
    /// </summary>
    public class DadosDaConexao
    {
        public static string StringDeConexao
        {
            get
            {
                return "Data Source=(local);Initial Catalog=ControleDeEstoque;Integrated Security=True";
            }
        }
    }
}
