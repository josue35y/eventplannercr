using System.Collections.Generic;
using EventPlannerCR_AccesoDatos;
using EventPlannerCR_backend.Entidades;

namespace EventPlannerCR_backend.Logica
{
    public class LogFactorias
    {
        public List<Usuario> ListaUsuarios(List<SP_Lista_UsuariosResult> tc)
        {
            List<Usuario> ListaUsuarios = new List<Usuario>();

            ListaUsuarios = FactoryListaUsuario(tc);

            return ListaUsuarios;
        }

        #region laFactoria!

        private List<Usuario> FactoryListaUsuario(List<SP_Lista_UsuariosResult> tc)
        {
            List<Usuario> ListaUsuarios = new List<Usuario>();

            foreach (var item in tc)
            {
                // Crear un nuevo objeto para cada elemento
                Usuario usuario = new Usuario();

                // Asignar propiedades
                usuario.Nombre = item.NOMBRE;
                usuario.Apellidos = item.APELLIDOS;
                usuario.Correo = item.CORREO;
                usuario.FechaRegistro = item.FECHAREGISTRO;
                usuario.Telefono = item.TELEFONO;
                usuario.FechaNacimiento = item.FECHANACIMIENTO;
                usuario.Vehiculo = (bool)item.VEHICULO;

                // Añadir a la lista
                ListaUsuarios.Add(usuario);
            }

            return ListaUsuarios;
        }

        #endregion

        public List<Usuario> BuscarUsuario(List<SP_Buscar_UsuarioResult> tc)
        {
            List<Usuario> ListaUsuarios = new List<Usuario>();

            ListaUsuarios = FactoryBuscarUsuario(tc);

            return ListaUsuarios;
        }

        #region laFactoria!

        private List<Usuario> FactoryBuscarUsuario(List<SP_Buscar_UsuarioResult> tc)
        {
            List<Usuario> ListaUsuarios = new List<Usuario>();

            foreach (var item in tc)
            {
                // Crear un nuevo objeto para cada elemento
                Usuario usuario = new Usuario();

                // Asignar propiedades
                usuario.Nombre = item.NOMBRE;
                usuario.Apellidos = item.APELLIDOS;
                usuario.Correo = item.CORREO;
                usuario.FechaRegistro = item.FECHAREGISTRO;
                usuario.Telefono = item.TELEFONO;
                usuario.FechaNacimiento = item.FECHANACIMIENTO;
                usuario.Vehiculo = (bool)item.VEHICULO;

                // Añadir a la lista
                ListaUsuarios.Add(usuario);
            }

            return ListaUsuarios;
        }

        #endregion

        public List<Evento> ListaEventos(List<SP_Lista_EventosResult> tc)
        {
            List<Evento> ListaEventos = new List<Evento>();

            ListaEventos = FactoryListaEventos(tc);

            return ListaEventos;
        }

        #region laFactoria!

        private List<Evento> FactoryListaEventos(List<SP_Lista_EventosResult> tc)
        {
            List<Evento> ListaEventos = new List<Evento>();

            foreach (var item in tc)
            {
                // Crear un nuevo objeto para cada elemento
                Evento evento = new Evento();

                // Asignar propiedades
                evento.Nombre = item.Nombre;
                evento.FechaInicio = item.FechaInicio;
                evento.FechaFin = item.FechaFin;
                evento.Lugar = item.Lugar;
                evento.Descripcion = item.Descripcion;
                evento.Clima = item.Clima;
                evento.Latitud = item.lat;
                evento.Longitud = item.lon;
                evento.Provincia = item.Provincia;
                evento.Canton = item.Canton;
                evento.Distrito = item.Distrito;

                // Añadir a la lista
                ListaEventos.Add(evento);
            }

            return ListaEventos;
        }

        #endregion

        public List<Evento> BuscarEvento(List<SP_Buscar_EventoResult> tc)
        {
            List<Evento> ListaEventos = new List<Evento>();

            ListaEventos = FactoryBuscarEvento(tc);

            return ListaEventos;
        }

        #region laFactoria!

        private List<Evento> FactoryBuscarEvento(List<SP_Buscar_EventoResult> tc)
        {
            List<Evento> ListaEventos = new List<Evento>();

            foreach (var item in tc)
            {
                // Crear un nuevo objeto para cada elemento
                Evento evento = new Evento
                {
                    // Asignar propiedades
                    Nombre = item.Nombre,
                    FechaInicio = item.FechaInicio,
                    FechaFin = item.FechaFin,
                    Lugar = item.Lugar,
                    Descripcion = item.Descripcion,
                    Clima = item.Clima,
                    Latitud = item.lat,
                    Longitud = item.lon,
                    Provincia = item.Provincia,
                    Canton = item.Canton,
                    Distrito = item.Distrito
                };

                // Añadir a la lista
                ListaEventos.Add(evento);
            }

            return ListaEventos;
        }

