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
        public async Task Create(User user)
        {
            try
            {
                //Validar la edad
                if (IsOver18(user))
                {
                    var uCrud = new UserCrudFactory();

                    //Consultar en la DB si existe con ese codigo
                    var uExist = uCrud.RetrieveByUserCode<User>(user);

                    if (uExist == null)
                    {
                        //Consultar si existe por correo
                        uExist = uCrud.RetrieveByEmail<User>(user);

                        if (uExist == null)
                        {
                            uCrud.Create(user);
                            await SendWelcomeEmail(user.Email, user.Name);
                        }
                        else
                        {
                            throw new Exception("Este correo electronico no esta disponible o ya se encuentra registrado");
                        }
                    }
                    else
                    {
                        throw new Exception("El codigo de usuario no esta disponible");
                    }
                }
                else
                {
                    throw new Exception("Usuario debe ser mayor de 18 años");
                }
            }
            catch (Exception ex)
            {
                ManagerException(ex);
            }
        }

        private async Task SendWelcomeEmail(string email, string name)
        {
            try
            {
                // La API key debe estar en configuración o variables de entorno
                // NO hardcodear la API key en el código
                var apiKey = Environment.GetEnvironmentVariable("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");

                if (string.IsNullOrEmpty(apiKey))
                {
                    Console.WriteLine("Warning: SendGrid API key not found. Email not sent.");
                    return;
                }

                var client = new SendGridClient(apiKey);
                var from_email = new EmailAddress("chrishuang060@gmail.com", "CenfoCinemas");
                var subject = "¡Bienvenido a CenfoCinemas!";
                var to_email = new EmailAddress(email, name);

                var plainTextContent = $"Hola {name},\n\n¡Bienvenid@ a CenfoCinemas! Tu cuenta ha sido creada exitosamente.\n\nGracias por unirte a nosotros.\n\nSaludos,\nEl equipo de CenfoCinemas";

                var htmlContent = $@"
                    <h2>¡Bienvenid@ a CenfoCinemas!</h2>
                    <p>Hola <strong>{name}</strong>,</p>
                    <p>Tu cuenta ha sido creada exitosamente.</p>
                    <p>¡Gracias por unirte a nosotros!</p>
                    <br>
                    <p>Saludos,<br>El equipo de CenfoCinemas</p>";

                var msg = MailHelper.CreateSingleEmail(from_email, to_email, subject, plainTextContent, htmlContent);

                var response = await client.SendEmailAsync(msg);

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    Console.WriteLine($"Email enviado exitosamente a {email}");
                }
                else
                {
                    Console.WriteLine($"Error enviando email: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error enviando email de bienvenida: {ex.Message}");
                // No lanzar excepción aquí para no interrumpir la creación del usuario
            }
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