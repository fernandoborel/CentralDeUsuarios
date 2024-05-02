namespace CentralDeUsuarios.Domain.Core;

/// <summary>
/// Interface para abstração dos repositórios
/// </summary>
public interface IBaseRepository<TEntity, TKey>
    where TEntity : class
{
    void Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);

    List<TEntity> GetAll();
    TEntity GetById(TKey id);
}
