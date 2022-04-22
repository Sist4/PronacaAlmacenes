using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
   public  class Conexion
    {
       public string String_Conexion()
       {
          //return @"data source=.\SQL; initial catalog = PRONACA_TIENDAS; user id = sa; password = Sistemas123";
           return @"data source = 1S1-INTEGRARP3; initial catalog = PRONACA_TIENDAS; User Id=sa;Password=Pronaca2020";
       
       }
    }
}
