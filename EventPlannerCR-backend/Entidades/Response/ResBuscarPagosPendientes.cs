using System.Collections.Generic;

namespace EventPlannerCR_backend.Entidades
{
    public class ResBuscarPagosPendientes : ResBase
    {
        public List<Pagos> PagosPendientes { get; set; }
    }
}