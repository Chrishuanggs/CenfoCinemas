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
        public MovieManager() { }

        /*
         * Método para la creación de una película
         * Valida que la fecha de lanzamiento no sea futura
         * Valida que el título no esté duplicado
         * Valida que los campos obligatorios tengan contenido
         */
        public void Create(Movie movie)
        {
            try
            {
                // Validar que los campos obligatorios tengan contenido
                if (ValidateRequiredFields(movie))
                {
                    var mCrud = new MovieCrudFactory();

                    // Validar que la fecha de lanzamiento no sea futura
                    if (IsValidReleaseDate(movie.ReleaseDate))
                    {

                        // Consultar en la DB si existe una película con ese título
                        var movieExists = mCrud.RetrieveByTitle<Movie>(movie);

                        if (movieExists == null)
                        {
                            mCrud.Create(movie);
                        }
                        else
                        {
                            throw new Exception("Ya existe una película con ese título");
                        }
                    }
                    else
                    {
                        throw new Exception("La fecha de lanzamiento no puede ser en el futuro");
                    }
                }
                else
                {
                    throw new Exception("Todos los campos obligatorios deben ser completados");
                }
            }
            catch (Exception ex)
            {
                ManagerException(ex);
            }
        }

        
         // Metodo para validar que todos los campos obligatorios esten completos
      
        private bool ValidateRequiredFields(Movie movie)
        {
            return !string.IsNullOrEmpty(movie.Title) &&
                   !string.IsNullOrEmpty(movie.Description) &&
                   !string.IsNullOrEmpty(movie.Genre) &&
                   !string.IsNullOrEmpty(movie.Director);
        }

          //Metodo para validar que la fecha de lanzamiento no sea futura
         
        private bool IsValidReleaseDate(DateTime releaseDate)
        {
            return releaseDate <= DateTime.Now;
        }
    }
}