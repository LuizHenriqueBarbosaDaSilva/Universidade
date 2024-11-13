namespace Universidade.Models
{
	public class Aluno
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public int Matricula { get; set; }
		public DateTime Data { get; set; }

		public Disciplina? Disciplina { get; set; }

	}
}
