using System;

namespace EventPlannerCR_backend.Entidades
{
    public class Pagos
    {
        public int idPago { get; set; }
        public Deuda Deuda { get; set; }
        public Usuario Usuario { get; set; }
        public double Monto { get; set; }
        public Boolean Pago { get; set; }
        public Boolean ConfirmacionPago { get; set; }
    }
}