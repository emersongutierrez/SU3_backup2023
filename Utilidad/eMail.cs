using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
using System.Web.Mail;
using System.Web.UI.WebControls;

namespace Utilidad
{
    /// <summary>
    /// Summary description for E_Mail.
    /// </summary>
    public class eMail
    {
        private int smtpauth = 1;

        public eMail()
        { }

        public eMail(string mail_ip_smtp, string mail_port_smtp)
        {
            this._mail_ip_smtp = mail_ip_smtp;
            this._mail_port_smtp = mail_port_smtp;
        }

        public eMail(string mail_origen, string mail_destino, string mail_body, string mail_subject,
            string mail_ip_smtp, string mail_port_smtp)
        {
            this._mail_origen = mail_origen;
            this._mail_destino = mail_destino;
            this._mail_body = mail_body;
            this._mail_subject = mail_subject;
            this._mail_ip_smtp = mail_ip_smtp;
            this._mail_port_smtp = mail_port_smtp;
        }

        #region propiedades

        string _mail_origen;
        string _nombre_origen;            
        string _mail_password;
        string _mail_destino;
        string _mail_copia;
        string _mail_oculto;
        string _mail_body;
        string _mail_subject;
        string _mail_ip_smtp;
        string _mail_port_smtp;

        public string mail_origen
        {
            get
            {
                return _mail_origen.Trim();
            }
            set
            {
                this._mail_origen = value.Trim();
                this._mail_password = ObtenerMailPassword(value);
            }
        }

        public string nombre_origen
        {
            get { return _nombre_origen.Trim(); }
            set {this._nombre_origen = !string.IsNullOrEmpty(value) ? value : _mail_origen; }
        }

        public string mail_password
        {
            get
            {
                return _mail_password.Trim();
            }
            set
            {
                this._mail_password = value.Trim();
            }
        }

        public string mail_destino
        {
            get
            {
                return _mail_destino;
            }
            set
            {
                this._mail_destino = value;
            }
        }

        public string mail_copia
        {
            get
            {
                return _mail_copia;
            }
            set
            {
                this._mail_copia = value;
            }
        }

        public string mail_oculto
        {
            get
            {
                return _mail_oculto;
            }
            set
            {
                this._mail_oculto = value;
            }
        }

        public string mail_body
        {
            get
            {
                return _mail_body;
            }
            set
            {
                this._mail_body = value;
            }
        }

        public string mail_subject
        {
            get
            {
                return _mail_subject;
            }
            set
            {
                this._mail_subject = value;
            }
        }

        public string mail_ip_smtp
        {
            get
            {
                return _mail_ip_smtp;
            }
            set
            {
                this._mail_ip_smtp = value;
            }
        }

        public string mail_port_smtp
        {
            get
            {
                return _mail_port_smtp;
            }
            set
            {
                this._mail_port_smtp = value;
            }
        }

        #endregion propiedades

        public void EnviarMail()
        {
            return;
            System.Net.Mail.MailMessage netMail = new System.Net.Mail.MailMessage();

            netMail.Body = _mail_body;
            netMail.From = new System.Net.Mail.MailAddress(_mail_origen);
            NIngresos(netMail.To, _mail_destino);
            if (!string.IsNullOrEmpty(_mail_copia) && _mail_copia.Length > 0)
                NIngresos(netMail.CC, _mail_copia);
            if (!string.IsNullOrEmpty(_mail_oculto) && _mail_oculto.Length > 0)
                NIngresos(netMail.Bcc, _mail_oculto);
            netMail.Subject = _mail_subject;

            System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("This is my plain text content, viewable by those clients that don't support html", null, "text/plain");
            System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(netMail.Body, null, "text/html");
            netMail.AlternateViews.Add(plainView);
            netMail.AlternateViews.Add(htmlView);

            System.Net.Mail.SmtpClient objSMTPClient = new System.Net.Mail.SmtpClient(_mail_ip_smtp);
            objSMTPClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            if (_mail_origen.IndexOf("<") > 0)
            {
                int desde = _mail_origen.IndexOf("<");
                int hasta = _mail_origen.IndexOf(">") - desde;
                _mail_origen = _mail_origen.Substring(desde + 1, hasta - 1);
            }
            objSMTPClient.Credentials = new System.Net.NetworkCredential(_mail_origen, _mail_password);
            objSMTPClient.Port = Utilidades.Utiles.ConvertirAInt32(_mail_port_smtp);
            objSMTPClient.Host = _mail_ip_smtp;
            objSMTPClient.EnableSsl = true;
            objSMTPClient.Send(netMail);
        }

