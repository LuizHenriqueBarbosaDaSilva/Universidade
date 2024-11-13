using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Universidade.Models
{
	public class Disciplina
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public string Descricao { get; set; }

        public bool Ativo { get; set; }

        [Display(Name = "Data Registro")]
        public DateTime DataRegistro { get; set; }

        [Display(Name = "Nome do Professor")]
        public int ProfessorId { get; set; }
        public Professor? Professor { get; set; }

        // Conexão de muitos para muitos com Aluno por meio de uma junção
        public List<Aluno> Alunos { get; set; } 

        public Disciplina()
        {
            DataRegistro = DateTime.Now;
            Alunos = new List<Aluno>();
        }
    }
}