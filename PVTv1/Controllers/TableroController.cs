using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

using PVTv1.Models;


public class TableroController : Controller
{
 
    // --- Simulación de Base de Datos (Reemplazar con acceso a datos real) ---
    private static List<TableroViewModel> _tableros = new List<TableroViewModel>
        {
            new TableroViewModel {
                Id = 1,
                NombreTablero = "Tablero de Ventas Globales",
                Descripcion = "Análisis detallado de las ventas mensuales y trimestrales a nivel global.",
                URL = "http://JDC.com/dashboards/ventas-globales",
                Grupo = "Ventas",
                Orden = 1,
                FechaModificacion = new DateTime(2023, 10, 20)
            },
            new TableroViewModel {
                Id = 2,
                NombreTablero = "Rendimiento de Marketing Digital",
                Descripcion = "Seguimiento de KPIs clave de campañas de marketing online y ROI.",
                URL = "http://JDC.com/dashboards/marketing-digital",
                Grupo = "Marketing",
                Orden = 2,
                FechaModificacion = new DateTime(2023, 10, 25)
            },
            new TableroViewModel {
                Id = 3,
                NombreTablero = "Operaciones y Logística",
                Descripcion = "Monitorización en tiempo real del estado de envíos, inventario y eficiencia logística.",
                URL = "http://JDC.com/dashboards/operaciones",
                Grupo = "Operaciones",
                Orden = 1, 
                FechaModificacion = new DateTime(2023, 10, 28)
            }
        };

    private static List<GrupoViewModel> _grupos = new List<GrupoViewModel>
        {
            new GrupoViewModel { IdGrupo = 1, Descripcion = "Ventas" },
            new GrupoViewModel { IdGrupo = 2, Descripcion = "Marketing" },
            new GrupoViewModel { IdGrupo = 3, Descripcion = "Operaciones" },
            new GrupoViewModel { IdGrupo = 4, Descripcion = "Recursos Humanos" }
        };

    private static int _nextGrupoId = _grupos.Any() ? _grupos.Max(g => g.IdGrupo) + 1 : 1;

    private static int _nextId = _tableros.Any() ? _tableros.Max(t => t.Id) + 1 : 1;
 
    public IActionResult Index_ORG(string searchTerm)
    {
        IEnumerable<TableroViewModel> tablerosFiltrados;

        if (!String.IsNullOrEmpty(searchTerm))
        {
            string lowerSearchTerm = searchTerm.ToLower();
            tablerosFiltrados = _tableros.Where(s =>
                (s.NombreTablero != null && s.NombreTablero.ToLower().Contains(lowerSearchTerm)) ||
                (s.Descripcion != null && s.Descripcion.ToLower().Contains(lowerSearchTerm)) ||
                (s.Grupo != null && s.Grupo.ToLower().Contains(lowerSearchTerm)) ||
                (s.URL != null && s.URL.ToLower().Contains(lowerSearchTerm))
            ).ToList(); // Convertir a lista después de filtrar
        }
        else
        {
            tablerosFiltrados = _tableros.ToList(); // Me lo transforma a lista
        }

        var viewModel = new TablerosPageViewModel
        {
            Tableros = tablerosFiltrados.OrderBy(t => t.Grupo).ThenBy(t => t.Orden).ThenBy(t => t.NombreTablero), // Le doy un orden
            SearchTerm = searchTerm
        };

        return View(viewModel);
    }

       public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tablero = _tableros.FirstOrDefault(t => t.Id == id);
        if (tablero == null)
        {
            return NotFound();
        }

