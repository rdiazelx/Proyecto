using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login
{
    public class Data
    {
    }



    public class Tiempo
    {
        private double segundos;

        public double Horas
        {
            get { return segundos; }
            set
            {
                if (value < 0 || value > 24)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Rango entre 0 y 24");
                }
                else
                {
                    segundos = value * 3600;
                }
            }
        }
    }


}