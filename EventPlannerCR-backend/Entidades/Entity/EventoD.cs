using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerCR_backend.Entidades
{
    public class EventoD
    {
        public int IdEvento { get; set; }
        public String Nombre { get; set; }
        public DateTime Fecha_Evento { get; set; }
        public String Lugar_Evento { get; set; }
        public String Descripcion { get; set; }
        public String Clima { get; set; }
        public String Latitud {  get; set; }
        public String Longitud { get; set; }
        public int Provincia { get; set; }
        public int Canton { get; set; }
        public int Distrito { get; set; }
        public String Imagen { get; set; } // Bits de la imagen
    }
}
