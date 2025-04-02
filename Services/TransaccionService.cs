using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ParqueoAutomotor.Data;
using ParqueoAutomotor.Models;
using System.Web.Razor.Tokenizer;
using System.Web.Util;


namespace ParqueoAutomotor.Services
{
    public class TransaccionService
    {

        private DatabaseHelper dbHelper;

        public TransaccionService()
        {
            dbHelper = new DatabaseHelper();
        }


        public List<Transacciones> ListarTransacciones()
        {
            string query = "SELECT Id, VehiculoId, Monto, FechaTransaccion FROM Transacciones";

            DataTable dt = dbHelper.ExecuteQuery(query);
            List<Transacciones> transacciones = new List<Transacciones>();

            foreach (DataRow row in dt.Rows)
            {
                transacciones.Add(new Transacciones
                {
                    Id = Convert.ToInt32(row["Id"]),
                    VehiculoId = Convert.ToInt32(row["VehiculoId"]),
                    Monto = Convert.ToInt32(row["Monto"]),
                    FechaTransaccion = Convert.ToDateTime(row["FechaTransaccion"])
                });
            }

            return transacciones;
        }

        public int CalcularGananciasDelDia()
        {
            string query = @"SELECT ISNULL(SUM(Monto), 0) 
                     FROM Transacciones 
                     WHERE CAST(FechaTransaccion AS DATE) = CAST(GETDATE() AS DATE)";

            object result = dbHelper.ExecuteScalar(query);
            return Convert.ToInt32(result);
        }

        public bool GenerarTransaccion(int vehiculoId, int monto)
        {
            string query = "INSERT INTO Transacciones (VehiculoId, Monto, FechaTransaccion) VALUES (@VehiculoId, @Monto, @FechaTransaccion)";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@VehiculoId", vehiculoId),
        new SqlParameter("@Monto", monto),
        new SqlParameter("@FechaTransaccion", DateTime.Now)
            };

            int filasAfectadas = dbHelper.ExecuteNonQuery(query, parameters);

            return filasAfectadas > 0;
        }

    }
}