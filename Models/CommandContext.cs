using Microsoft.EntityFrameworkCore;

namespace CmdAPI.Model
{
    public class CommandContext: DbContext
    {
        public CommandContext(DbContextOptions<CommandContext> options) : base(options)
        {

        }

        public DbSet<Commands> CommandItems {get; set;}

    }
}