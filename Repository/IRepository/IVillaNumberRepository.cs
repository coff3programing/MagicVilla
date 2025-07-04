using MagicVilla_API.Models;

namespace MagicVilla_API.Repository.IRepository;

public interface IVillaNumberRepository : IRepository<VillaNumber>
{
    Task<VillaNumber> Update(VillaNumber entity);
}
