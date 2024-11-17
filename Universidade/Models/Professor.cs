using System.ComponentModel.DataAnnotations;

namespace Universidade.Models
{
	public class Professor
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public int Matricula { get; set; }
        [Display(Name = "Data de adimissão")]
        public DateOnly Data { get; set; }
	}
}