        public void EnviarMail(out string salida)
        {
            salida = "";
            return;
            try
            {
                System.Net.Mail.MailMessage netMail = new System.Net.Mail.MailMessage();

                netMail.Body = _mail_body;
                netMail.From = new System.Net.Mail.MailAddress(_mail_origen);
                NIngresos(netMail.To, _mail_destino);
                if (!string.IsNullOrEmpty(_mail_copia) && _mail_copia.Length > 0)
                    NIngresos(netMail.CC, _mail_copia);
                if (!string.IsNullOrEmpty(_mail_oculto) && _mail_oculto.Length > 0)
                    NIngresos(netMail.Bcc, _mail_oculto);
                netMail.Subject = _mail_subject;

                System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("This is my plain text content, viewable by those clients that don't support html", null, "text/plain");
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(netMail.Body, null, "text/html");
                netMail.AlternateViews.Add(plainView);
                netMail.AlternateViews.Add(htmlView);

                System.Net.Mail.SmtpClient objSMTPClient = new System.Net.Mail.SmtpClient(_mail_ip_smtp);
                objSMTPClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

                if (_mail_origen.IndexOf("<") > 0)
                {
                    int desde = _mail_origen.IndexOf("<");
                    int hasta = _mail_origen.IndexOf(">") - desde;
                    _mail_origen = _mail_origen.Substring(desde + 1, hasta - 1);
                }
                objSMTPClient.Credentials = new System.Net.NetworkCredential(_mail_origen, _mail_password);
                objSMTPClient.Port = Utilidades.Utiles.ConvertirAInt32(_mail_port_smtp);
                objSMTPClient.Host = _mail_ip_smtp;
                objSMTPClient.EnableSsl = true;
                objSMTPClient.Send(netMail);
                salida = string.Empty;
            }
            catch (Exception ex)
            {
                salida = ex.Message;
            }
        }

        //public void EnviarMail(string rutaFisicaImagen, string nombreImagen)
        //{
        //    System.Net.Mail.MailMessage netMail = new System.Net.Mail.MailMessage();
        //    //netMail.BodyEncoding = System.Text.Encoding.GetEncoding("UTF-8"); //iso-8859-1

        //    netMail.Body = _mail_body;
        //    netMail.From = new System.Net.Mail.MailAddress(_mail_origen);
        //    netMail.To.Add(_mail_destino);
        //    if (!string.IsNullOrEmpty(_mail_copia) && _mail_copia.Length > 0)
        //        NIngresos(netMail.CC, _mail_copia);
        //    if (!string.IsNullOrEmpty(_mail_oculto) && _mail_oculto.Length > 0)
        //        NIngresos(netMail.Bcc, _mail_oculto);
        //    netMail.Subject = _mail_subject;

        //    System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("This is my plain text content, viewable by those clients that don't support html", null, "text/plain");
        //    System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(netMail.Body, null, "text/html");
        //    System.Net.Mail.LinkedResource imagen = new System.Net.Mail.LinkedResource(rutaFisicaImagen);
        //    imagen.ContentId = nombreImagen;
        //    htmlView.LinkedResources.Add(imagen);
        //    netMail.AlternateViews.Add(plainView);
        //    netMail.AlternateViews.Add(htmlView);

        //    //netMail.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess; //confirmación de entrega
        //    System.Net.Mail.SmtpClient objSMTPClient = new System.Net.Mail.SmtpClient(_mail_ip_smtp);
        //    objSMTPClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

        //    if (_mail_origen.IndexOf("<") > 0)
        //    {
        //        int desde = _mail_origen.IndexOf("<");
        //        int hasta = _mail_origen.IndexOf(">") - desde;
        //        _mail_origen = _mail_origen.Substring(desde + 1, hasta - 1);
        //    }
        //    objSMTPClient.Credentials = new System.Net.NetworkCredential(_mail_origen, _mail_password);

        //    objSMTPClient.Send(netMail);
        //}

