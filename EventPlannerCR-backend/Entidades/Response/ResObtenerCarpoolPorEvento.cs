using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerCR_backend.Entidades
{
    public class ResObtenerCarpoolPorEvento : ResBase
    {
        public List<CarpoolPorEvento_Modelo> CarpoolList { get; set; }
        public class CarpoolPorEvento_Modelo
        {
            public int idCarpool { get; set; }

            /// <summary>
            /// Nombre completo del dueño del carpool
            /// sale de la tabla Usuario, con el idUsuario
            /// </summary>
            public String NombreCompletoDueno { get; set; }

            /// <summary>
            /// Despliega "2/5" siendo 5 el numero de campos totales segun la tabla carpool, 
            /// y el 2 es la diferencia de "campos totales" menos  
            /// cantidad de gente esta asociada al carpool segun la tabla asistencia.
            /// </summary>
            public String CamposDisponibles { get; set; }

            /// <summary>
            /// Nombre completo y ID de los usuarios que se unieron al carpool 
            /// puestos en una sola columna que contiene un arreglo "idUsuario , Nombre Apellido"
            /// </summary>
            public List<UsuariosCarpool> CarpoolUsuarios { get; set; }


            /// <summary>
            /// Nombre del evento, sale de la tabla evento. se debe desplegar como (Provincia, Canton, Distrito) 
            /// en una columna llamada "Nombre de evento".
            /// </summary>
            public String NombreEvento { get; set; }


            /// <summary>
            /// Direccion de donde el carpool hace la salida, sale de la tabla carpool. se debe desplegar como (Provincia, Canton, Distrito) 
            /// en una columna llamada "Origen de carpool".
            /// </summary>
            public String DireccionOrigen { get; set; }


            /// <summary>
            /// Direccion del evento (destino del carpool), sale de la tabla evento. se debe desplegar como (Provincia, Canton, Distrito) 
            /// en una columna llamada "Direccion de evento".
            /// </summary>
            public String DireccionDestino { get; set; }

            /// <summary>
            /// Hora en la que el carpool inicia su viaje, es ingresada por el dueno del carpool al crearlo.
            /// <summary>
            public DateTime HoraSalida { get; set; }

            /// <summary>
            /// Comentario adicional del carpool
            /// <summary>
            public String NotasCarpool { get; set; } //anadir a tabla


        }

        public class UsuariosCarpool
        {
            public String idUsuario { get; set; }
            public String NombreApellido { get; set; }

        }

    }
}