        return View(tablero);
    }


    public IActionResult Create()
    {

        var model = new TableroViewModel
        {
            FechaModificacion = DateTime.Today // Pongo la fecha de hoy automáticamete
        };
        return View(model);
    }


    [HttpPost]
    [ValidateAntiForgeryToken] 
    public IActionResult Create([Bind("NombreTablero,Descripcion,URL,Grupo,Orden,FechaModificacion")] TableroViewModel tablero) // Chequea que se se solape
    {

        if (ModelState.IsValid)
        {
            tablero.Id = _nextId++; // Asignar un nuevo ID 
            _tableros.Add(tablero);
            TempData["SuccessMessage"] = "Tablero creado exitosamente."; // Mensaje para usuario
            return RedirectToAction(nameof(Index));
        }
        
        return View(tablero);
    }

    
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tablero = _tableros.FirstOrDefault(t => t.Id == id);
        if (tablero == null)
        {
            return NotFound();
        }
        return View(tablero);
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,NombreTablero,Descripcion,URL,Grupo,Orden,FechaModificacion")] TableroViewModel tablero)
    {
        if (id != tablero.Id)
        {
            return NotFound(); 
        }

        if (ModelState.IsValid)
        {
            try
            {
                var existingTablero = _tableros.FirstOrDefault(t => t.Id == id);
                if (existingTablero == null)
                {
                    return NotFound();
                }

                // Actualizar las propiedades del tablero existente
                existingTablero.NombreTablero = tablero.NombreTablero;
                existingTablero.Descripcion = tablero.Descripcion;
                existingTablero.URL = tablero.URL;
                existingTablero.Grupo = tablero.Grupo;
                existingTablero.Orden = tablero.Orden;
                existingTablero.FechaModificacion = tablero.FechaModificacion;
                

                TempData["SuccessMessage"] = "Tablero actualizado exitosamente.";
            }
            catch (Exception /* ex */)
            {
                
                ModelState.AddModelError("", "No se pudo guardar los cambios. Inténtelo de nuevo, y si el problema persiste, contacte al administrador.");
                return View(tablero); 
            }
            return RedirectToAction(nameof(Index));
        }

        return View(tablero);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tablero = _tableros.FirstOrDefault(t => t.Id == id);
        if (tablero == null)
        {
            return NotFound();
        }

        return View(tablero); 
    }



    [HttpPost, ActionName("Delete")] 
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id) // Se espera el 'id' desde la ruta
    {
        var tablero = _tableros.FirstOrDefault(t => t.Id == id);
        if (tablero == null)
        {
            
            TempData["ErrorMessage"] = "El tablero que intentó eliminar no fue encontrado.";
            return RedirectToAction(nameof(Index_ORG));
        }

        _tableros.Remove(tablero);
        TempData["SuccessMessage"] = "Tablero eliminado exitosamente.";
        return RedirectToAction(nameof(Index));
    }


    private bool TableroExists(int id)
    {
        return _tableros.Any(e => e.Id == id);
    }

    public IActionResult Grupos()
    {
       
        var gruposOrdenados = _grupos.OrderBy(g => g.Descripcion).ToList();
        return View(gruposOrdenados);
    }

    public IActionResult CreateGrupo()
    {
        return View(new GrupoViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateGrupo(GrupoViewModel grupo)
    {
        if (ModelState.IsValid)
        {
            grupo.IdGrupo = _nextGrupoId++;
            _grupos.Add(grupo);
            TempData["SuccessMessage"] = "Grupo creado exitosamente.";
            return RedirectToAction(nameof(Grupos)); // Redirige a la lista de grupos
        }
     
        return View(grupo); 
    }

    public IActionResult EditGrupo(int? id)
    {
        if (id == null) return NotFound();
        var grupo = _grupos.FirstOrDefault(g => g.IdGrupo == id);
        if (grupo == null) return NotFound();
        return View(grupo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditGrupo(int id, GrupoViewModel grupo)
    {
        if (id != grupo.IdGrupo) return NotFound();

        if (ModelState.IsValid)
        {
            var existingGrupo = _grupos.FirstOrDefault(g => g.IdGrupo == id);
            if (existingGrupo == null) return NotFound();

            existingGrupo.Descripcion = grupo.Descripcion;
            TempData["SuccessMessage"] = "Grupo actualizado exitosamente.";
            return RedirectToAction(nameof(Grupos));
        }
        
        return View(grupo); 
    }


 
    [HttpPost, ActionName("DeleteGrupo")] 
    [ValidateAntiForgeryToken]
    public IActionResult DeleteGrupoConfirmed(int id) // Recibe Id de la ruta
    {
        var grupo = _grupos.FirstOrDefault(g => g.IdGrupo == id);
        if (grupo != null)
        {
            _grupos.Remove(grupo);
            TempData["SuccessMessage"] = "Grupo eliminado exitosamente.";
        }
        else
        {
            TempData["ErrorMessage"] = "Grupo no encontrado.";
        }
        return RedirectToAction(nameof(Grupos));
    }

}

