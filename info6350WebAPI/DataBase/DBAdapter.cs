namespace info6350WebAPI;

public class DBAdapter<T> : IDB<T> where T : IModel, new()
{
    public T? Get(long id)
    {
        return DB<T>.Get(id);
    }

    public IEnumerable<T> GetAll()
    {
        return DB<T>.GetAll();
    }

    public void Insert(T model)
    {
        DB<T>.Insert(model);
    }

    public void Update(T model)
    {
        DB<T>.Update(model);
    }

    public void Delete(long id)
    {
        DB<T>.Delete(id);
    }
}
