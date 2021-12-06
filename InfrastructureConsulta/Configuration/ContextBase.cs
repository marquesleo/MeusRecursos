using Domain.Consulta.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Consulta.Configuration
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
       public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Acesso> Acessos { get; set; }

        public DbSet<Empresa> Empresas { get; set; }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Psicologa> Psicologas { get; set; }

        public DbSet<Domain.Consulta.Entities.Consulta> Consultas { get; set; }


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
            modelBuilder.Entity<Usuario>()
                .HasOne<Acesso>(s => s.Acesso)
                .WithOne(ad => ad.Usuario)
                .HasForeignKey<Acesso>(ad => ad.Usuario_Id);

            modelBuilder.Entity<Empresa>()
                .HasOne<Acesso>(s => s.Acesso)
                .WithOne(ad => ad.Empresa)
                .HasForeignKey<Acesso>(ad => ad.Empresa_Id);

            modelBuilder.Entity<Acesso>()
               .HasOne<Paciente>(s => s.Paciente)
               .WithOne(ad => ad.Acesso)
               .HasForeignKey<Paciente>(ad => ad.Acesso_Id);


            modelBuilder.Entity<Acesso>()
              .HasOne<Psicologa>(s => s.Psicologa)
              .WithOne(ad => ad.Acesso)
              .HasForeignKey<Psicologa>(ad => ad.Acesso_Id);


            modelBuilder.Entity<Psicologa>()
           .HasOne<Domain.Consulta.Entities.Consulta>(s => s.Consulta)
           .WithOne(ad => ad.Psicologa)
           .HasForeignKey<Domain.Consulta.Entities.Consulta>(ad => ad.Psicologa_Id);

            modelBuilder.Entity<Paciente>()
            .HasOne<Domain.Consulta.Entities.Consulta>(s => s.Consulta)
            .WithOne(ad => ad.Paciente)
            .HasForeignKey<Domain.Consulta.Entities.Consulta>(ad => ad.Paciente_Id);

        }


        private string GetStringConectionConfig()
        {
            return myDB.getStringConn().conexao;
          
        }
    }
}
