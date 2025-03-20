using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerCR_backend.Entidades
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        public DateTime FechaRegistro { get; set; }


        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Provincia { get; set; }
        public int Canton { get; set; }
        public int Distrito { get; set; }
        public bool Admin { get; set; }
        public string Password { get; set; }
        public bool Vehiculo { get; set; }
        public bool Estado { get; set; }
        public string CodigoVerificacion { get; set; }
    }
}
