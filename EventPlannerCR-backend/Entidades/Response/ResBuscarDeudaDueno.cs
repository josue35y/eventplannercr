using System.Collections.Generic;

namespace EventPlannerCR_backend.Entidades
{
    public class ResBuscarDeudaDueno : ResBase
    {
        public List<Deuda> Deuda { get; set; }
    }
}