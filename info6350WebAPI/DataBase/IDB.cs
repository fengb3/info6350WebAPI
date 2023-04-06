namespace info6350WebAPI;

public interface IDB<T> where T : IModel, new()
{
    T?             Get(long id);
    IEnumerable<T> GetAll();
    void           Insert(T    model);
    void           Update(T    model);
    void           Delete(long id);
    bool TryGet(long id, out T? model);
}
