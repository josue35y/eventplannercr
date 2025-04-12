using System;

namespace EventPlannerCR_backend.Entidades
{
    public class Deudor : Deuda
    {
        public int idPropietario { get; set; }
        public String NombrePropietario { get; set; }
        public int idDeudor { get; set; }
        public String NombreDeudor { get; set; }
        public Double? Monto { get; set; }
    }
}