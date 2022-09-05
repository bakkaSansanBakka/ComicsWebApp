namespace ComicsWebApp.Data.Repositories
{
    public interface IUnitOfWork
    {
        ComicsRepository ComicsRepository { get; }
        ComicsGenresRepository ComicsGenresRepository { get; }
        ComicsPagesRepository ComicsPagesRepository { get; }
        void Save();
        Task SaveAsync();
    }
}
