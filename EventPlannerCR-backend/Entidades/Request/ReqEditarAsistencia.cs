using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerCR_backend.Entidades
{
    public class ReqEditarAsistencia : ReqBase
    {
        public int idAsistencia { get; set; }
        
        public int? idCarpool { get; set; }

        public bool? Estado { get; set; }
    }
}
