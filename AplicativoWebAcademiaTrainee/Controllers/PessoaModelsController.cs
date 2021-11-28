using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AplicativoWebAcademiaTrainee.Data;
using AplicativoWebAcademiaTrainee.Models;

namespace AplicativoWebAcademiaTrainee.Controllers
{
    public class PessoaModelsController : Controller
    {
        private readonly AplicativoWebAcademiaTraineeContext _context;

        public PessoaModelsController(AplicativoWebAcademiaTraineeContext context)
        {
            _context = context;
        }

        // GET: PessoaModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.PessoaModel.ToListAsync());
        }

        // GET: PessoaModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (pessoaModel == null)
            {
                return NotFound();
            }

            return View(pessoaModel);
        }

        // GET: PessoaModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PessoaModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Nome,Email,DataNascimento,QuantidadeFilhos,Salario,Situacao")] PessoaModel pessoaModel)
        {
            pessoaModel.Situacao = "Ativo";
            // REGRAS DE INCLUSÃO
            var pessoasEmail = _context.PessoaModel.Where(x => x.Email.Equals(pessoaModel.Email) && x.Codigo != pessoaModel.Codigo);
            if (pessoasEmail.Count() > 0)
            {
                ModelState.AddModelError("Regra de Negócio", "E-mail já cadastrado");
                return View(pessoaModel);
            }
            if (pessoaModel.DataNascimento < new DateTime(1990, 1, 1))
            {
                ModelState.AddModelError("Regra de Negócio", "A data de nascimento deve ser superior a 01/01/1990");
                return View(pessoaModel);
            }
            if (pessoaModel.Salario < 1200 || pessoaModel.Salario > 13000)
            {
                ModelState.AddModelError("Regra de Negócio", "O salário deve estar entre R$ 1.200 e R$ 13.000");
                return View(pessoaModel);
            }
            if (pessoaModel.QuantidadeFilhos < 0)
            {
                ModelState.AddModelError("Regra de Negócio", "A quantidade de filhos deve ser superior ou igual a 0");
                return View(pessoaModel);
            }
            else
            {
                _context.Add(pessoaModel);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: PessoaModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel.FindAsync(id);
            if (pessoaModel == null)
            {
                return NotFound();
            }
            return View(pessoaModel);
        }

        // POST: PessoaModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Nome,Email,DataNascimento,QuantidadeFilhos,Salario,Situacao")] PessoaModel pessoaModel)
        {
            if (id != pessoaModel.Codigo)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                // REGRAS DE EDIÇÃO
                var pessoasEmail = _context.PessoaModel.Where(x => x.Email.Equals(pessoaModel.Email) && x.Codigo != pessoaModel.Codigo);
                if (pessoasEmail.Count() > 0)
                {
                    ModelState.AddModelError("Regra de Negócio", "E-mail já cadastrado");
                    return View(pessoaModel);
                }
                if (pessoaModel.DataNascimento < new DateTime(1990, 1, 1))
                {
                    ModelState.AddModelError("Regra de Negócio", "A data de nascimento deve ser superior a 01/01/1990");
                    return View(pessoaModel);
                }
                if (pessoaModel.Salario < 1200 || pessoaModel.Salario > 13000)
                {
                    ModelState.AddModelError("Regra de Negócio", "O salário deve estar entre R$ 1.200 e R$ 13.000");
                    return View(pessoaModel);
                }
                if (pessoaModel.QuantidadeFilhos < 0)
                {
                    ModelState.AddModelError("Regra de Negócio", "A quantidade de filhos deve ser superior ou igual a 0");
                    return View(pessoaModel);
                }
                if (pessoaModel.Situacao.Equals("Inativo"))
                {
                    ModelState.AddModelError("Regra de Negócio", "Não é possível editar uma pessoa na situação 'Inativo'");
                    return View(pessoaModel);
                }
                else
                {
                    try
                    {
                        _context.Update(pessoaModel);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PessoaModelExists(pessoaModel.Codigo))
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
            }
            return View(pessoaModel);
        }

        // GET: PessoaModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (pessoaModel == null)
            {
                return NotFound();
            }

            return View(pessoaModel);
        }

        // POST: PessoaModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoaModel = await _context.PessoaModel.FindAsync(id);
            // REGRA DE EXCLUSÃO
            if (pessoaModel.Situacao.Equals("Ativo"))
            {
                ModelState.AddModelError("Regra de Negócio", "Não é possível excluir uma pessoa na situação 'Ativo'");
                return View(pessoaModel);
            }
            else
            {
                _context.PessoaModel.Remove(pessoaModel);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: PessoaModels/ChangeStatus/5
        [HttpGet]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            var pessoaModel = await _context.PessoaModel.FindAsync(id);
            if (pessoaModel.Situacao.Equals("Ativo"))
            {
                pessoaModel.Situacao = "Inativo";
            }
            else
            {
                pessoaModel.Situacao = "Ativo";
            }
            _context.Update(pessoaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaModelExists(int id)
        {
            return _context.PessoaModel.Any(e => e.Codigo == id);
        }
    }
}
