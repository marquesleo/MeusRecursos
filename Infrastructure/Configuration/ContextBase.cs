using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Configuration
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
       public DbSet<Domain.Prioridades.Entities.Usuario> Usuarios { get; set; }
       public DbSet<Domain.Prioridades.Entities.Prioridade> Prioridades { get; set; }
       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(GetStringConectionConfig());

            }
            base.OnConfiguring(optionsBuilder);
        }

        private string GetStringConectionConfig()
        {
            return myDB.getStringConn().conexao;
          
        }
    }
}
