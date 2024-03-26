namespace Api.Dto
{
    public class PersonaDtoAdd
    {

        public string Nombres { get; set; } = "";
        public string Apellidos { get; set; } = "";
        public string DocumentoIdentidad { get; set; } = "";
        public DateTime FechaNacimiento { get; set; }
        public string Estado { get; set; } = "";
    }

}
