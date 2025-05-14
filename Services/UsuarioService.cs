using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ParqueoAutomotor.Data;
using ParqueoAutomotor.Models;
using System.Security.Cryptography;
using System.Text;

namespace ParqueoAutomotor.Services
{
    public class UsuarioService
    {
        private readonly DatabaseHelper dbHelper;

        public UsuarioService()
        {
            dbHelper = new DatabaseHelper();
        }

        public Usuario ObtenerPorNombre(string Nombre)
        {
            string query = "SELECT * FROM Usuario WHERE Nombre = @Nombre";
            SqlParameter[] parameters = {
                new SqlParameter("@Nombre", Nombre)
            };

            DataTable dt = dbHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0)
            { return null; }
               

            DataRow row = dt.Rows[0];
            return new Usuario
            {
                Id = Convert.ToInt32(row["Id"]),
                Nombre = row["Nombre"].ToString(),
                Clave = row["Clave"].ToString()
            };


        }

        public bool CrearUsuario(Usuario usuario)
        {
            string query = "INSERT INTO Usuario (Nombre, Clave) VALUES (@Nombre, @Clave)";
            SqlParameter[] parameters = {
                new SqlParameter("@Nombre", usuario.Nombre),
                new SqlParameter("@Clave", HashPassword(usuario.Clave))
            };

            return dbHelper.ExecuteNonQuery(query, parameters) > 0;
        }


        // Método para generar hash SHA256
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}