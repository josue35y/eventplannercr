using System;

namespace EventPlannerCR_backend.Entidades
{
    public class Deuda
    {
        public int idDeuda {  get; set; }
        public Usuario Usuario { get; set; }
        public string Motivo { get; set; }
        public Decimal Total {  get; set; }
        public DateTimeOffset? FechaCreacion {  get; set; }
        public String estado { get; set; }
    }
}
