﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MCLauncher.Model {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ModelResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ModelResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MCLauncher.Model.ModelResource", typeof(ModelResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to old_alpha.
        /// </summary>
        internal static string alpha {
            get {
                return ResourceManager.GetString("alpha", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to old_beta.
        /// </summary>
        internal static string beta {
            get {
                return ResourceManager.GetString("beta", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to release.
        /// </summary>
        internal static string release {
            get {
                return ResourceManager.GetString("release", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to snapshot.
        /// </summary>
        internal static string snapshot {
            get {
                return ResourceManager.GetString("snapshot", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://s3.amazonaws.com/Minecraft.Download/versions/versions.json.
        /// </summary>
        internal static string Versions {
            get {
                return ResourceManager.GetString("Versions", resourceCulture);
            }
        }
    }
}