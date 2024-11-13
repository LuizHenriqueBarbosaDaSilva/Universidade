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

        // GET: Aluno
        public async Task<IActionResult> Index()
        {
            var alunoContext = _context.Alunos.Include(a => a.Disciplinas).ThenInclude(d => d.Professor); // Inclui as disciplinas associadas ao aluno e os professores de cada disciplina

            return View(await alunoContext.ToListAsync());
        }

        // GET: Aluno/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Alunos.Include(a => a.Disciplinas).ThenInclude(d => d.Professor).FirstOrDefaultAsync(a => a.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // GET: Aluno/Create
        [HttpGet("Aluno/Create")]
        public IActionResult Create()
        {
            var disciplinas = _context.Disciplinas.Select(d => new { d.Id, d.Nome }).ToList();
            ViewBag.Disciplinas = disciplinas;
            return View();
        }

        // POST: Aluno/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Aluno/Create/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Aluno aluno, int[] disciplinasSelecionadas)
        {
            if (ModelState.IsValid)
            {
                // Adicionar o aluno
                _context.Alunos.Add(aluno);
                await _context.SaveChangesAsync();

                // Associar as disciplinas selecionadas
                if (disciplinasSelecionadas != null && disciplinasSelecionadas.Any())
                {
                    var disciplinas = _context.Disciplinas
                        .Where(d => disciplinasSelecionadas.Contains(d.Id))
                        .ToList();

                    aluno.Disciplinas = disciplinas;
                    _context.Update(aluno);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Disciplinas = new SelectList(_context.Disciplinas, "Id", "Nome");
            return View(aluno); 
        }

        // GET: Aluno/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            // Se o ID não foi fornecido, retorna a página de erro 404 (NotFound)
            if (id == null)
            {
                return NotFound();
            }

            // Carrega o aluno e as disciplinas associadas. Inclui a lista disciplinas para apresentar as disciplinas no front end alem de passar a PK do aluno
            var aluno = await _context.Alunos.Include(a => a.Disciplinas).FirstOrDefaultAsync(a => a.Id == id);

            // Se o aluno não foi encontrado, retorna a página de erro 404 (NotFound)
            if (aluno == null)
            {
                return NotFound();
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

        // POST: Aluno/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Aluno aluno, int[] disciplinasSelecionadas)
        {
            if (id != aluno.Id) // Se o ID do aluno recebido não corresponde ao ID do formulário, retorna erro 404 (NotFound)
            {
                return NotFound();
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

        // GET: Aluno/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Alunos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // POST: Aluno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno != null)
            {
                _context.Alunos.Remove(aluno);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlunoExists(int id)
        {
            return _context.Alunos.Any(e => e.Id == id);
        }
    }
}
