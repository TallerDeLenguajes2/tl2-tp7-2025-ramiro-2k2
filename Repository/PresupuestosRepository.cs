using Microsoft.Data.Sqlite;
using TP07.Models;

namespace TP07.Repositorios;

public class PresupuestoRepository
{
    private readonly string connectionString = "Data Source=Tienda.db";

    public void Crear(Presupuestos presupuesto)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var command = new SqliteCommand(
            "INSERT INTO presupuestos (nombreDestinatario, fechaCreacion) VALUES (@n, @f);",
            connection
        );
        command.Parameters.AddWithValue("@n", presupuesto.NombreDestinatario);
        command.Parameters.AddWithValue("@f", presupuesto.FechaCreacion);
        command.ExecuteNonQuery();
    }

    public List<Presupuestos> GetAll()
    {
        var presupuestos = new List<Presupuestos>();
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var command = new SqliteCommand("SELECT * FROM presupuestos", connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            presupuestos.Add(new Presupuestos
            {
                IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]),
                NombreDestinatario = reader["nombreDestinatario"].ToString() ?? "",
                FechaCreacion = reader["fechaCreacion"].ToString() ?? ""
            });
        }
        return presupuestos;
    }

    public Presupuestos? GetById(int id)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var presupuesto = new Presupuestos();
        var command = new SqliteCommand(
            "SELECT * FROM presupuestos WHERE idPresupuesto=@id",
            connection
        );
        command.Parameters.AddWithValue("@id", id);
        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            presupuesto.IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]);
            presupuesto.NombreDestinatario = reader["nombreDestinatario"].ToString() ?? "";
            presupuesto.FechaCreacion = reader["fechaCreacion"].ToString() ?? "";
        }
        else
            return null;

        // Leer detalle asociado
        var detalleCmd = new SqliteCommand(
            @"SELECT d.cantidad, p.idProducto, p.descripcion, p.precio
              FROM detallePresupuesto d
              JOIN productos p ON d.idProducto = p.idProducto
              WHERE d.idPresupuesto = @id",
            connection
        );
        detalleCmd.Parameters.AddWithValue("@id", id);
        using var readerDet = detalleCmd.ExecuteReader();
        while (readerDet.Read())
        {
            var prod = new Productos
            {
                IdProducto = Convert.ToInt32(readerDet["idProducto"]),
                Descripcion = readerDet["descripcion"].ToString() ?? "",
                Precio = Convert.ToInt32(readerDet["precio"])
            };
            presupuesto.Detalle.Add(new PresupuestoDetalle
            {
                Producto = prod,
                Cantidad = Convert.ToInt32(readerDet["cantidad"])
            });
        }

        return presupuesto;
    }

    public void AgregarProducto(int idPresupuesto, int idProducto, int cantidad)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var command = new SqliteCommand(
            "INSERT INTO detallePresupuesto (idPresupuesto, idProducto, cantidad) VALUES (@p, @i, @c)",
            connection
        );
        command.Parameters.AddWithValue("@p", idPresupuesto);
        command.Parameters.AddWithValue("@i", idProducto);
        command.Parameters.AddWithValue("@c", cantidad);
        command.ExecuteNonQuery();
    }

    public bool Eliminar(int id)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var cmdDetalle = new SqliteCommand("DELETE FROM detallePresupuesto WHERE idPresupuesto=@id", connection);
        cmdDetalle.Parameters.AddWithValue("@id", id);
        cmdDetalle.ExecuteNonQuery();

        var cmdPresupuesto = new SqliteCommand("DELETE FROM presupuestos WHERE idPresupuesto=@id", connection);
        cmdPresupuesto.Parameters.AddWithValue("@id", id);
        int rows = cmdPresupuesto.ExecuteNonQuery();

        return rows > 0;
    }
}
