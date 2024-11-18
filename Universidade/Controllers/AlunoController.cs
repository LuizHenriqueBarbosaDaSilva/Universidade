using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Universidade.Data;
using Universidade.Models;

namespace Universidade.Controllers
{
    public class AlunoController : Controller
    {
        private readonly UniversidadeContext _context;

        public AlunoController(UniversidadeContext context)
        {
            _context = context;
        }

        //Metodo -> GET: Aluno
        public async Task<IActionResult> Index()
        {
            var alunoContext = _context.Alunos.Include(a => a.Disciplinas).ThenInclude(d => d.Professor); // Inclui as disciplinas associadas ao aluno e os professores de cada disciplina

            return View(await alunoContext.ToListAsync());
        }

        //Metodo -> GET: Aluno/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) 
            {
                return NotFound(); // Retorna a pagina 404 (NotFound)
            }

            var aluno = await _context.Alunos.Include(a => a.Disciplinas).ThenInclude(d => d.Professor).FirstOrDefaultAsync(a => a.Id == id);
            if (aluno == null)
            {
                return NotFound(); // Retorna a pagina 404 (NotFound)
            }

            return View(aluno);
        }

        //Metodo -> GET: Aluno/Create
        [HttpGet("Aluno/Create")]
        public IActionResult Create()
        {
            var disciplinas = _context.Disciplinas.Select(d => new { d.Id, d.Nome }).ToList();
            ViewBag.Disciplinas = disciplinas;
            return View();
        }

