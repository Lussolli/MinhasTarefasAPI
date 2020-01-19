using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MinhasTarefasAPI.Models;

namespace MinhasTarefasAPI.Database
{
    public class MinhasTarefasContexto : IdentityDbContext<ApplicationUser>
    {
        public MinhasTarefasContexto(DbContextOptions<MinhasTarefasContexto> options)
            : base(options)
        {
            
        }

        public DbSet<Tarefa> Tarefas { get; set; }
    }
}
