using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerCR_backend.Entidades
{
    public class Pagos
    {
        public int idPago {  get; set; }
        public Deuda Deuda { get; set; }
        public Usuario Usuario { get; set; }
        public float Monto { get; set; }
        public bool Confirmacionpago { get; set; }

    }
}
