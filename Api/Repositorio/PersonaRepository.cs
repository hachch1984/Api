using Microsoft.EntityFrameworkCore;
using Api.Entity;
using Api.EntityFramework;

namespace Api.Repositorio
{




    public class PersonaRepository : IPersonaRepository
    {
        private readonly ApplicationDbContext context;

        public PersonaRepository(ApplicationDbContext context)
        {
            this.context = context;
        }


        public async Task<string> Add(Persona obj, CancellationToken cancellationToken = default)
        {
            obj.Id = Guid.NewGuid().ToString();
            obj.Estado = obj.Estado.ToUpper();
            await context.Persona.AddAsync(obj, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return obj.Id;
        }

        public async Task Update(Persona obj, CancellationToken cancellationToken = default)
        {
            obj.Estado = obj.Estado.ToUpper();
            context.Persona.Update(obj);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveById(string id, CancellationToken cancellationToken = default)
        {
            await context.Persona.Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);
        }

        public async Task<bool> ExistsByNombresApellidos(string id, string nombres, string apellidos, CancellationToken cancellationToken = default)
        {

            var query = context.Persona.AsNoTracking();

            if (string.IsNullOrEmpty(id) == false)
            {
                query = query.Where(x => x.Id != id);
            }

            return await query.AnyAsync(x => x.Nombres == nombres && x.Apellidos == apellidos, cancellationToken);

        }

        public async Task<bool> ExistsById(string id, CancellationToken cancellationToken = default)
        {
            return await context.Persona.AsNoTracking().AnyAsync(x => x.Id == id, cancellationToken);
        }


        public async Task<List<Persona>> GetAll (CancellationToken cancellationToken = default)
        {
            return await context.Persona.AsNoTracking().ToListAsync(cancellationToken);
        } 
        public async Task<Persona?>GetById(string id, CancellationToken cancellationToken = default)
        {
            return await context.Persona.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

    }
}