        public void EnviarMailNet(string ruta01, string ruta02, string ruta03, FileUpload FileUpload1, FileUpload FileUpload2, FileUpload FileUpload3)
        {
            return;
            System.Net.Mail.MailMessage netMail = new System.Net.Mail.MailMessage();
            //netMail.BodyEncoding = System.Text.Encoding.GetEncoding("windows-1252");
            //netMail.SubjectEncoding = System.Text.Encoding.GetEncoding("windows-1252");

            string nombrearchivo = "";
            netMail.Body = _mail_body;
            netMail.From = new System.Net.Mail.MailAddress(_mail_origen);
            //netMail.To.Add(_mail_destino);
            NIngresos(netMail.To, _mail_destino);
            if (!string.IsNullOrEmpty(_mail_copia) && _mail_copia.Length > 0)
                NIngresos(netMail.CC, _mail_copia);
            if (!string.IsNullOrEmpty(_mail_oculto) && _mail_oculto.Length > 0)
                NIngresos(netMail.Bcc, _mail_oculto);
            netMail.Subject = _mail_subject;

            if (FileUpload1.PostedFile != null)
            {
                System.Web.HttpPostedFile ulFile = FileUpload1.PostedFile;
                int nFileLen = ulFile.ContentLength;
                if (nFileLen > 0)
                {
                    FileUpload1.PostedFile.SaveAs(ruta01);
                    //System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(ruta01);
                    //attach.NameEncoding = System.Text.Encoding.UTF8;
                    //attach.ContentType.CharSet="UTF-8";
                    //netMail.Headers.Add("Content-Transfer-Encoding", "base64");
                    //netMail.Attachments.Add(attach);

                    nombrearchivo = ObtenerNombre(ruta01);
                    System.Net.Mail.Attachment attachment = AttachmentHelper.CreateAttachment(ruta01, nombrearchivo, System.Net.Mime.TransferEncoding.Base64);
                    netMail.Attachments.Add(attachment);
                }
                else if (ruta01.Length > 10 && ruta01.LastIndexOf('.') > 10 && ruta01.ToLower().LastIndexOf("listado") > 0) //listado de cursos
                {
                    nombrearchivo = ObtenerNombre(ruta01);
                    System.Net.Mail.Attachment attachment = AttachmentHelper.CreateAttachment(ruta01, nombrearchivo, System.Net.Mime.TransferEncoding.Base64);
                    netMail.Attachments.Add(attachment);
                }
            }

            if (FileUpload2.PostedFile != null)
            {
                System.Web.HttpPostedFile ulFile = FileUpload2.PostedFile;
                int nFileLen = ulFile.ContentLength;
                if (nFileLen > 0)
                {
                    FileUpload2.PostedFile.SaveAs(ruta02);
                    nombrearchivo = ObtenerNombre(ruta02);
                    System.Net.Mail.Attachment attachment = AttachmentHelper.CreateAttachment(ruta02, nombrearchivo, System.Net.Mime.TransferEncoding.Base64);
                    netMail.Attachments.Add(attachment);
                }
            }

            if (FileUpload3.PostedFile != null)
            {
                System.Web.HttpPostedFile ulFile = FileUpload3.PostedFile;
                int nFileLen = ulFile.ContentLength;
                if (nFileLen > 0)
                {
                    FileUpload3.PostedFile.SaveAs(ruta03);
                    nombrearchivo = ObtenerNombre(ruta03);
                    System.Net.Mail.Attachment attachment = AttachmentHelper.CreateAttachment(ruta03, nombrearchivo, System.Net.Mime.TransferEncoding.Base64);
                    netMail.Attachments.Add(attachment);
                }
            }

            System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("This is my plain text content, viewable by those clients that don't support html", null, "text/plain");
            System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(netMail.Body, null, "text/html");
            netMail.AlternateViews.Add(plainView);
            netMail.AlternateViews.Add(htmlView);

            //netMail.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess; //confirmación de entrega
            System.Net.Mail.SmtpClient objSMTPClient = new System.Net.Mail.SmtpClient(_mail_ip_smtp);
            objSMTPClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            if (_mail_origen.IndexOf("<") > 0)
            {
                int desde = _mail_origen.IndexOf("<");
                int hasta = _mail_origen.IndexOf(">") - desde;
                _mail_origen = _mail_origen.Substring(desde + 1, hasta - 1);
            }
            objSMTPClient.Credentials = new System.Net.NetworkCredential(_mail_origen, _mail_password);
            objSMTPClient.Port = Utilidades.Utiles.ConvertirAInt32(_mail_port_smtp);
            objSMTPClient.Host = _mail_ip_smtp;
            objSMTPClient.EnableSsl = true;
            objSMTPClient.Send(netMail);
        }

