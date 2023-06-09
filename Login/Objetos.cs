using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login
{
    public class Objetos
    {
    }

    public class oTipoIdentifiacion{

        public int id { get; set; }
        public string tipoIdentificacion { get; set;}
    
    
    }

    public class oGenero
    {

        public int id { get; set; }
        public string genero { get; set; }


    }

    public class oPersona
    {

        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido1 { get; set; }

        public string apellido2 { get; set; }

        public string identificacion  { get; set; }

        public string tipoIdentificacion { get; set; }

        public DateTime fechaDeNacimiento { get; set; }

        public string estadoCivil { get; set; }

        public string genero { get; set; }


    }
}