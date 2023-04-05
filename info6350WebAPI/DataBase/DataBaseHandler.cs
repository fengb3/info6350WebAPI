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

// public static class DB
// {
//     public static T Get<T>(long id) where T : IModel, new()
//     {
//         return DataBaseHandler.Conn.Find<T>(id);
//     }
//     
//     public static IEnumerable<T> GetAll<T>() where T : IModel, new()
//     {
//         return DataBaseHandler.Conn.Table<T>();
//     }
//     
//     public static void Insert<T>(T model) where T : IModel, new()
//     {
//         DataBaseHandler.Conn.Insert(model);
//     }
//     
//     public static void Update<T>(T model) where T : IModel, new()
//     {
//         DataBaseHandler.Conn.Update(model);
//     }
//     
//     public static void Delete<T>(long id) where T : IModel, new()
//     {
//         DataBaseHandler.Conn.Delete<T>(id);
//     }
// }

public static class DB<T> where T : IModel, new()
{
    public static T? Get(long id)
    {
        return DataBaseHandler.Conn.Find<T>(id);
    }
    
    public static bool TryGet(long id, out T? model)
    {
        model = DataBaseHandler.Conn.Find<T>(id);
        
        return model != null;
    }
    
    public static IEnumerable<T> GetAll()
    {
        return DataBaseHandler.Conn.Table<T>();
    }
    
    public static void Insert(T model)
    {
        DataBaseHandler.Conn.Insert(model);
    }
    
    public static void Update(T model)
    {
        DataBaseHandler.Conn.Update(model);
    }
    
    public static void Delete(long id)
    {
        DataBaseHandler.Conn.Delete<T>(id);
    }
}