        public void EnviarMailNet()
        {
            return;
            System.Net.Mail.MailMessage netMail = new System.Net.Mail.MailMessage();

            netMail.Body = _mail_body;
            netMail.From = new System.Net.Mail.MailAddress(_mail_origen, _nombre_origen);
            NIngresos(netMail.To, _mail_destino);
            if (!string.IsNullOrEmpty(_mail_copia) && _mail_copia.Length > 0)
                NIngresos(netMail.CC, _mail_copia);
            if (!string.IsNullOrEmpty(_mail_oculto) && _mail_oculto.Length > 0)
                NIngresos(netMail.Bcc, _mail_oculto);
            netMail.Subject = _mail_subject;

            System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("This is my plain text content, viewable by those clients that don't support html", null, "text/plain");
            System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(netMail.Body, null, "text/html");
            netMail.AlternateViews.Add(plainView);
            netMail.AlternateViews.Add(htmlView);
            
            System.Net.Mail.SmtpClient objSMTPClient = new System.Net.Mail.SmtpClient(_mail_ip_smtp);
            objSMTPClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            if (_mail_origen.IndexOf("<") > 0)
            {
                int desde = _mail_origen.IndexOf("<");
                int hasta = _mail_origen.IndexOf(">") - desde;
                _mail_origen = _mail_origen.Substring(desde + 1, hasta - 1);
            }
            objSMTPClient.Credentials = new System.Net.NetworkCredential(_mail_origen, _mail_password);
            objSMTPClient.Port = Utilidades.Utiles.ConvertirAInt32(_mail_port_smtp);
            objSMTPClient.Host = _mail_ip_smtp;
            objSMTPClient.EnableSsl = true;
            objSMTPClient.Send(netMail);
        }

        public void EnviarMailNet(string directorio, string archivo)
        {
            return;
            try
            {
                System.Net.Mail.MailMessage netMail = new System.Net.Mail.MailMessage();

                netMail.Body = _mail_body;
                netMail.From = new System.Net.Mail.MailAddress(_mail_origen);
                NIngresos(netMail.To, _mail_destino);
                if (!string.IsNullOrEmpty(_mail_copia) && _mail_copia.Length > 0)
                    NIngresos(netMail.CC, _mail_copia);
                if (!string.IsNullOrEmpty(_mail_oculto) && _mail_oculto.Length > 0)
                    NIngresos(netMail.Bcc, _mail_oculto);
                netMail.Subject = _mail_subject;

                if (!string.IsNullOrEmpty(archivo))
                {
                    System.Net.Mail.Attachment attachment = AttachmentHelper.CreateAttachment(directorio, archivo, System.Net.Mime.TransferEncoding.Base64);
                    netMail.Attachments.Add(attachment);
                }

                System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("This is my plain text content, viewable by those clients that don't support html", null, "text/plain");
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(netMail.Body, null, "text/html");
                netMail.AlternateViews.Add(plainView);
                netMail.AlternateViews.Add(htmlView);

                System.Net.Mail.SmtpClient objSMTPClient = new System.Net.Mail.SmtpClient(_mail_ip_smtp);
                objSMTPClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

                if (_mail_origen.IndexOf("<") > 0)
                {
                    int desde = _mail_origen.IndexOf("<");
                    int hasta = _mail_origen.IndexOf(">") - desde;
                    _mail_origen = _mail_origen.Substring(desde + 1, hasta - 1);
                }
                objSMTPClient.Credentials = new System.Net.NetworkCredential(_mail_origen, _mail_password);
                objSMTPClient.Port = Utilidades.Utiles.ConvertirAInt32(_mail_port_smtp);
                objSMTPClient.Host = _mail_ip_smtp;
                objSMTPClient.EnableSsl = true;
                objSMTPClient.Send(netMail);
            }
            catch { }
        }

