using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ModeloUnidadeDeMedida
    {
        private int umed_cod;
        private string umed_nome;

        public ModeloUnidadeDeMedida()
        {
            this.umed_cod = 0;
            this.umed_nome = "";
        }

        public ModeloUnidadeDeMedida(int cod, string nome)
        {
            this.UmedCod = cod;
            this.UmedNome = nome;
        }

        public int UmedCod
        {
            get { return this.umed_cod; }
            set { this.umed_cod = value; }
        }

        public string UmedNome
        {
            get { return umed_nome; }
            set { umed_nome = value; }
        }
    }
}
