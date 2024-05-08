using Domain.Prioridades.Entities;
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
        public DbSet<Domain.Prioridades.Entities.Categoria> Categorias { get; set; }
        public DbSet<Domain.Prioridades.Entities.ContadorDeSenha> ContadorDeSenhas { get; set; }

        public DbSet<Domain.Prioridades.Entities.RefreshToken> RefreshTokens { get; set; }
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
             //modelBuilder.Entity<Domain.Prioridades.Entities.Senha>()
             //   .HasOne<Domain.Prioridades.Entities.Usuario>(s => s.Usuario)
             //   .WithOne(ad => ad.senha )
             //   .HasForeignKey<Domain.Prioridades.Entities.Senha>(ad => ad.Usuario_Id);

             //     modelBuilder.Entity<Domain.Prioridades.Entities.Senha>()
             //       .HasIndex(b => b.Usuario_Id)
             //       .IsUnique(false);


            modelBuilder.Entity<Domain.Prioridades.Entities.Usuario>()
         .HasOne<Domain.Prioridades.Entities.Senha>(s => s.senha)
         .WithOne(ad => ad.Usuario)
         .HasForeignKey<Domain.Prioridades.Entities.Senha>(ad => ad.Usuario_Id)
         .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Domain.Prioridades.Entities.Senha>()
                   .HasIndex(b => b.Usuario_Id)
                   .IsUnique(false);



            modelBuilder.Entity<Domain.Prioridades.Entities.Usuario>()
          .HasOne<Domain.Prioridades.Entities.Prioridade>(s => s.prioridade)
          .WithOne(ad => ad.Usuario)
          .HasForeignKey<Domain.Prioridades.Entities.Prioridade>(ad => ad.Usuario_Id)
          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Domain.Prioridades.Entities.Prioridade>()
                   .HasIndex(b => b.Usuario_Id)
                   .IsUnique(false);



            //categoria - > usuario

            modelBuilder.Entity < Domain.Prioridades.Entities.Usuario>()
            .HasOne<Domain.Prioridades.Entities.Categoria>(s => s.categoria)
            .WithOne(ad => ad.Usuario)
            .HasForeignKey<Domain.Prioridades.Entities.Categoria>(ad => ad.Usuario_Id)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Domain.Prioridades.Entities.Categoria>()
                   .HasIndex(b => b.Usuario_Id)
                   .IsUnique(false);



            //senha -> Contador
            modelBuilder.Entity<Domain.Prioridades.Entities.Senha>()
           .HasOne<Domain.Prioridades.Entities.ContadorDeSenha>(s => s.ContadorDeSenha)
           .WithOne(ad => ad.Senha)
           .HasForeignKey<Domain.Prioridades.Entities.ContadorDeSenha>(ad => ad.SenhaId);
            
            modelBuilder.Entity<Domain.Prioridades.Entities.ContadorDeSenha>()
                   .HasIndex(b => b.SenhaId)
                   .IsUnique(false);



            modelBuilder.Entity<Senha>()
          .HasOne(b => b.Categoria)
          .WithMany(a => a.Senhas)
          .HasForeignKey(b => b.Categoria_Id)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Domain.Prioridades.Entities.Senha>()
                   .HasIndex(b => b.Categoria_Id)
                   .IsUnique(false);


        }
        private string GetStringConectionConfig()
        {
            return myDB.getStringConn().conexao;
          
        }
    }
}
