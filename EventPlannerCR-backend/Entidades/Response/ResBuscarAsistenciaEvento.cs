using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerCR_backend.Entidades
{
    public class ResBuscarAsistenciaEvento : ResBase
    {
        public List<ResBuscarAsistenciaEvento_Modelo> ListaAsistenciaEvento { get; set; }


        public class ResBuscarAsistenciaEvento_Modelo
        {
            public int idAsistencia { get; set; }
            public String NombreCompleto {get; set;}
            public String Trasnporte { get; set; }
            public DateTime ConfirmacionAsistencia { get; set; }
            public String Estado { get; set; }

        }
    }
}
