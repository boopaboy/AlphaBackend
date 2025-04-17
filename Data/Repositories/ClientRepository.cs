using Data.Contexts;
using Data.Entities;

namespace Data.Repositories;

public class ClientRepository(DataContext context) : BaseRepository<ClientEntity>(context)
{
}
