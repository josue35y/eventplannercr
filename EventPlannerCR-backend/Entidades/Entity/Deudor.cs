using System;

namespace EventPlannerCR_backend.Entidades
{
    public class Deudor : Deuda
    {
        public int idDeuda { get; set; }
        public String motivo { get; set; }
        public int idPropietario { get; set; }
        public String nombrePropietario { get; set; }
        public String telefonoPropietario { get; set; }
        public int idDeudor { get; set; }
        public String nombreDeudor { get; set; }
        public String telefonoDeudor { get; set; }
        public Double? monto { get; set; }
    }
}