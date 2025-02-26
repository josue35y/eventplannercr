using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerCR_backend.Entidades
{
    public class Carpool
    {
        public int idCarpool { get; set; }
        public Evento Evento {  get; set; }
        public int CamposDisponibles { get; set; }
        public int Provincia { get; set; }
        public int Canton {  get; set; }
        public int Distrito { get; set; }
        
    }
}
