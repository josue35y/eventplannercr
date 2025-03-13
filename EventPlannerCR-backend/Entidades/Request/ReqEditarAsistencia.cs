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
        public bool Status { get; set; } = true;
        public int? idCarpool { get; set; } = null;
    }
}
