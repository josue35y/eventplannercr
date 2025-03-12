using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerCR_backend.Entidades
{
    public class Asistencia
    {
        public int idAsistencia {  get; set; }
        public Usuario Usuario { get; set; }
        public Evento Evento { get; set; }
        public bool Status { get; set; }
        public Carpool Carpool { get; set; }


        //public Asistencia()
        //{
        //    Carpool = null; 
        //}
    }
}
