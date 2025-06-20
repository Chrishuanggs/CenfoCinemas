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
                            SendWelcomeEmail(user.Email, user.Name);
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
        async Task SendWelcomeEmail(string email,string name)
        {
            var apiKey = Environment.GetEnvironmentVariable("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");
            var client = new SendGridClient(apiKey);
            var from_email = new EmailAddress("chuangr@ucenfotec.ac.cr", "Chris Huang");
            var subject = "Bienvenido a CenfoCinemas!";
            var to_email = new EmailAddress(email, name);
            var plainTextContent = "Hola, " + name + "Bienvenid@!";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from_email, to_email, subject, plainTextContent, htmlContent);
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
