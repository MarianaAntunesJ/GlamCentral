﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GlamCentral.Libraries.Language {
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
    public class Mensagem {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Mensagem() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GlamCentral.Libraries.Language.Mensagem", typeof(Mensagem).Assembly);
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
        ///   Looks up a localized string similar to Registro não pode ser salvo..
        /// </summary>
        public static string MSG_E {
            get {
                return ResourceManager.GetString("MSG_E", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} inválido..
        /// </summary>
        public static string MSG_E_Email {
            get {
                return ResourceManager.GetString("MSG_E_Email", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} deve ter no máximo {1} caracteres..
        /// </summary>
        public static string MSG_E_Maxima {
            get {
                return ResourceManager.GetString("MSG_E_Maxima", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} deve ter no mínimo {1} caracteres..
        /// </summary>
        public static string MSG_E_Minimo {
            get {
                return ResourceManager.GetString("MSG_E_Minimo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} é obrigatório!.
        /// </summary>
        public static string MSG_E_Obrigatorio {
            get {
                return ResourceManager.GetString("MSG_E_Obrigatorio", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O valor do campo {0} não se encontra entre {1} e {0}.
        /// </summary>
        public static string MSG_E_Quantidade {
            get {
                return ResourceManager.GetString("MSG_E_Quantidade", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Senhas não coincidem!.
        /// </summary>
        public static string MSG_E_Senhas {
            get {
                return ResourceManager.GetString("MSG_E_Senhas", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Registro salvo com sucesso!.
        /// </summary>
        public static string MSG_S {
            get {
                return ResourceManager.GetString("MSG_S", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Senha enviada ao e-mail do funcionario com sucesso!.
        /// </summary>
        public static string MSG_S_SenhaGerada {
            get {
                return ResourceManager.GetString("MSG_S_SenhaGerada", resourceCulture);
            }
        }
    }
}
