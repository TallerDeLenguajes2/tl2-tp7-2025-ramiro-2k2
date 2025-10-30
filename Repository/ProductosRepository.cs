using Microsoft.Data.Sqlite;
using TP07.Models;

namespace TP07.Repositorios;

public class ProductoRepository
{
    private readonly string connectionString = "Data Source=Tienda.db";

    public List<Productos> GetAll()
    {
        var productos = new List<Productos>();
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var command = new SqliteCommand("SELECT * FROM productos", connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            productos.Add(new Productos
            {
                IdProducto = Convert.ToInt32(reader["idProducto"]),
                Descripcion = reader["descripcion"].ToString() ?? "",
                Precio = Convert.ToInt32(reader["precio"])
            });
        }

        return productos;
    }

    public Productos? GetById(int id)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var command = new SqliteCommand("SELECT * FROM productos WHERE idProducto=@id", connection);
        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Productos
            {
                IdProducto = Convert.ToInt32(reader["idProducto"]),
                Descripcion = reader["descripcion"].ToString() ?? "",
                Precio = Convert.ToInt32(reader["precio"])
            };
        }

        return null;
    }

    public void Crear(Productos producto)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var command = new SqliteCommand("INSERT INTO productos (descripcion, precio) VALUES (@d, @p)", connection);
        command.Parameters.AddWithValue("@d", producto.Descripcion);
        command.Parameters.AddWithValue("@p", producto.Precio);
        command.ExecuteNonQuery();
    }

    public void Modificar(int id, Productos producto)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var command = new SqliteCommand("UPDATE productos SET descripcion=@d, precio=@p WHERE idProducto=@id", connection);
        command.Parameters.AddWithValue("@d", producto.Descripcion);
        command.Parameters.AddWithValue("@p", producto.Precio);
        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();
    }

    public bool Eliminar(int id)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var command = new SqliteCommand("DELETE FROM productos WHERE idProducto=@id", connection);
        command.Parameters.AddWithValue("@id", id);
        int rows = command.ExecuteNonQuery();

        return rows > 0;
    }
}
