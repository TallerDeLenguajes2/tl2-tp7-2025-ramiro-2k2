

using System.Reflection.Metadata.Ecma335;
using Microsoft.Data.Sqlite;

public class ProductoRepository 
{

    string chainConecction = "DataSource=Tienda.db";


    public List<Productos> GetAll()
    {
        string query = "SELECT * FROM productos";
        List<Productos> productos = [];
        var producto = new List<Productos>();
        using var conecction = new SqliteConnection(chainConecction);
        conecction.Open();
        var command = new SqliteCommand(query,conecction);
        using (SqliteDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var producto = new Producto
                {
                    Id = Convert.ToInt32(reader["id"])
                    Descripcion = reader["descripcion"].ToString
                    Precio = Convert.ToInt32(reader["precio"])
                }
                porductos.Add(producto);
            }
        }
        connection.close();
    }
    return productos;



}

 