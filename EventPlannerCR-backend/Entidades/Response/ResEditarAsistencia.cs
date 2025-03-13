using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerCR_backend.Entidades
{
    public class ResEditarAsistencia : ResBase
    {
        public int idAsistencia { get; set; }
        public String NombreCompleto { get; set; }
        public String NombreEvento { get; set; }
        public String DescripcionEvento { get; set; }
        public DateTime FechaEvento { get; set; }
        public String LugarEvento { get; set; }
        public String Trasnporte { get; set; }
        public String Estado { get; set; }
        public DateTime ConfirmacionAsistencia { get; set; }
    }
}
