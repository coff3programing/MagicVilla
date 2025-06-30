
using System.Linq.Expressions;
using MagicVilla_API.Datos;
using MagicVilla_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AplicationDBContext _db;
    internal DbSet<T> dbSet;

    public Repository(AplicationDBContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        dbSet = _db.Set<T>();
    }

    public async Task Create(T entity)
    {
        await dbSet.AddAsync(entity);
        await Save();
    }

    public async Task<T> Get(Expression<Func<T, bool>> filter = null, bool tracked = true)
    {
        IQueryable<T> query = dbSet;
        if (!tracked) query = query.AsNoTracking();
        if (filter != null) query = query.Where(filter);
        return await query.FirstOrDefaultAsync();
    }

    public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null)
    {
        IQueryable<T> query = dbSet;
        if (filter != null) query = query.Where(filter);
        return await query.ToListAsync();
    }

    public async Task Remove(T entity)
    {
        dbSet.Remove(entity);
        await Save();
    }

    public async Task Save() => await _db.SaveChangesAsync();
}
