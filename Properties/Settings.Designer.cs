﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SkyStopwatch.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.7.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("754, 770")]
        public global::System.Drawing.Point TimeViewPoint {
            get {
                return ((global::System.Drawing.Point)(this["TimeViewPoint"]));
            }
            set {
                this["TimeViewPoint"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("140, 30")]
        public global::System.Drawing.Size TimeViewSize {
            get {
                return ((global::System.Drawing.Size)(this["TimeViewSize"]));
            }
            set {
                this["TimeViewSize"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1460, 240")]
        public global::System.Drawing.Point PriceViewPoint {
            get {
                return ((global::System.Drawing.Point)(this["PriceViewPoint"]));
            }
            set {
                this["PriceViewPoint"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("140, 740")]
        public global::System.Drawing.Size PriceViewSize {
            get {
                return ((global::System.Drawing.Size)(this["PriceViewSize"]));
            }
            set {
                this["PriceViewSize"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int BootingArgs {
            get {
                return ((int)(this["BootingArgs"]));
            }
            set {
                this["BootingArgs"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool TopMost {
            get {
                return ((bool)(this["TopMost"]));
            }
            set {
                this["TopMost"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10:30\\r\\n20:30\\r\\n35:00")]
        public string TimeNodeCheckingList {
            get {
                return ((string)(this["TimeNodeCheckingList"]));
            }
            set {
                this["TimeNodeCheckingList"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool LogToFile {
            get {
                return ((bool)(this["LogToFile"]));
            }
            set {
                this["LogToFile"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("crossfire,overwatch2,csgo,devenv")]
        public string ProcessListCSV {
            get {
                return ((string)(this["ProcessListCSV"]));
            }
            set {
                this["ProcessListCSV"] = value;
            }
        }
    }
}
