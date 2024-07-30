using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppAlumnos.Models.Entities;
using AppAlumnos.Models.BL;

namespace AppAlumnos.Controllers
{
    public class AlumnosController : Controller
    {
        private readonly BLAlumnos _bLAlumnos;

        public AlumnosController()
        {
            _bLAlumnos = new BLAlumnos();
        }

        // GET: Alumnos
        public async Task<IActionResult> Index()
        {
            var alumnos = await _bLAlumnos.Consultar();
            return View(alumnos);
        }

        // GET: Alumnos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            return await Consultar(id);
        }

        // GET: Alumnos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alumnos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,PrimerApellido,SegundoApellido,Correo,Telefono,FechaNacimiento,Curp,SueldoMensual,IdEstadoOrigen,IdEstatus")] Alumnos alumnos)
        {
            if (ModelState.IsValid)
            {
                var alum = await _bLAlumnos.Agregar(alumnos);
                return RedirectToAction(nameof(Index));
            }
            return View(alumnos);
        }

        // GET: Alumnos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            return await Consultar(id);
        }

        // POST: Alumnos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,PrimerApellido,SegundoApellido,Correo,Telefono,FechaNacimiento,Curp,SueldoMensual,IdEstadoOrigen,IdEstatus")] Alumnos alumnos)
        {
            if (id != alumnos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bLAlumnos.Actualizar(alumnos);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlumnosExists(alumnos.Id))
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
            return View(alumnos);
        }

        // GET: Alumnos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            return await Consultar(id);
        }

        // POST: Alumnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bLAlumnos.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AlumnosExists(int id)
        {
            return (_bLAlumnos.Consultar(id) != null ? true : false);
        }

        private async Task<IActionResult> Consultar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var alumnos = await _bLAlumnos.Consultar(id);
            if (alumnos == null)
            {
                return NotFound();
            }
            return View(alumnos);
        }
    }
}
