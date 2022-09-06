
using Domain.RDI.Entidade;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.RDI.Configuration
{
    public class ContextBase :DbContext
    {

        private MyDB myDB;
        public ContextBase(DbContextOptions<ContextBase> options,MyDB myDB) : base(options) {

           this.myDB = myDB;
        }


        //dotnet tool uninstall --global dotnet-ef
        //dotnet tool install --global dotnet-ef
        //dotnet ef(This is only to check that "DOTNET EF" is installed correctly.)
        //dotnet ef migrations --project YourProject.Data --startup-project YourProject.UI add InitialCreate
        //dotnet ef database --project YourProject.Data --startup-project YourProject.UI update
        //dotnet ef migrations --project .\Infrastructure  --startup-project .\MinhasPrioridades add EmailColumn
        //dotnet ef database --project.\Infrastructure --startup-project.\MinhasPrioridades update
       public DbSet<Card> Card { get; set; }
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(GetStringConectionConfig());

            }
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }


        private string GetStringConectionConfig()
        {
            return myDB.getStringConn().conexao;
          
        }
    }
}
