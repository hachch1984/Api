using FluentValidation;
using MediatR;
using Api.Entity;
using Api.Repositorio;

namespace Api.Command
{

    public class PersonaCommand
    {
        /// <summary>
        /// base for add and update 
        /// </summary>
        
       public   abstract class Base : CmdBase
        {
            public Persona Persona { get; protected set; }
            public Base(Persona persona)
            {
                Persona = persona;
            }
 
        }
        public abstract class Validator<T> : AbstractValidator<T> where T : Base, IRequest<T>
        {
            public Validator(CommandAction addOrUpdate, IPersonaRepository personaRepositorio)
            {
                RuleFor(x => x).CustomAsync(async (value, context, cancellationToken) =>
                {
                    if (value is null)
                    {
                        throw new Exception("el objeto es nulo");
                    }

                    var obj = value.Persona;
                    if (obj is null)
                    {
                        throw new Exception("el objeto es nulo");
                    }

                    switch (addOrUpdate)
                    {
                        case CommandAction.Add:
                            if (string.IsNullOrEmpty(obj.Id)==false)
                            {
                                context.AddFailure(nameof(obj.Id), "el id debe ser nulo o vacio");
                                return;
                            }

                            break;
                        case CommandAction.Update:
                            if (obj.Id ==null || obj.Id.Trim()=="")
                            {
                                context.AddFailure(nameof(obj.Id), "id id no puede ser nulo");
                                return;
                            }
                            else if ((await personaRepositorio.ExistsById(obj.Id,cancellationToken)) == false)
                            {
                                context.AddFailure(nameof(obj.Id), "no existe el registro");
                                return;
                            }
                            break;
                    }

                    if (await personaRepositorio.ExistsByNombresApellidos(obj.Id, obj.Nombres, obj.Apellidos, cancellationToken))
                    {
                        context.AddFailure(nameof(obj.Id), "la persona ya ha sido registrada");
                        return;
                    }


                    if (string.IsNullOrEmpty(obj.Nombres))
                    {
                        context.AddFailure(nameof(obj.Nombres), "los nombres no pueden estar en blanco");
                    }
                    else if (obj.Nombres.Length<3 || obj.Nombres.Length>50)
                    {
                        context.AddFailure(nameof(obj.Nombres), "los nombres deben tener entre 3 y 50 caracteres");
                    }
                    

                    if (string.IsNullOrEmpty(obj.Apellidos))
                    {
                        context.AddFailure(nameof(obj.Apellidos), "los apellidos no pueden estar en blanco");
                    }else if (obj.Apellidos.Length <3 || obj.Apellidos.Length >50)
                    {
                        context.AddFailure(nameof(obj.Apellidos), "los apellidos deben tener entre 3 y 50 caracteres");
                    }

                    if (string.IsNullOrEmpty(obj.DocumentoIdentidad))
                    {
                        context.AddFailure(nameof(obj.DocumentoIdentidad), "el documento no puede estar en blanco");
                    }else if (obj.DocumentoIdentidad.Trim().Length!=8)
                    {
                        context.AddFailure(nameof(obj.DocumentoIdentidad), "el documento de identidad debe estar conformado por 8 caracteres");
                    }



                


                  


                });
            }
        }

        public class Add(Persona persona) : Base(persona), IRequest<Add>
        {
            public class Handler(IPersonaRepository personaRepository) : IRequestHandler<Add, Add>
            {
                public async Task<Add> Handle(Add request, CancellationToken cancellationToken)
                {
                    request.Persona.Id = await personaRepository.Add(request.Persona, cancellationToken);
                    return request;
                }
            }

            public class Validator(IPersonaRepository generoRepositorio) : Validator<Add>(CommandAction.Add, generoRepositorio)
            {
            }

        }

        public class Update(Persona persona) : Base(persona), IRequest<Update>
        {

            public class Handler(IPersonaRepository generoRepositorio) : IRequestHandler<Update, Update>
            {
                public async Task<Update> Handle(Update request, CancellationToken cancellationToken)
                {
                    await generoRepositorio.Update(request.Persona, cancellationToken);
                    return request;
                }
            }

            public class Validator(IPersonaRepository personaRepository) : Validator<Update>(CommandAction.Update, personaRepository)
            {

            }

        }



        public class Remove : CmdBase, IRequest<Remove>
        {
            private readonly string Id;
            public Remove(string id)
            {
                Id = id;
            }


            public class Handler(IPersonaRepository personaRepository) : IRequestHandler<Remove, Remove>
            {
                public async Task<Remove> Handle(Remove request, CancellationToken cancellationToken)
                {
                    await personaRepository.RemoveById(request.Id);
                    return request;
                }
            }
            public class Validator : AbstractValidator<Remove>
            {
                public Validator(IPersonaRepository personaRepository)
                {
                    RuleFor(x => x.Id).CustomAsync(async (value, context, cancellationToken) =>
                    {
                        var exists = await personaRepository.ExistsById(value, cancellationToken);
                        if (exists == false)
                        {
                            context.AddFailure("no existe el id");
                        }
                    });
                }
            }

        }


    }


}
