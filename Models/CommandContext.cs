using Microsoft.EntityFrameworkCore;

namespace API_Sandbox.Model
{
    public class CommandContext: DbContext
    {
        public CommandContext(DbContextOptions<CommandContext> options) : base(options)
        {

        }

        public DbSet<Commands> CommandItems {get; set;}

    }
}