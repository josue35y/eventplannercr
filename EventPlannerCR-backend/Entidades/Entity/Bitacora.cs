using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerCR_backend.Entidades
{
    public class Bitacora
    {
        public int idBitacora {  get; set; }
        public string Clase { get; set; }
        public string Metodo { get; set; }
        public string Tipo { get; set; }
        public string Error_ID { get; set; }
        public string Descripcion { get; set; }
        public string Request {  get; set; }
        public string Response { get; set; }
        public DateTime Fecha_Registro { get; set; }
    }
}
