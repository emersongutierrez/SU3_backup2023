using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Utilidades;

namespace Utilidad
{
    public class InfoUsuario
    {
        private static readonly InfoUsuario _intancia = new InfoUsuario();

        static InfoUsuario() { }

        private InfoUsuario() { }

        public static InfoUsuario Instancia
        {
            get { return _intancia; }
        }

        public int UsuarioConectadoID
        {
            get
            {
                if (ExitenSesiones() && HttpContext.Current.Session["userId"] != null)
                    return Utiles.ConvertirAInt32(HttpContext.Current.Session["userId"]);
                return 0;
            }
        }

        public string UsuarioConectadoMail
        {
            get
            {
                if (ExitenSesiones() && HttpContext.Current.Session["mailUsuario"] != null)
                    return Utiles.ConvertirAString(HttpContext.Current.Session["mailUsuario"]);
                return string.Empty;
            }
        }

        public int TipoUsuarioID
        {
            get
            {
                if (ExitenSesiones() && HttpContext.Current.Session["tipoUsuario"] != null)
                    return Utiles.ConvertirAInt32(HttpContext.Current.Session["tipoUsuario"]);
                return 0;
            }
        }

        public string NombreUsuario
        {
            get
            {
                if (ExitenSesiones() && HttpContext.Current.Session["nombres"] != null)
                    return HttpContext.Current.Session["nombres"].ToString();
                return string.Empty;
            }
        }

        public string FotoUsuario
        {
            get
            {
                if (ExitenSesiones() && HttpContext.Current.Session["foto"] != null)
                    return HttpContext.Current.Session["foto"].ToString();
                return string.Empty;
            }
        }

        public bool ExitenSesiones()
        {
            return (HttpContext.Current != null && HttpContext.Current.Session != null);
        }
    }
}