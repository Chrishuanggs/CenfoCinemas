using CoreApp;
using DataAccess.CRUD;
using DataAccess.DAOs;
using DTOs;
using Newtonsoft.Json;
using SqlOperation = DataAccess.DAOs.SqlOperation;

public class Program{
    public static void Main(string[] args)
    {

        Console.WriteLine("Seleccione la opcion deseada: ");
        Console.WriteLine("1. Crear usuario");
        Console.WriteLine("2. Consultar todos los usuarios");
        Console.WriteLine("3. Consultar usuario por ID");
        Console.WriteLine("4. Actualizar usuarios");
        Console.WriteLine("5. Eliminar usuario");
        Console.WriteLine("6. Crear pelicula");
        Console.WriteLine("7. Consultar todas las peliculas");
        Console.WriteLine("8. Consultar pelicula por ID");
        Console.WriteLine("9. Actualizar pelicula");
        Console.WriteLine("10. Eliminar pelicula");

        var option = Int32.Parse(Console.ReadLine());

        // Declarar sqlOperation aquí, fuera del switch
        var sqlOperation = new SqlOperation();

        switch (option)
        {
            case 1:
                var uCrud = new UserCrudFactory();
                Console.WriteLine("Digite el codigo de usuario: ");
                var userCode=Console.ReadLine();

                Console.WriteLine("Digite el nombre del usuario: ");
                var name = Console.ReadLine();

                Console.WriteLine("Digite el correo del usuario: ");
                var email = Console.ReadLine();

                Console.WriteLine("Digite la contrasena del usuario: ");

                var password = Console.ReadLine();
                var status = "AC";

                Console.WriteLine("Digite la fecha de nacimiento del usuario: ");
                var bdate = DateTime.Parse(Console.ReadLine());

                //Crear objeto a partir de los valores capturados en consola
                var user = new User()
                {
                    UserCode = userCode,
                    Name = name,
                    Email = email,
                    Password = password,
                    Status = status,
                    BirthDate = bdate
                };
                var um = new UserManager();
                um.Create(user);

                break;

            case 2:
                uCrud = new UserCrudFactory();
                var listUsers = uCrud.RetrieveAll<User>();
                foreach(var u in listUsers)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(u));
                }
                break;

            case 3:
                uCrud = new UserCrudFactory();
                Console.WriteLine("Digite el ID del usuario a consultar: ");
                var userId = Int32.Parse(Console.ReadLine());

                var userById = uCrud.RetrieveById<User>(userId);
                if (userById != null)
                {
                    Console.WriteLine("Usuario encontrado:");
                    Console.WriteLine(JsonConvert.SerializeObject(userById));
                }
                else
                {
                    Console.WriteLine("Usuario no encontrado.");
                }
                break;

            case 6:
                var mCrud = new MovieCrudFactory();
                Console.WriteLine("Digite el titulo");
                var title = Console.ReadLine();

                Console.WriteLine("Digitre la descripcion");
                var desc = Console.ReadLine();

                Console.WriteLine("Digite la fecha de lanzamiento");
              var rDate = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Digite el genero de la pelicula");
                var genre = Console.ReadLine();

                Console.WriteLine("Digite el director");
                var director = Console.ReadLine();


                //Crear objeto a partir de los valores capturados en consola
                var movie = new Movie()
                {
                    Title = title,
                    Description = desc,
                    ReleaseDate = rDate,
                    Genre = genre,
                    Director = director
    
                };
                var mm = new MovieManager();
                mm.Create(movie);
                break;

            case 7:

                mCrud = new MovieCrudFactory();
                var listMovies = mCrud.RetrieveAll<Movie>();
                foreach (var u in listMovies)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(u));
                }

                break;

            case 8:
                mCrud = new MovieCrudFactory();
                Console.WriteLine("Digite el ID de la pelicula a consultar: ");
                var movieId = Int32.Parse(Console.ReadLine());

                var movieById = mCrud.RetrieveById<Movie>(movieId);
                if (movieById != null)
                {
                    Console.WriteLine("Pelicula encontrada:");
                    Console.WriteLine(JsonConvert.SerializeObject(movieById));
                }
                else
                {
                    Console.WriteLine("Pelicula no encontrada.");
                }
                break;
        }

        // Ejecucion de procedures
        var sqlDao = SqlDao.GetInstance();
    }
}