using SQLite;

// ReSharper disable InconsistentNaming

namespace info6350WebAPI;

public static class DataBaseHandler
{
    private const string CONNECTION_STRING = "info6350.db";

    private static readonly Lazy<SQLiteConnection> Lazy = new(CreateDataBase);

    public static SQLiteConnection Conn => Lazy.Value;
    
    private static SQLiteConnection CreateDataBase()
    {
        var connection = new SQLiteConnection(CONNECTION_STRING);
        connection.CreateTable<Company>();
        connection.CreateTable<Product>();
        connection.CreateTable<ProductType>();
        connection.CreateTable<ProductPost>();
        connection.CreateTable<Order>();
        return connection;
    }
}