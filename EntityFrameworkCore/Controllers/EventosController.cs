using EntityFrameworkCore.Data;
using EntityFrameworkCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Controllers
{
    public class EventosController : Controller
    {
        private readonly EventoContext _context;

        public EventosController(EventoContext context)
        {
            _context = context;
        }

        // GET: Eventos
        /// <summary>
        /// Exibe todos os eventos cadastrados.
        /// </summary>
        /// <response code="200">Todos os eventos exibido com sucesso.</response>
        /// <response code="500">Opa! Não foi possível exibir todos os eventos agora.</response>
        [HttpGet("Eventos/GetAll")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Eventos.ToListAsync());
        }

        /// <summary>
        /// Exibe os detalhes do evento selecionado.
        /// </summary>
        /// <param name="id">Id do evento a editar</param>
        /// <response code="200">Evento exibido com sucesso.</response>
        /// <response code="404">Evento não encontrado.</response>
        /// <response code="500">Opa! Não foi possível exibir os detalhes do evento agora.</response>
        [HttpGet("Eventos/Details/{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var evento = await _context.Eventos
                .FirstOrDefaultAsync(m => m.Id == id);

            if (evento == null)
                return NotFound();

            return View(evento);
        }

        // GET: Eventos/Create

        
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Criar um novo evento.
        /// </summary>
        /// <param name="evento">Here is the description for ID.</param>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Evento criado com sucesso.</response>
        /// <response code="400">Evento com valores ausentes/inválidos.</response>
        /// <response code="500">Opa! Não foi possível criar seu evento agora.</response>
        [HttpPost("Eventos/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Valor,Gratuito,Descricao,Data")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                evento.Id = Guid.NewGuid();
                _context.Add(evento);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(evento);
        }

        /// <summary>
        /// Edita um evento já cadastrado.
        /// </summary>
        /// <param name="id">Id do evento a editar</param>
        /// <response code="200">Evento editado com sucesso.</response>
        /// <response code="404">Evento não encontrado.</response>
        /// <response code="500">Opa! Não foi possível editar seu evento agora.</response>
        [HttpGet("Eventos/Edit/{id}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var evento = await _context.Eventos.FindAsync(id);

            if (evento == null)
                return NotFound();

            return View(evento);
        }

        // POST: Eventos/Edit/5
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, [Bind("Nome,Valor,Gratuito,Descricao,Data")] Evento evento)
        {
            if (id != evento.Id)
                return NotFound();

            if (!ModelState.IsValid) return View(evento);

            try
            {
                _context.Update(evento);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoExists(evento.Id))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Eventos/Delete/5
        /// <summary>
        /// Exclui um evento já cadastrado.
        /// </summary>
        /// <param name="id">Id do evento a editar</param>
        /// <response code="200">Evento excluído com sucesso.</response>
        /// <response code="404">Evento não encontrado.</response>
        /// <response code="500">Opa! Não foi possível excluir seu evento agora.</response>
        [HttpDelete("Eventos/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var evento = await _context.Eventos
                .FirstOrDefaultAsync(m => m.Id == id);

            if (evento == null)
                return NotFound();

            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            _context.Eventos.Remove(evento);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(Guid id)
        {
            return _context.Eventos.Any(e => e.Id == id);
        }
    }
}