        private string ObtenerNombre(string ruta)
        {
            string nombre = "";
            if (ruta.Length > 0)
            {
                string[] array = ruta.Split('\\');
                int largo = array.Length;
                nombre = array[largo - 1];
                nombre = nombre.Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o').Replace('ú', 'u').Replace(' ', '_').Replace('ñ', 'n');
            }

            return nombre;
        }

        private void NIngresos(System.Net.Mail.MailAddressCollection mailAddressCollection, string mailes)
        {
            if (mailes.Length > 0)
            {
                string[] array = mailes.Split(';');
                foreach (string item in array)
                {
                    if (item.Length > 5)
                        mailAddressCollection.Add(item);
                }
            }
        }

        //public void EnviarMail(string ruta01, string ruta02, string ruta03, FileUpload FileUpload1, FileUpload FileUpload2, FileUpload FileUpload3)
        //{
        //    System.Net.Mail.MailMessage netMail = new System.Net.Mail.MailMessage();
        //    //netMail.BodyEncoding = System.Text.Encoding.GetEncoding("windows-1252");
        //    //netMail.SubjectEncoding = System.Text.Encoding.GetEncoding("windows-1252");

        //    string nombrearchivo = "";
        //    netMail.Body = _mail_body;
        //    netMail.From = new System.Net.Mail.MailAddress(_mail_origen);
        //    //netMail.To.Add(_mail_destino);
        //    NIngresos(netMail.To, _mail_destino);
        //    if (!string.IsNullOrEmpty(_mail_copia) && _mail_copia.Length > 0)
        //        NIngresos(netMail.CC, _mail_copia);
        //    if (!string.IsNullOrEmpty(_mail_oculto) && _mail_oculto.Length > 0)
        //        NIngresos(netMail.Bcc, _mail_oculto);
        //    netMail.Subject = _mail_subject;

        //    if (FileUpload1.PostedFile != null)
        //    {
        //        System.Web.HttpPostedFile ulFile = FileUpload1.PostedFile;
        //        int nFileLen = ulFile.ContentLength;
        //        if (nFileLen > 0)
        //        {
        //            FileUpload1.PostedFile.SaveAs(ruta01);
        //            //System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(ruta01);
        //            //attach.NameEncoding = System.Text.Encoding.UTF8;
        //            //attach.ContentType.CharSet="UTF-8";
        //            //netMail.Headers.Add("Content-Transfer-Encoding", "base64");
        //            //netMail.Attachments.Add(attach);

        //            nombrearchivo = ObtenerNombre(ruta01);
        //            System.Net.Mail.Attachment attachment = AttachmentHelper.CreateAttachment(ruta01, nombrearchivo, System.Net.Mime.TransferEncoding.Base64);
        //            netMail.Attachments.Add(attachment);
        //        }
        //        else if (ruta01.Length > 10 && ruta01.LastIndexOf('.') > 10 && ruta01.ToLower().LastIndexOf("listado") > 0) //listado de cursos
        //        {
        //            nombrearchivo = ObtenerNombre(ruta01);
        //            System.Net.Mail.Attachment attachment = AttachmentHelper.CreateAttachment(ruta01, nombrearchivo, System.Net.Mime.TransferEncoding.Base64);
        //            netMail.Attachments.Add(attachment);
        //        }
        //    }

        //    if (FileUpload2.PostedFile != null)
        //    {
        //        System.Web.HttpPostedFile ulFile = FileUpload2.PostedFile;
        //        int nFileLen = ulFile.ContentLength;
        //        if (nFileLen > 0)
        //        {
        //            FileUpload2.PostedFile.SaveAs(ruta02);
        //            nombrearchivo = ObtenerNombre(ruta02);
        //            System.Net.Mail.Attachment attachment = AttachmentHelper.CreateAttachment(ruta02, nombrearchivo, System.Net.Mime.TransferEncoding.Base64);
        //            netMail.Attachments.Add(attachment);
        //        }
        //    }

        //    if (FileUpload3.PostedFile != null)
        //    {
        //        System.Web.HttpPostedFile ulFile = FileUpload3.PostedFile;
        //        int nFileLen = ulFile.ContentLength;
        //        if (nFileLen > 0)
        //        {
        //            FileUpload3.PostedFile.SaveAs(ruta03);
        //            nombrearchivo = ObtenerNombre(ruta03);
        //            System.Net.Mail.Attachment attachment = AttachmentHelper.CreateAttachment(ruta03, nombrearchivo, System.Net.Mime.TransferEncoding.Base64);
        //            netMail.Attachments.Add(attachment);
        //        }
        //    }

