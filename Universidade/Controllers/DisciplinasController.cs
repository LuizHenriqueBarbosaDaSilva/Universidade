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
    public class DisciplinasController : Controller
    {
        private readonly UniversidadeContext _context;

        public DisciplinasController(UniversidadeContext context)
        {
            _context = context;
        }

        // GET: Disciplinas
        public async Task<IActionResult> Index()
        {
            var universidadeContext = _context.Disciplinas.Include(d => d.Aluno).Include(d => d.Professor);
            return View(await universidadeContext.ToListAsync());
        }

        // GET: Disciplinas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplina = await _context.Disciplinas
                .Include(d => d.Aluno)
                .Include(d => d.Professor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disciplina == null)
            {
                return NotFound();
            }

            return View(disciplina);
        }

        // GET: Disciplinas/Create
        public IActionResult Create()
        {
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Id");
            ViewData["ProfessorId"] = new SelectList(_context.Professores, "Id", "Id");
            return View();
        }

        // POST: Disciplinas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,Ativo,ProfessorId,AlunoId")] Disciplina disciplina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disciplina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Id", disciplina.AlunoId);
            ViewData["ProfessorId"] = new SelectList(_context.Professores, "Id", "Id", disciplina.ProfessorId);
            return View(disciplina);
        }

        // GET: Disciplinas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplina = await _context.Disciplinas.FindAsync(id);
            if (disciplina == null)
            {
                return NotFound();
            }
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Id", disciplina.AlunoId);
            ViewData["ProfessorId"] = new SelectList(_context.Professores, "Id", "Id", disciplina.ProfessorId);
            return View(disciplina);
        }

        // POST: Disciplinas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,Ativo,ProfessorId,AlunoId")] Disciplina disciplina)
        {
            if (id != disciplina.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disciplina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisciplinaExists(disciplina.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Id", disciplina.AlunoId);
            ViewData["ProfessorId"] = new SelectList(_context.Professores, "Id", "Id", disciplina.ProfessorId);
            return View(disciplina);
        }

        // GET: Disciplinas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplina = await _context.Disciplinas
                .Include(d => d.Aluno)
                .Include(d => d.Professor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disciplina == null)
            {
                return NotFound();
            }

            return View(disciplina);
        }

        // POST: Disciplinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disciplina = await _context.Disciplinas.FindAsync(id);
            if (disciplina != null)
            {
                _context.Disciplinas.Remove(disciplina);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisciplinaExists(int id)
        {
            return _context.Disciplinas.Any(e => e.Id == id);
        }
    }
}
