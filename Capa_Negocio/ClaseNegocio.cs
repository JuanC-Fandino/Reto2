using System;
using System.Data;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class ClaseNegocio
    {
        ClaseDatos _claseDatos = new ClaseDatos();

        public DataTable ListarLibros()
        {
            return _claseDatos.ListarLibrosDataTable();
        }
        public DataTable BuscarLibro(Libro miLibro)
        {
            return _claseDatos.BuscarLibrosDataTable(miLibro);
        }
        public string MantenimientoLibros(Libro miLibro)
        {
            return _claseDatos.MantenimientoLibrosDataTable(miLibro);
        }
    }
}