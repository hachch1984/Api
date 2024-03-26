using FluentValidation;
using MediatR;
using MinimalApiPeliculas.Entity;
using MinimalApiPeliculas.Repositorio;

namespace MinimalApiPeliculas.Command
{



    public class PersonaQuery
    {





        public class GetAll : CmdBase, IRequest<GetAll>
        {

            public List<Persona> Results { get; protected set; } = [];



            public class Handler(IPersonaRepository personaRepository) : IRequestHandler<GetAll, GetAll>
            {
                public async Task<GetAll> Handle(GetAll request, CancellationToken cancellationToken)
                {

                    request.Results = await personaRepository.GetAll();

                    return request;
                }
            }

        }





        public class GetById : CmdBase, IRequest<GetById>
        {
            protected string Id;
            public GetById(string id)
            {
                Id = id;
            }




            public Persona? Result { get; protected set; }



            public class Handler(IPersonaRepository personaRepository) : IRequestHandler<GetById, GetById>
            {
                public async Task<GetById> Handle(GetById request, CancellationToken cancellationToken)
                {
                    request.Result = await personaRepository.GetById(request.Id);
                    return request;
                }
            }


            public class Validator : AbstractValidator<GetById>
            {
                public Validator()
                {
                    RuleFor(x => x.Id).Custom((value, context) =>
                    {
                        if (value==null || value.Trim()=="")
                        {
                            context.AddFailure("el valor no puede ser vacio o nulo");
                        }
                       
                    });
                }
            }
        }







    }



}
