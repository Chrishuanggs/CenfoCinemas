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
        Console.WriteLine("2. Consultar usuarios");
        Console.WriteLine("3. Actualizar usuarios");
        Console.WriteLine("4. Eliminar usuario");
        Console.WriteLine("5. Crear pelicula");
        Console.WriteLine("6. Consultar peliculas");
        Console.WriteLine("7. Actualizar pelicula");
        Console.WriteLine("8. Eliminar pelicula");

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
                uCrud.Create(user);


                break;

            case 2:
                uCrud = new UserCrudFactory();
                var listUsers = uCrud.RetrieveAll<User>();
                foreach(var u in listUsers)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(u));
                }
                break;

            case 5:
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
                mCrud.Create(movie);
                break;

            case 6:

                mCrud = new MovieCrudFactory();
                var listMovies = mCrud.RetrieveAll<Movie>();
                foreach (var u in listMovies)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(u));
                }

                break;
        }

        // Ejecucion de procedures
        var sqlDao = SqlDao.GetInstance();
    }
}