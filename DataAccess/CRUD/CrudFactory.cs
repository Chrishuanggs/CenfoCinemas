using DataAccess.DAOs;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    //Clase padre/madre abstracta de los crud
    //Define como se hacen cruds en la arquitectura

    public abstract class CrudFactory
    {
        protected SqlDao _sqlDao;

        //Definir los metodos que forman parte del contrato (clase abstracta)
        //C = create
        //R = retrieve
        //U = update
        //D = delete

        public abstract void Create(BaseDTO baseDTO);
        public abstract void Update(BaseDTO baseDTO);
        public abstract void Delete(BaseDTO baseDTO);

        public abstract T Retrieve<T>();

        public abstract T RetrieveById<T>();
        public abstract List<T> RetrieveAll<T>();

    }
}