        //Metodo -> POST: Aluno/Create
        [HttpPost("Aluno/Create/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Aluno aluno, int[] disciplinasItens)
        {
            if (ModelState.IsValid)
            {
                _context.Alunos.Add(aluno); // Atualiza os Alunos no banco com as novas associações   
                await _context.SaveChangesAsync(); // Espera para as mudanças estarem assincronas

                // Associar as disciplinas selecionadas
                if (disciplinasItens != null && disciplinasItens.Any()) 
                {
                    var disciplinas = _context.Disciplinas
                        .Where(d => disciplinasItens.Contains(d.Id))
                        .ToList(); // Busca no context os conteudos de Disciplina nos Ids e faz uma lista

                    aluno.Disciplinas = disciplinas;
                    _context.Update(aluno);            // Atualiza a lista nas disciplinas no banco com as novas associações    
                    await _context.SaveChangesAsync(); // Espera para as mudanças estarem assincronas
                }

                return RedirectToAction(nameof(Index)); // Redireciona para a página de listagem (Index)
            }

            ViewBag.Disciplinas = new SelectList(_context.Disciplinas, "Id", "Nome");
            return View(aluno); 
        }

        //Metodo -> GET: Aluno/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            // Se o ID não foi fornecido, retorna a página de erro 404 (NotFound)
            if (id == null)
            {
                return NotFound(); // Retorna a pagina 404 (NotFound)
            }

            // Carrega o aluno e as disciplinas associadas. Inclui a lista disciplinas para apresentar as disciplinas no front end alem de passar a PK do aluno
            var aluno = await _context.Alunos.Include(a => a.Disciplinas).FirstOrDefaultAsync(a => a.Id == id);

            // Se o aluno não foi encontrado, retorna a página de erro 404 (NotFound)
            if (aluno == null)
            {
                return NotFound(); // Retorna a pagina 404 (NotFound)
            }

            // Prepara uma lista de todas as disciplinas, marcando quais estão associadas ao aluno
            var todasDisciplinas = await _context.Disciplinas.ToListAsync();
            var disciplinasMarcadas = todasDisciplinas.Select(d => new
            {
                d.Id, // ID da disciplina
                d.Nome, // Nome da disciplina
                Selecionado = aluno.Disciplinas.Any(ad => ad.Id == d.Id) // Verifica se a disciplina está associada ao aluno
            }).ToList(); // Faz uma lista com as disciplinas achadas

            ViewBag.Disciplinas = disciplinasMarcadas; // Passa a lista de disciplinas para a View através de ViewBag
            return View(aluno); // Retorna o aluno para ser exibido na View
        }

        //Metodo -> POST: Aluno/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Aluno aluno, int[] disciplinasSelecionadas)
        {
            if (id != aluno.Id) // Se o ID do aluno recebido não corresponde ao ID do formulário, retorna erro 404 (NotFound)
            {
                return NotFound(); // Retorna a pagina 404 (NotFound)
            }

            if (ModelState.IsValid) // Verifica se os dados do formulário são válidos
            {
                try
                {
                    // Atualiza os dados básicos do aluno
                    _context.Update(aluno); 
                    await _context.SaveChangesAsync();

                    // Busca o aluno no banco para atualizar as disciplinas associadas.
                    var alunoExistente = await _context.Alunos.Include(a => a.Disciplinas).FirstOrDefaultAsync(a => a.Id == aluno.Id);                                                                                                                                      

                    if (alunoExistente != null) // Se o aluno existir no banco processa as associações!
                    {
                        // Remove as disciplinas que ja estavam antigamente
                        alunoExistente.Disciplinas.Clear();

                        // Caso existam disciplinas selecionadas para o Aluno adiciona as novas
                        if (disciplinasSelecionadas != null && disciplinasSelecionadas.Any())
                        {
                            // Busca as disciplinas selecionadas no banco e cria uma lista nomeada disciplinasListagem
                            var disciplinasListagem = _context.Disciplinas.Where(d => disciplinasSelecionadas.Contains(d.Id)).ToList();
                            // Associa as disciplinas ao alunoExistente
                            alunoExistente.Disciplinas = disciplinasListagem;
                        }
                        // Atualiza a tabela Alunos no banco com as novas associações
                        _context.Update(alunoExistente);
                        await _context.SaveChangesAsync(); // Espera para as mudanças estarem assincronas
                    }
                } 
                catch (DbUpdateConcurrencyException) // Lida com conflitos ao atualizar o banco de dados
                {
                    if (!AlunoExists(aluno.Id)) // Verifica se o aluno ainda existe no banco antes de lançar o erro!
                    {
                        return NotFound(); // Retorna a pagina 404 (NotFound)
                    }
                    else
                    {
                        throw; // Mostra a exeção novamente
                    }
                }

                return RedirectToAction(nameof(Index)); // Redireciona para a página Index após o sucesso da edição do aluno 
            }

            // Caso a validação falhe, prepara os dados novamente para o formulário
            var todasDisciplinas = await _context.Disciplinas.ToListAsync();
            var disciplinasMarcadas = todasDisciplinas.Select(d => new
            {
                d.Id, // ID da disciplina
                d.Nome, // Nome da disciplina
                Selecionado = aluno.Disciplinas.Any(ad => ad.Id == d.Id) // Verifica se a disciplina está associada ao aluno
            }).ToList(); // Faz uma lista com as disciplinas achadas

            ViewBag.Disciplinas = disciplinasMarcadas; // Passa as disciplinas para a View através de ViewBag
            return View(aluno); // Retorna o aluno com os dados preenchidos
        }

        //Metodo -> GET: Aluno/Delete/5
        public async Task<IActionResult> Delete(int? id) // O Delete recebe o Id do iten que voce quer deletar
        {
            if (id == null) // Verifica se o ID é nulo
            {
                return NotFound(); // Retorna a pagina 404 (NotFound)
            }

            var aluno = await _context.Alunos
                .FirstOrDefaultAsync(a => a.Id == id); // Busca o aluno no contexto pelo ID fornecido
            if (aluno == null) // Verifica se o aluno existe    
            {
                return NotFound(); // Retorna a pagina 404 (NotFound)
            }

            return View(aluno); // Retorna a visão com os detalhes do aluno para confirmação
        }

        //Metodo -> POST: Aluno/Delete/5
        [HttpPost, ActionName("Delete")] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) // recebe o Id que foi desejado
        {
            var aluno = await _context.Alunos.FindAsync(id); // Procura o aluno no contexto pelo ID
            if (aluno != null) // Confere se o aluno não existe, se for diferente entra na condição
            {
                _context.Alunos.Remove(aluno); // Remove o registro do aluno do contexto
            }

            await _context.SaveChangesAsync(); // Salva as mudanças no banco de dados
            return RedirectToAction(nameof(Index)); // Redireciona para a página de listagem (Index)
        }

        private bool AlunoExists(int id)
        {
            // Verifica se existe algum aluno no context cuja propriedade Id seja igual ao valor fornecido
            return _context.Alunos.Any(e => e.Id == id); 
        }
    }
}