        #endregion

        public List<Deudor> BuscarDeuda(List<SP_BuscarDeudaResult> tc)
        {
            List<Deudor> ListaDeudas = new List<Deudor>();

            ListaDeudas = FactoryBuscarDeuda(tc);

            return ListaDeudas;
        }

        #region factoria

        private List<Deudor> FactoryBuscarDeuda(List<SP_BuscarDeudaResult> tc)
        {
            List<Deudor> ListaDeudas = new List<Deudor>();

            foreach (SP_BuscarDeudaResult item in tc)
            {
                Deudor deudor = new Deudor
                {
                    idDeuda = item.IdDeuda,
                    motivo = item.Motivo,
                    idPropietario = item.IDDEUDOR,
                    nombrePropietario = item.NOMBREPROPIETARIO,
                    telefonoPropietario = item.TELEFONOPROPIETARIO,
                    idDeudor = item.IDDEUDOR,
                    nombreDeudor = item.NOMBREDEUDOR,
                    telefonoDeudor = item.TELEFONODEUDOR,
                    monto = item.MONTOPERSONAL,
                    FechaCreacion = item.FechaCreacion
                };
                ListaDeudas.Add(deudor);
            }

            return ListaDeudas;
        }

        #endregion

        public List<Deuda> BuscarDeudaDueno(List<SP_BuscarDeudasDueñoResult> tc)
        {
            List<Deuda> ListaDeudas = new List<Deuda>();

            ListaDeudas = FactoryBuscarDeudaDueno(tc);

            return ListaDeudas;
        }

        #region factoria

        private List<Deuda> FactoryBuscarDeudaDueno(List<SP_BuscarDeudasDueñoResult> tc)
        {
            List<Deuda> ListaDeudas = new List<Deuda>();

            foreach (SP_BuscarDeudasDueñoResult item in tc)
            {
                Deuda Deuda = new Deuda
                {
                    idDeuda = item.IdDeuda,
                    Motivo = item.Motivo,
                    Total = (decimal)item.Total,
                    FechaCreacion = item.FechaCreacion,
                    estado = item.EstadoDeuda
                };
                ListaDeudas.Add(Deuda);
            }

            return ListaDeudas;
        }

        #endregion

        public List<Deudor> BuscarDeudaPorUsuario(List<SP_BuscarDeudasPorUsuarioResult> tc)
        {
            List<Deudor> listaDeudas = new List<Deudor>();

            listaDeudas = FactoryBuscarDeudaUsuario(tc);
            
            return listaDeudas;
        }

        #region Factoria

        private List<Deudor> FactoryBuscarDeudaUsuario(List<SP_BuscarDeudasPorUsuarioResult> tc)
        {
            List<Deudor> ListaDeudas = new List<Deudor>();
            
            foreach (SP_BuscarDeudasPorUsuarioResult item in tc)
            {
                Deudor Deudor = new Deudor
                {
                    idDeuda = item.IdDeuda,
                    motivo = item.Motivo,
                    idPropietario = item.IDPROPIETARIO,
                    nombrePropietario = item.NOMBREPROPIETARIO,
                    telefonoPropietario = item.TELEFONOPROPIETARIO,
                    idDeudor = item.IDDEUDOR,
                    nombreDeudor = item.NOMBREDEUDOR,
                    telefonoDeudor = item.TELEFONODEUDOR,
                    monto = item.MONTOPERSONAL,
                    FechaCreacion = item.FechaCreacion
                };
                ListaDeudas.Add(Deudor);
            }

            return ListaDeudas;
        }

        #endregion

        public List<Pagos> BuscarPagos(List<SP_BuscarPagosPendientesResult> tc)
        {
            List<Pagos> ListaPagos = new List<Pagos>();
            
            ListaPagos = FactoryBuscarPagos(tc);

            return ListaPagos;
        }

        #region Factoria
        private List<Pagos> FactoryBuscarPagos(List<SP_BuscarPagosPendientesResult> tc)
        {
            List<Pagos> ListaPagos = new List<Pagos>();
            
            foreach (SP_BuscarPagosPendientesResult item in tc)
            {
                Deuda deuda = new Deuda
                {
                    idDeuda = item.IdDeuda,
                    Motivo = item.Motivo,
                    FechaCreacion = item.FechaCreacion
                };
                Usuario usuario = new Usuario
                {
                    IdUsuario = item.IDPROPIETARIO,
                    Nombre = item.NOMBREPROPIETARIO,
                };
                Pagos pagos = new Pagos
                {
                    idPago = item.IdPago,
                    Monto = item.Monto,
                    Pago = item.EstadoPago,
                    ConfirmacionPago = item.ConfirmacionPago,
                    Deuda = deuda,
                    Usuario = usuario
                };
                ListaPagos.Add(pagos);
            }

            return ListaPagos;
        }
        
        #endregion
    }
}