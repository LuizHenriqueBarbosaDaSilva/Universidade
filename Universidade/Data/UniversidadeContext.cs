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
                new Aluno { Id = 1, Nome = "Maria Lopes", Matricula = 202314593, Data = DateOnly.Parse("21/9/2023")},
                new Aluno { Id = 2, Nome = "Joao Carlos", Matricula = 202314956, Data = DateOnly.Parse("22/10/2023")}
                );
            modelBuilder.Entity<Professor>().HasData(
                new Professor { Id = 1, Nome = "Jon Cleber", Matricula = 20231214, Data = DateOnly.Parse("20/01/2013") },
                new Professor { Id = 1, Nome = "Leo John", Matricula = 20231215, Data = DateOnly.Parse("20/01/2013") }
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