        //    System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("This is my plain text content, viewable by those clients that don't support html", null, "text/plain");
        //    System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(netMail.Body, null, "text/html");
        //    netMail.AlternateViews.Add(plainView);
        //    netMail.AlternateViews.Add(htmlView);

        //    //netMail.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess; //confirmación de entrega
        //    System.Net.Mail.SmtpClient objSMTPClient = new System.Net.Mail.SmtpClient(_mail_ip_smtp);
        //    objSMTPClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

        //    if (_mail_origen.IndexOf("<") > 0)
        //    {
        //        int desde = _mail_origen.IndexOf("<");
        //        int hasta = _mail_origen.IndexOf(">") - desde;
        //        _mail_origen = _mail_origen.Substring(desde + 1, hasta - 1);
        //    }
        //    objSMTPClient.Credentials = new System.Net.NetworkCredential(_mail_origen, _mail_password);
        //    //objSMTPClient.UseDefaultCredentials = true;
        //    //objSMTPClient.EnableSsl = false;
        //    objSMTPClient.Send(netMail);

        //}

        //public void EnviarMail(string ruta01, string ruta02, string ruta03)
        //{
        //    try
        //    {
        //        MailMessage Mail = new MailMessage();
        //        _mail_body = _mail_body.Replace("\r\n", "<br>");

        //        Mail.Body = _mail_body;
        //        Mail.From = _mail_origen;
        //        Mail.To = _mail_destino;
        //        Mail.Cc = _mail_copia;
        //        Mail.Bcc = _mail_oculto;
        //        Mail.Subject = _mail_subject;
        //        Mail.BodyFormat = MailFormat.Html;

        //        MailAttachment attach = new MailAttachment(ruta01);
        //        Mail.Attachments.Add(attach);

        //        if (_mail_origen.IndexOf("<") > 0)
        //        {
        //            int desde = _mail_origen.IndexOf("<");
        //            int hasta = _mail_origen.IndexOf(">") - desde;
        //            _mail_origen = _mail_origen.Substring(desde + 1, hasta - 1);
        //        }

        //        Mail.Fields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"] = smtpauth;
        //        Mail.Fields["http://schemas.microsoft.com/cdo/configuration/_mail_origen"] = _mail_origen;
        //        Mail.Fields["http://schemas.microsoft.com/cdo/configuration/_mail_password"] = _mail_password;

        //        SmtpMail.SmtpServer = _mail_ip_smtp;
        //        SmtpMail.Send(Mail);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        public string RecuperarPlantilla(string plantilla, string campo)
        {
            return GetStringText(plantilla, campo);
        }

        public void ReemplazarPlantilla(string variablePlantilla, string valor)
        {
            this.mail_body = this.mail_body.Replace(variablePlantilla.Trim(), valor);
        }

        public string ReemplazarPlantilla(string plantillaMail, string variablePlantilla, string valor)
        {
            string plantilla = plantillaMail;
            plantilla = plantilla.Replace(variablePlantilla.Trim(), valor);
            return plantilla;
        }

        private string GetStringText(string modulo, string key)
        {
            //System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentUICulture;
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("es-CL");
            System.Reflection.Assembly aThis = System.Reflection.Assembly.GetExecutingAssembly();
            string resStrings = aThis.GetName().Name + "." + modulo + "." + ci.Name;
            System.Reflection.Assembly aRes = System.Reflection.Assembly.GetExecutingAssembly().GetSatelliteAssembly(ci);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager(resStrings, aRes);
            return rm.GetString(key);
        }

        private string ObtenerMailPassword(string casilla)
        {
            try
            {
                if (casilla.IndexOf("<") > 0)
                {
                    int desde = casilla.IndexOf("<");
                    int hasta = casilla.IndexOf(">") - desde;
                    casilla = casilla.Substring(desde + 1, hasta - 1);
                }

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand com = db.GetStoredProcCommand("pa_MailNetglobalisObtener",
                    casilla);

                string res = db.ExecuteScalar(com).ToString();
                return res;
            }
            catch (Exception)
            {
                return "";
            }
            // return "ago430";
        }
    }
}