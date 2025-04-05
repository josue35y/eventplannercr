using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerCR_backend.Entidades
{
    public class ResBuscarUsuario : ResBase
    {
        public List<UsuarioD> ListaUsuarios { get; set; }
    }
}
