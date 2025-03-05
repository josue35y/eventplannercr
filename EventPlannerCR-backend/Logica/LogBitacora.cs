using EventPlannerCR_AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerCR_backend.Logica
{
    public class LogBitacora
    {
        public static void RegistrarBitacora(string clase, string metodo, string tipo, string errorID, string descripcion, string request, string response)
        {
            int? idBD = 0;
            int? idError = 0;
            string errorDescripcion = string.Empty;

            try
            {
                using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                {
                    linq.SP_InsertarBitacora(
                        clase,
                        metodo,
                        tipo,
                        errorID,
                        descripcion,
                        request,
                        response,
                        ref idBD,
                        ref idError,
                        ref errorDescripcion
                    );
                }

                if (idError != 0)
                {
                    Console.WriteLine($"Error al registrar en bitácora: {errorDescripcion}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción al registrar bitácora: {ex.Message}");
            }
        }

    }
}
