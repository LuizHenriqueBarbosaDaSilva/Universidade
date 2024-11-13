namespace Universidade.Models
{
	public class Disciplina
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public string Descricao { get; set; }
		public bool Ativo { get; set; }
		public int ProfessorId { get; set; }

		public int AlunoId { get; set; }

		public Aluno? Aluno { get; set; }
        public Professor? Professor { get; set; }
    }
}