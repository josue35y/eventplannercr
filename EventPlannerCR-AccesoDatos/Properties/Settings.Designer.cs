﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EventPlannerCR_AccesoDatos.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.12.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=eventplanner.database.windows.net;Initial Catalog=EventPlanner;Persis" +
            "t Security Info=True;User ID=master;Password=B6vcaiNQTAMhu;Encrypt=True;TrustSer" +
            "verCertificate=True")]
        public string EventPlannerConnectionString {
            get {
                return ((string)(this["EventPlannerConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=eventplanner.database.windows.net;Initial Catalog=EventPlanner;Persis" +
            "t Security Info=True;User ID=master;Password=B6vcaiNQTAMhu;Encrypt=True")]
        public string EventPlannerConnectionString1 {
            get {
                return ((string)(this["EventPlannerConnectionString1"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=eventplanner.database.windows.net;Initial Catalog=EventPlanner;Persis" +
            "t Security Info=True;User ID=master;Encrypt=True;TrustServerCertificate=True")]
        public string EventPlannerConnectionString2 {
            get {
                return ((string)(this["EventPlannerConnectionString2"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=eventplanner.database.windows.net;Initial Catalog=EventPlanner;Persis" +
            "t Security Info=True;User ID=master;Password=B6vcaiNQTAMhu;Encrypt=True;TrustSer" +
            "verCertificate=True")]
        public string EventPlannerConnectionString3 {
            get {
                return ((string)(this["EventPlannerConnectionString3"]));
            }
        }
    }
}
