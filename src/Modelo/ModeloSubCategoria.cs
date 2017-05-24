﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ModeloSubCategoria
    {
        public ModeloSubCategoria()
        {
            CatCod = 0;
            ScatCod = 0;
            ScatNome = string.Empty;
        }

        public ModeloSubCategoria(int scatcod, int catcod, string scatnome)
        {
            CatCod = catcod;
            ScatCod = scatcod;
            ScatNome = scatnome;
        }

        private int scat_cod;
        private string scat_nome;
        private int cat_cod;

        public int ScatCod
        {
            get { return scat_cod; }
            set { scat_cod = value; }
        }

        public string ScatNome
        {
            get { return scat_nome; }
            set { scat_nome = value; }
        }

        public int CatCod
        {
            get { return cat_cod; }
            set { cat_cod = value; }
        }
    }
}
