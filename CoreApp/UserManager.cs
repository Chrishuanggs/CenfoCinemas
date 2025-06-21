using DataAccess.CRUD;
using DTOs;
using SendGrid;
using SendGrid.Helpers.Mail;

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

        public List<User> RetrieveAll()
        {
            var uCrud = new UserCrudFactory();
            return uCrud.RetrieveAll<User>();
        }
        public User RetrieveById(int id)
        {
            try
            {
                var uCrud = new UserCrudFactory();
                return uCrud.RetrieveById<User>(id);
            }
            catch (Exception ex)
            {
                ManagerException(ex);
                return null;
            }
        }

        public User RetrieveByUserCode(string userCode)
        {
            try
            {
                var uCrud = new UserCrudFactory();
                var user = new User { UserCode = userCode };
                return uCrud.RetrieveByUserCode<User>(user);
            }
            catch (Exception ex)
            {
                ManagerException(ex);
                return null;
            }
        }

        public User RetrieveByEmail(string email)
        {
            try
            {
                var uCrud = new UserCrudFactory();
                var user = new User { Email = email };
                return uCrud.RetrieveByEmail<User>(user);
            }
            catch (Exception ex)
            {
                ManagerException(ex);
                return null;
            }
        }

        public void Update(User user)
        {
            try
            {
                // Validaciones antes de actualizar
                if (user.Id <= 0)
                {
                    throw new Exception("ID de usuario inválido");
                }

                var uCrud = new UserCrudFactory();

                // Verificar que el usuario existe
                var existingUser = uCrud.RetrieveById<User>(user.Id);
                if (existingUser == null)
                {
                    throw new Exception("El usuario no existe");
                }

                // Validar edad si se actualiza fecha de nacimiento
                if (user.BirthDate != default(DateTime) && !IsOver18(user))
                {
                    throw new Exception("Usuario debe ser mayor de 18 años");
                }

                uCrud.Update(user);
            }
            catch (Exception ex)
            {
                ManagerException(ex);
            }
        }

        public void Delete(int userId)
        {
            try
            {
                var uCrud = new UserCrudFactory();

                // Verificar que el usuario existe
                var existingUser = uCrud.RetrieveById<User>(userId);
                if (existingUser == null)
                {
                    throw new Exception("El usuario no existe");
                }

                var userToDelete = new User { Id = userId };
                uCrud.Delete(userToDelete);
            }
            catch (Exception ex)
            {
                ManagerException(ex);
            }
        }
        private async Task SendWelcomeEmail(string email, string name)
        {
          
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
