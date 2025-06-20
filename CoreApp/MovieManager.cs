using DataAccess.CRUD;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class MovieManager : BaseManager 
    {
        public void Create(Movie movie)
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
                            //Enviar el correo
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
