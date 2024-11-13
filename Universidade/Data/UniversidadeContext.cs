using Universidade.Models;
using Microsoft.EntityFrameworkCore;

namespace Universidade.Data
{
    public class UniversidadeContext : DbContext
    {
        public UniversidadeContext(DbContextOptions<UniversidadeContext> options) : base(options)
        { }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>().HasData(
                new Aluno { Id = 1, Nome = "John Doe", Matricula = 0000002, Data = DateOnly.Parse("20/10/0002")},
                new Aluno { Id = 2, Nome = "Jane Doe", Matricula = 0000003, Data = DateOnly.Parse("20/10/0003")}
                );
            modelBuilder.Entity<Professor>().HasData(
                new Professor { Id = 1, Nome = "Jesus", Matricula = 0000001, Data = DateOnly.Parse("01/01/001") }
                );

            modelBuilder.Entity<Disciplina>().HasData(
                new Disciplina { Id = 1, Nome = "Profeta" , Descricao = "Traga as palavras", Ativo = true, DataRegistro = DateTime.Now, ProfessorId = 1},
                new Disciplina { Id = 2, Nome = "Testemunha", Descricao = "Testemunhe o mundo", Ativo = true,DataRegistro = DateTime.Now, ProfessorId = 1}
                );

            modelBuilder.Entity<Aluno>().HasMany(a => a.Disciplinas).WithMany(d => d.Alunos).UsingEntity(ad => ad.HasData(
                    new { DisciplinasId = 1, AlunosId = 2 },
                    new { DisciplinasId = 2, AlunosId = 1 }
                   ));
        }

    }
}
