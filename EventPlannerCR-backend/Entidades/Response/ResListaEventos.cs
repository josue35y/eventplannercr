using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerCR_backend.Entidades
{
    public class ResListaEventos : ResBase
    {
        public List<Evento> ListaEventos { get; set; }
    }
}
