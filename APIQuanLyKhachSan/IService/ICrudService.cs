namespace APIQuanLyKhachSan.IService
{
    public interface ICrudService<TEntity, in TKey>//TKe = Guid, TEntity = Entity
    {
        Task<IQueryable<TEntity>> GetAll();
        Task<TEntity> GetById(TKey id);
        Task<TEntity> Create(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> DeleteById(TKey id);
    }
}
