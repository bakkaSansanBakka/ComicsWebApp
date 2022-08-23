namespace ComicsWebApp.Data.Repositories
{
    public interface IRepository<T>
        where T: class
    {
        List<T> GetAll();
        T GetById(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
