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
	}
}
