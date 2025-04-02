using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ParqueoAutomotor.Data;
using ParqueoAutomotor.Models;

namespace ParqueoAutomotor.Services
{
    public class VehiculoService
    {
        private readonly DatabaseHelper dbHelper;

        public VehiculoService()
        {
            dbHelper = new DatabaseHelper();
        }

        public List<Vehiculos> ObtenerVehiculos()
        {
            List<Vehiculos> vehiculos = new List<Vehiculos>();
            DataTable dt = dbHelper.ExecuteQuery("SELECT * FROM Vehiculos");

            foreach (DataRow row in dt.Rows)
            {
                vehiculos.Add(new Vehiculos
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Placa = row["Placa"].ToString(),
                    TipoVehiculo = row["TipoVehiculo"].ToString(),
                    EsHibridoOElectrico = Convert.ToInt32(row["EsHibridoOElctrico"]),
                    HoraIngreso = Convert.ToDateTime(row["HoraIngreso"]),
                    HoraSalida = row["HoraSalida"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["HoraSalida"]),
                    PlazaAsignada = Convert.ToInt32(row["PlazaAsignada"])
                });
            }
            return vehiculos;
        }

        public bool AgregarVehiculo(Vehiculos vehiculo)
        {
            string query = "INSERT INTO Vehiculos (Placa, TipoVehiculo, EsHibridoOElctrico, HoraIngreso, PlazaAsignada) VALUES (@Placa, @TipoVehiculo, @EsHibridoOElctrico, @HoraIngreso, @PlazaAsignada)";
            SqlParameter[] parameters = {
                new SqlParameter("@Placa", vehiculo.Placa),
                new SqlParameter("@TipoVehiculo", vehiculo.TipoVehiculo),
                new SqlParameter("@EsHibridoOElctrico", vehiculo.EsHibridoOElectrico),
                new SqlParameter("@HoraIngreso", vehiculo.HoraIngreso),
                new SqlParameter("@PlazaAsignada", vehiculo.PlazaAsignada)
            };
            return dbHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool ActualizarVehiculo(int id, Vehiculos vehiculo)
        {
            string query = @"UPDATE Vehiculos 
                         SET Placa = @Placa, 
                             TipoVehiculo = @TipoVehiculo, 
                             EsHibridoOElctrico = @EsHibridoOElctrico, 
                             HoraIngreso = @HoraIngreso, 
                             HoraSalida = @HoraSalida, 
                             PlazaAsignada = @PlazaAsignada
                         WHERE Id = @Id";

            List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@Id", id),
            new SqlParameter("@Placa", vehiculo.Placa),
            new SqlParameter("@TipoVehiculo", vehiculo.TipoVehiculo),
            new SqlParameter("@EsHibridoOElctrico", vehiculo.EsHibridoOElectrico),
            new SqlParameter("@HoraIngreso", vehiculo.HoraIngreso),
            new SqlParameter("@HoraSalida", (object)vehiculo.HoraSalida ?? DBNull.Value),
            new SqlParameter("@PlazaAsignada", vehiculo.PlazaAsignada)
        };

            int filasAfectadas = dbHelper.ExecuteNonQuery(query, parameters.ToArray());
            return filasAfectadas > 0;
        }

        public bool EliminarVehiculo(int id)
        {
            string query = "DELETE FROM Vehiculos WHERE Id = @Id";

            List<SqlParameter> parameters = new List<SqlParameter>
    {
        new SqlParameter("@Id", id)
    };

            int filasAfectadas = dbHelper.ExecuteNonQuery(query, parameters.ToArray());
            return filasAfectadas > 0;
        }

        public Vehiculos ObtenerVehiculoPorId(int vehiculoId)
        {
            string query = "SELECT * FROM Vehiculos WHERE Id = @VehiculoId";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@VehiculoId", vehiculoId)
            };

            // Aquí ejecutamos el query para obtener los detalles del vehículo
            DataTable dt = dbHelper.ExecuteQuery(query, parameters);

            // Si no se encuentra el vehículo, retornamos null
            if (dt.Rows.Count == 0)
                return null;

            // Creamos un objeto Vehiculo y lo devolvemos
            var vehiculo = new Vehiculos
            {
                Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                Placa = dt.Rows[0]["Placa"].ToString(),
                TipoVehiculo = dt.Rows[0]["TipoVehiculo"].ToString(),
                EsHibridoOElectrico = Convert.ToInt32(dt.Rows[0]["EsHibridoOElctrico"]),
                HoraIngreso = Convert.ToDateTime(dt.Rows[0]["HoraIngreso"]),
                HoraSalida = dt.Rows[0]["HoraSalida"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["HoraSalida"]) : (DateTime?)null,
                PlazaAsignada = Convert.ToInt32(dt.Rows[0]["PlazaAsignada"])
            };

            return vehiculo;
        }

        public int CalcularMonto(Vehiculos vehiculo)
        {
            // Calcula el tiempo de estancia en horas
            TimeSpan tiempoEstacionado = DateTime.Now - vehiculo.HoraIngreso;

            // Establece el costo por hora dependiendo del tipo de vehículo
            int costoPorHora = vehiculo.TipoVehiculo.ToLower() == "moto" ? 62 : 120;

            // Si el vehículo es híbrido o eléctrico, aplicamos un 25% de descuento
            if (vehiculo.EsHibridoOElectrico == 1)
            {
                costoPorHora = (int)(costoPorHora * 0.75);  // Aplicamos el descuento
            }

            // Calculamos el monto total (redondeamos el tiempo a horas completas)
            int monto = (int)(Math.Ceiling(tiempoEstacionado.TotalHours)) * costoPorHora;

            return monto;
        }


    }
}