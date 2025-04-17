using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<UserEntity>(options)
{

    public DbSet<StatusEntity> Statuses { get; set; }
    public DbSet<ClientEntity> Clients { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<MemberEntity> Members { get; set; }

    public DbSet <UserEntity> Users { get; set; } 


}
