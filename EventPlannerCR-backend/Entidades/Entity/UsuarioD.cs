using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerCR_backend.Entidades
{
    public class UsuarioD
    {
        public int IdUsuario { get; set; }
        public String Nombre {  get; set; }
        public String Apellidos { get; set; }
        public String Telefono { get; set; }
        public bool Telefono_Verificado { get; set; }
        public int Cod_Ver_Tel {  get; set; }
        public String Correo { get; set; }
        public bool Correo_Verificado { get; set; }
        public int Cod_Ver_Cor { get; set; }
        public bool Admin {  get; set; }
        public String Password { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Vehiculo { get; set; }
    }
}
