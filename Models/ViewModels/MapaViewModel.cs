using U2Ejericio4.Models.Entities;

namespace U2Ejericio4.Models.ViewModels
{
    public class MapaViewModel
    {
        public string NombreCarrera { get; set; } = null!;
        public string Plan { get; set; } = null!;
        public int Creditos {  get; set; }
        public IEnumerable<SemestreModel> Semestre { get; set; }=null!;


    }

    public class SemestreModel
    {
        public int NumeroSemestre { get; set; }
        public IEnumerable<MateriaModel> Materias { get; set; } = null!;
    }

    public class MateriaModel
    {
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Creditos { get; set; } = null!;
    }
}
