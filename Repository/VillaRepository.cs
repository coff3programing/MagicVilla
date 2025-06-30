using MagicVilla_API.Datos;
using MagicVilla_API.Models;
using MagicVilla_API.Repository.IRepository;

namespace MagicVilla_API.Repository;

public class VillaRepository : Repository<Villa>, IVillaRepository
{
    private readonly AplicationDBContext _db;
    public VillaRepository(AplicationDBContext db) : base(db) => _db = db;

    public async Task<Villa> Update(Villa entity)
    {
        DateTime now = DateTime.Now;
        DateOnly get_date = new DateOnly(now.Year, now.Month, now.Day);
        entity.UpdateDate = get_date;
        _db.Villas.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }
}
