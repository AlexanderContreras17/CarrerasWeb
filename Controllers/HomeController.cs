using Microsoft.AspNetCore.Mvc;
using U2Ejericio4.Models.Entities;
using U2Ejericio4.Models.ViewModels;

namespace U2Ejericio4.Controllers
{
    public class HomeController : Controller
    {
            MapaCurricularContext context = new MapaCurricularContext();
        public IActionResult Index()
        {
            var proyeccion = context.Carreras.Select(x => new IndexViewModel
            {
                Nombre = x.Nombre,
                Plan = x.Plan
            }).OrderBy(x=>x.Nombre);
            return View(proyeccion);
        }
        [Route("/{id}/Informacion")]
        public IActionResult Informacion(string id)
        {
            id = id.Replace("-", "");
            var datos = context.Carreras.Select(x => new InformacionViewModel
            {
                Id= x.Id,
                Nombre= x.Nombre,
                Descripcion=x.Descripcion?? "No Disponible",
                Especialidad=x.Especialidad??"No Disponible",
                Plan=x.Plan
            }).Where(y=>y.Nombre==id).FirstOrDefault();

            if (datos == null)
            {
                return RedirectToAction("Index");
            }
            return View(datos);
        }
        [Route ("/{carrera}/Reticula")]
        public IActionResult Mapa(string carrera)
        {
            var datos = context.Carreras.Where(x => x.Nombre == carrera.Replace("-", ""))
                .Select(x => new MapaViewModel
                {
                    NombreCarrera = x.Nombre,
                    Plan = x.Plan,
                    Creditos = x.Materias.Sum(m => m.Creditos),
                    Semestre = x.Materias.GroupBy(s => s.Semestre).Select(s => new SemestreModel
                    {
                        NumeroSemestre = s.Key,
                        Materias = s.Select(m => new MateriaModel
                        {
                            Clave = m.Clave,
                            Nombre = m.Nombre,
                            Creditos = m.HorasTeoricas + "-" + m.HorasPracticas + "-" + m.Creditos
                        })
                    })

                })
                .FirstOrDefault();
            if(datos== null)
            {
                return RedirectToAction("Index");
            }
            return View(datos);
        }
    }
}
