using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerCR_backend.Entidades
{
    public enum enumErrores
    {
        
        excepcionBaseDatos = -2,
        excepcionLogica = -1,

        //el error 0 no existe

        requestNulo = 1,
        nombreFaltante = 2,
        apellidoFaltante = 3,
        correoFaltante = 4,
        passwordFaltante = 5,
        correoIncorrecto = 6,
        passwordInvalido = 7,
        idFaltante = 8,
        sessionCerrada = 9,

        camposDisponiblesFaltante = 10,
        provinciaFaltante = 11,
        cantonFaltante = 12,
        distritoFaltante = 13,

        AtributoInvalido = 14,
        datosNoEncontrados = 15,
        errorConversion = 16,

        requestIncompleto = 17,
        noAutorizado = 18,
        NombreInvalido = 19,
        ApellidoInvalido = 20,
        CorreoInvalido = 21,
        FechaNacimientoFaltante = 22,
        excepcionListaUsuarios = 23,
        excepcionEliminarUsuario = 24,
    }
}
