﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    /*
     * Clase u objeto que se encarga de la comunicacion con la base de datos
     * Solo ejecuta Stored Procedures
     * 
     * Esta clase implementa el patron del Singleton
     * para asegurar la existencia de una unica instancia de este objeto
     * 
     */
    public class SqlDao
    {
        // Paso 1: Crear una instancia privada de la misma clase
        private static SqlDao _instance;

        private string _connectionString;

        // Paso 2: Redefinir el constructor default y convertirlo en privado
        private SqlDao()
        {
            _connectionString = string.Empty;
        }

        // Paso 3: Definir el metodo que expone la instancia
        public static SqlDao getInstance() {

            if (_instance == null) { 
                _instance = new SqlDao();
            }
            return _instance;
        }

        // Metodo para la ejecucion de SP sin retorno
        public void ExecuteProcedure(SqlOperation operation)
        {
        // Conectarse a la base de datos
        // Ejecutar SP
        }

        // Metodo para la ejecucion de SP con retorno de data
        public List<Dictionary<String, Object>> ExecuteQueryProcedure(SqlOperation operation)
        {
            // Conectarse a la base de datos
            // Ejecutar SP
            // Capturar el resultado
            // Convertirlo en DTOs
            var list =new List<Dictionary<String, Object>>();
            return list;
        }
    }
}
