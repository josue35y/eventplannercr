﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventPlannerCR_AccesoDatos;
using EventPlannerCR_backend.Entidades;

namespace EventPlannerCR_backend.Logica
{
    public class LogFactorias
    {
        public List<Usuario> ListaUsuarios(List<SP_Lista_UsuariosResult> tc) {

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
                Evento evento = new Evento();

                // Asignar propiedades
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
    }
}
