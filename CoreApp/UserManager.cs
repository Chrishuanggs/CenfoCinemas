using DataAccess.CRUD;
using DTOs;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class UserManager : BaseManager
    {
        public UserManager() { }
        /*
         * Metodo para la creacion de un usuario
         * Valida que el usuario sea mayor de 18 years
         * Valida que el codigo de usuario este disponible
         * Valida que el correo electronico no este registrado
         * Envia un correo electronico de bienvenida
         */
        public void Create(User user)
        {
            try
            {
                //Validar la edad
                if (IsOver18(user)) {
                    var uCrud = new UserCrudFactory();

                    //Consultar en la DB si existe con ese codigo
                    var uExist = uCrud.RetrieveByUserCode<User>(user);
                   
                    if (uExist == null) {

                        //Consultar si existe por correo
                       uExist=uCrud.RetrieveByEmail<User>(user);

                        if (uExist == null) {
                            uCrud.Create(user);
                        SendWelcomeEmail(user);
                        }
                        else
                        {
                            throw new Exception("Este correo electronico no esta disponible o ya se encuentra registrado");
                        }
                    }
                    else
                    {
                        throw new Exception("El codigo de usuario no esta dispoible");
                    }
                }
                else
                {
                    throw new Exception("Usuario no tiene 18");
                }
            }
            catch (Exception ex)
            {
                ManagerException(ex);
            }
        }
        async Task SendWelcomeEmail(User user)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress("chuangr@ucenfotec.ac.cr", "Chris Huang");
            var to = new EmailAddress(user.Email, user.Name);
            var subject = "¡Bienvenido a CenfoCinemas" + user.Name + "!";

            var plainTextContent = $"Hola {user.Name}, te damos la bienvenida a CenfoCinemas. Estamos emocionados de tenerte con nosotros.";
            var htmlContent = $"<strong>Hola {user.Name}</strong>, te damos la bienvenida a CenfoCinemas. Estamos emocionados de tenerte con nosotros. <br/><br/>Registrate en CenfoCinemas para registrarte de CenfoCinemas. Tu codigo de usuario es <strong>{user.UserCode}</strong><br/>con nosotros.<br/>Saludos,<br/>Equipo administrativo de CenfoCinemas<br/><br/>Puedes contactarnos en info@cenfocinemas.com<br/>msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
        private bool IsOver18(User user)
        {

            var currentDate = DateTime.Now;
            int age = currentDate.Year - user.BirthDate.Year;

            if (user.BirthDate > currentDate.AddYears(-age).Date)
            {
                age--;
            }
            return age >= 18;
        }
    }
}
