namespace PAW_Caso2.Models
{
    public class Dashboard
    {
        public int TotalEventos { get; set; }
        public int TotalUsuariosActivos { get; set; }
        public int AsistenciasMesActual { get; set; }
        public List<TopEvento> TopEventos { get; set; }
    }

    public class TopEvento
    {
        public string Titulo { get; set; }
        public string Categoria { get; set; }
        public int CantidadAsistentes { get; set; }
        public DateTime Fecha { get; set; }
    }
}
