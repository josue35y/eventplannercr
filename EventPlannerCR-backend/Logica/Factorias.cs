using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acceso_de_datos;
using EventPlannerCR_backend.Entidades;

namespace EventPlannerCR_backend.Logica
{
    public class Factorias
    {
        public List<UsuarioD> ListaUsuarios(SP_Lista_UsuariosResult tc) {

            List<UsuarioD> ListaUsuarios = new List<UsuarioD>();

            FactoryListaUsuario(tc);

            return ListaUsuarios;
        }

        #region laFactoria!
        private List<UsuarioD> FactoryListaUsuario(SP_Lista_UsuariosResult tc)
        {
            List<UsuarioD> ListaUsuarios = new List<UsuarioD>();
            UsuarioD usuario = new UsuarioD();

            usuario.Nombre = tc.NOMBRE;
            usuario.Apellidos = tc.APELLIDOS;
            usuario.Correo = tc.CORREO;
            usuario.FechaRegistro = tc.FECHAREGISTRO;
            usuario.Telefono = tc.TELEFONO;
            usuario.FechaNacimiento = tc.FECHANACIMIENTO;
            usuario.Vehiculo = tc.VEHICULO;
            ListaUsuarios.Add(usuario);

            return ListaUsuarios;
        }
        #endregion
    }
}
