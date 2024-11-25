﻿using System.ComponentModel.DataAnnotations;

namespace Universidade.Models
{
	public class Aluno
	{
		public Aluno() 
		{
			Disciplinas = new List<Disciplina>();
		}
		public int Id { get; set; }
		public string Nome { get; set; }
		public int Matricula { get; set; }
        [Display(Name = "Data de efitivação")]
        public DateOnly Data { get; set; }

        public List<Disciplina> Disciplinas { get; set; }


    }
}
