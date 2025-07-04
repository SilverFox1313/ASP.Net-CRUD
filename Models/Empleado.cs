namespace modulo10proyectofinal.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int PaisId { get; set; }

        // Nueva propiedad para mostrar el nombre del país
        public string PaisNombre { get; set; }
    }
}
