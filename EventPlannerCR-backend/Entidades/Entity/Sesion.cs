using EventPlannerCR_backend.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerCR_backend.Entidades
{
    public class Sesion
    {
        public int Id { get; set; }
        public string CodigoSesion {  get; set; }
        public Usuario Usuario { get; set; }
        public string Origen { get; set; }
        public DateTime Fecha_Inicio { get; set; }
        public DateTime Fecha_Final {  get; set; }
        public enumEstadoSesion Estado { get; set; }
        public DateTime Fecha_Actualizacion { get; set; }        
    }
}
