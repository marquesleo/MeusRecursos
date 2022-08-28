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
        //dotnet ef migrations --project .\Infrastructure  --startup-project .\MinhasPrioridades add EmailColumn
        //dotnet ef database --project.\Infrastructure --startup-project.\MinhasPrioridades update
        //dotnet ef database update SenhaTable
       public DbSet<Domain.Prioridades.Entities.Usuario> Usuarios { get; set; }
       public DbSet<Domain.Prioridades.Entities.Prioridade> Prioridades { get; set; }
       public DbSet<Domain.Prioridades.Entities.Senha> Senhas { get; set; }
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
             modelBuilder.Entity<Domain.Prioridades.Entities.Senha>()
                .HasOne<Domain.Prioridades.Entities.Usuario>(s => s.Usuario)
                .WithOne(ad => ad.senha )
                .HasForeignKey<Domain.Prioridades.Entities.Senha>(ad => ad.Usuario_Id);

                  modelBuilder.Entity<Domain.Prioridades.Entities.Senha>()
                    .HasIndex(b => b.Usuario_Id)
                    .IsUnique(false);
        }
        private string GetStringConectionConfig()
        {
            return myDB.getStringConn().conexao;
          
        }
    }
}
