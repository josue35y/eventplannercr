﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EventPlannerCR_AccesoDatos
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="EventPlanner")]
	public partial class ConexionLinqDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public ConexionLinqDataContext() : 
				base(global::EventPlannerCR_AccesoDatos.Properties.Settings.Default.EventPlannerConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public ConexionLinqDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ConexionLinqDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ConexionLinqDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ConexionLinqDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SP_InsertarUsuario")]
		public int SP_InsertarUsuario([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Nombre", DbType="NVarChar(100)")] string nombre, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Apellidos", DbType="NVarChar(100)")] string apellidos, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Telefono", DbType="NVarChar(15)")] string telefono, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Correo", DbType="NVarChar(100)")] string correo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="FechaNacimiento", DbType="Date")] System.Nullable<System.DateTime> fechaNacimiento, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Provincia", DbType="NVarChar(100)")] string provincia, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Admin", DbType="Bit")] System.Nullable<bool> admin, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Password", DbType="NVarChar(255)")] string password, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Vehiculo", DbType="Bit")] System.Nullable<bool> vehiculo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="IDRETURN", DbType="Int")] ref System.Nullable<int> iDRETURN, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="ERRORID", DbType="Int")] ref System.Nullable<int> eRRORID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="ERRORDESCRIPCION", DbType="NVarChar(MAX)")] ref string eRRORDESCRIPCION)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), nombre, apellidos, telefono, correo, fechaNacimiento, provincia, admin, password, vehiculo, iDRETURN, eRRORID, eRRORDESCRIPCION);
			iDRETURN = ((System.Nullable<int>)(result.GetParameterValue(9)));
			eRRORID = ((System.Nullable<int>)(result.GetParameterValue(10)));
			eRRORDESCRIPCION = ((string)(result.GetParameterValue(11)));
			return ((int)(result.ReturnValue));
		}
	}
}
#pragma warning restore 1591
