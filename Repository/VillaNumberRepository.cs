using MagicVilla_API.Datos;
using MagicVilla_API.Models;
using MagicVilla_API.Repository.IRepository;

namespace MagicVilla_API.Repository;

public class NumberVillaRepository : Repository<VillaNumber>, IVillaNumberRepository
{
    private readonly AplicationDBContext _db;
    public NumberVillaRepository(AplicationDBContext db) : base(db) => _db = db;

    public async Task<VillaNumber> Update(VillaNumber entity)
    {
        DateTime now = DateTime.Now;
        DateOnly get_date = new DateOnly(now.Year, now.Month, now.Day);
        entity.UpdateDate = get_date;
        _db.villaNumbers.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }
}
