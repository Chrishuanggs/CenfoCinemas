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

        public List<Movie> RetrieveAll()
        {
            try
            {
                var mCrud = new MovieCrudFactory();
                return mCrud.RetrieveAll<Movie>();
            }
            catch (Exception ex)
            {
                ManagerException(ex);
                return new List<Movie>();
            }
        }

        public Movie RetrieveById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new Exception("ID de película inválido");
                }

                var mCrud = new MovieCrudFactory();
                return mCrud.RetrieveById<Movie>(id);
            }
            catch (Exception ex)
            {
                ManagerException(ex);
                return null;
            }
        }

        public Movie RetrieveByTitle(string title)
        {
            try
            {
                if (string.IsNullOrEmpty(title))
                {
                    throw new Exception("El título no puede estar vacío");
                }

                var mCrud = new MovieCrudFactory();
                var movie = new Movie { Title = title };
                return mCrud.RetrieveByTitle<Movie>(movie);
            }
            catch (Exception ex)
            {
                ManagerException(ex);
                return null;
            }
        }

        public void Update(Movie movie)
        {
            try
            {
                // Validar que los campos obligatorios tengan contenido
                if (!ValidateRequiredFields(movie))
                {
                    throw new Exception("Todos los campos obligatorios deben ser completados");
                }

                if (movie.Id <= 0)
                {
                    throw new Exception("ID de película inválido");
                }

                var mCrud = new MovieCrudFactory();

                // Verificar que la película existe
                var existingMovie = mCrud.RetrieveById<Movie>(movie.Id);
                if (existingMovie == null)
                {
                    throw new Exception("La película no existe");
                }

                // Validar que la fecha de lanzamiento no sea futura
                if (!IsValidReleaseDate(movie.ReleaseDate))
                {
                    throw new Exception("La fecha de lanzamiento no puede ser en el futuro");
                }

                // Validar que no haya otra película con el mismo título (excluyendo la actual)
                var movieWithSameTitle = mCrud.RetrieveByTitle<Movie>(movie);
                if (movieWithSameTitle != null && movieWithSameTitle.Id != movie.Id)
                {
                    throw new Exception("Ya existe otra película con ese título");
                }

                mCrud.Update(movie);
            }
            catch (Exception ex)
            {
                ManagerException(ex);
            }
        }

        public void Delete(int movieId)
        {
            try
            {
                if (movieId <= 0)
                {
                    throw new Exception("ID de película inválido");
                }

                var mCrud = new MovieCrudFactory();

                // Verificar que la película existe
                var existingMovie = mCrud.RetrieveById<Movie>(movieId);
                if (existingMovie == null)
                {
                    throw new Exception("La película no existe");
                }

                var movieToDelete = new Movie { Id = movieId };
                mCrud.Delete(movieToDelete);
            }
            catch (Exception ex)
            {
                ManagerException(ex);
            }
        }

        private bool IsValidReleaseDate(DateTime releaseDate)
        {
            return releaseDate <= DateTime.Now;
        }

        // Metodo para validar que todos los campos obligatorios esten completos

        private bool ValidateRequiredFields(Movie movie)
        {
            return !string.IsNullOrEmpty(movie.Title) &&
                   !string.IsNullOrEmpty(movie.Description) &&
                   !string.IsNullOrEmpty(movie.Genre) &&
                   !string.IsNullOrEmpty(movie.Director);
        }
    }
}