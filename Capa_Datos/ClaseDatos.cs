using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using  Capa_Entidad;

namespace Capa_Datos
{
    public class ClaseDatos
    {
        SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sql"].ConnectionString);

        public DataTable ListarLibrosDataTable()
        {
            var sqlCommand = new SqlCommand("listar_libros", Connection);
            var sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            var dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        
        public DataTable BuscarLibrosDataTable(Libro miLibro)
        {
            var sqlCommand = new SqlCommand("buscar_libros", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@titulo", miLibro.Titulo);
            var sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            var dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        
        public string MantenimientoLibrosDataTable(Libro miLibro)
        {
            var accion = "";
            var sqlCommand = new SqlCommand("mantenimiento_libros", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@codigo", miLibro.Codigo);
            sqlCommand.Parameters.AddWithValue("@titulo", miLibro.Titulo);
            sqlCommand.Parameters.AddWithValue("@autor", miLibro.Autor);
            sqlCommand.Parameters.AddWithValue("@editorial", miLibro.Editorial);
            sqlCommand.Parameters.AddWithValue("@precio", miLibro.Precio);
            sqlCommand.Parameters.AddWithValue("@cantidad", miLibro.Cantidad);
            sqlCommand.Parameters.Add("@accion", SqlDbType.VarChar, 50).Value = miLibro.Accion;
            sqlCommand.Parameters["@accion"].Direction = ParameterDirection.InputOutput;
            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
            }
            Connection.Open();
            sqlCommand.ExecuteNonQuery();
            accion = sqlCommand.Parameters["@accion"].Value.ToString();
            Connection.Close();
            return accion;
        }
        
    }
}