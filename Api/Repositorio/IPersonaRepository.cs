using Api.Entity;

namespace Api.Repositorio
{
    public interface IPersonaRepository
    {
        Task<string> Add(Persona obj, CancellationToken cancellationToken = default);
        Task<bool> ExistsById(string id, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNombresApellidos(string id, string nombres, string apellidos, CancellationToken cancellationToken = default);
        Task<List<Persona>> GetAll(CancellationToken cancellationToken = default);
        Task<Persona?> GetById(string id, CancellationToken cancellationToken = default);
        Task RemoveById(string id, CancellationToken cancellationToken = default);
        Task Update(Persona obj, CancellationToken cancellationToken = default);
    }
}