﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AMD_CrudClientDDDSample.Infrastructure.Shared.Messages {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class AuthMessage {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AuthMessage() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AMD_CrudClientDDDSample.Infrastructure.Shared.Messages.AuthMessage", typeof(AuthMessage).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Erro ao criar o Token!.
        /// </summary>
        public static string AuthError {
            get {
                return ResourceManager.GetString("AuthError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Token criado com sucesso!.
        /// </summary>
        public static string AuthSucess {
            get {
                return ResourceManager.GetString("AuthSucess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Senha não informada!.
        /// </summary>
        public static string PasswordNotInformed {
            get {
                return ResourceManager.GetString("PasswordNotInformed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usuário não informado!.
        /// </summary>
        public static string UserNotInformed {
            get {
                return ResourceManager.GetString("UserNotInformed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usuário ou senha invalidos!.
        /// </summary>
        public static string UserOrPasswordError {
            get {
                return ResourceManager.GetString("UserOrPasswordError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Por favor, validar os dados!.
        /// </summary>
        public static string Validate {
            get {
                return ResourceManager.GetString("Validate", resourceCulture);
            }
        }
    }
}
