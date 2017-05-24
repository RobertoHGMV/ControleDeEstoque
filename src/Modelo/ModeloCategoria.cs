using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    /// <summary>
    /// Classe que representa a tabela de categoria (guarda as informações)
    /// </summary>
    public class ModeloCategoria
    {
        public ModeloCategoria()
        {
            CatCod = 0;
            CatNome = string.Empty;
        }

        public ModeloCategoria(int catcode, string catnome)
        {
            CatCod = catcode;
            CatNome = catnome;
        }

        private int cat_cod;
        private string cat_nome;

        public int CatCod
        {
            get { return cat_cod; }
            set { cat_cod = value; }
        }

        public string CatNome
        {
            get { return cat_nome; }
            set { cat_nome = value; }
        }
    }
}
