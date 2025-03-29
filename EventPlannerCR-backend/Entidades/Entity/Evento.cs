using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerCR_backend.Entidades
{
    public class Evento
    {
        public int idEvento {  get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime FechaFinal {  get; set; }
        public string Lugar { get; set; }
        public string Descripcion { get; set; }
        public string Clima { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public int diasFaltantes { get; set; }
    }
}
