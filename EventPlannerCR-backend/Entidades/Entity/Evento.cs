using System;

namespace EventPlannerCR_backend.Entidades
{
    public class Evento
    {
        public int IdEvento { get; set; }
        public String Nombre { get; set; }
        public DateTime? FechaInicio { get; set; }
        
        public DateTime? FechaFin { get; set; }
        public String Lugar { get; set; }
        public String Descripcion { get; set; }
        public String Clima { get; set; }
        public double? Latitud {  get; set; }
        public double? Longitud { get; set; }
        public int? DiasFaltantes { get; set; }
        public int Provincia { get; set; }
        public int Canton { get; set; }
        public int Distrito { get; set; }
        public String Imagen { get; set; }
    }
}
