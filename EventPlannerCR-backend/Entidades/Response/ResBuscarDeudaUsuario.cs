using System.Collections.Generic;

namespace EventPlannerCR_backend.Entidades
{
    public class ResBuscarDeudaUsuario : ResBase
    {
        public List<Deudor> Deudas { get; set; }
    }
}