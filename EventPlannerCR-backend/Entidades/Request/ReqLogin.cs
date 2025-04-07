using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerCR_backend.Entidades
{
    public class ReqLogin
    {
        public bool Rol { get; set; }
        public String Usuario { get; set; }
        public String Password { get; set; }
    }
}

