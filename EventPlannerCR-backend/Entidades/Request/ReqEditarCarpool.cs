using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerCR_backend.Entidades
{
    public class ReqEditarCarpool : ReqBase
    {
        public int IdUsuario {  get; set; }
        public int IdEvento {  get; set; }
        public int? IdUsuarioOcupante { get; set; }
        public string NotasCarpool { get; set; }
        public int? Provincia { get; set; }
        public int? Canton { get; set; }
        public int? Distrito { get; set; }
        public DateTime? HoraSalida { get; set; }     
        public int? IdCarpool_Nuevo { get; set; }

//    @idUsuario INT,
//    @idEvento INT,
//    @idUsuarioOcupante INT = NULL,
//    @NotasCarpool VARCHAR(1) = NULL,
//    @Provincia INT = NULL,
//    @Canton INT = NULL,
//    @Distrito INT = NULL,
//    @HoraSalida DATETIME = NULL,
//    @idCarpool_Nuevo INT = NULL,
//    @ErrorID INT OUTPUT,
//    @ErrorDescripcion NVARCHAR(MAX) OUTPUT
    }
}
