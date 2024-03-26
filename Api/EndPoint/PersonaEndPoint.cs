using Api.Dto;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Api.Command;
using Api.Entity;

namespace Api.EndPoint
{
    public static class PersonaEndPoint
    {



        public static RouteGroupBuilder MapActorEndPoint(this RouteGroupBuilder rgb)
        {
            rgb.MapGet("/{id}", GetById);
            rgb.MapGet("/", GetAll);


            rgb.MapPost("/", Add).DisableAntiforgery();
            rgb.MapPut("/", Update).DisableAntiforgery();
            rgb.MapDelete("/{id}", Remove);



            return rgb;
        }





        public static async Task<Results<Created<Persona>, ValidationProblem>> Add(
            [FromBody] PersonaDtoAdd personaDtoAdd,
            IOutputCacheStore outputCacheStore,
            IMapper mapper,
            IMediator mediator)
        {
            var persona = mapper.Map<Persona>(personaDtoAdd);

            var cmd = await mediator.Send(new PersonaCommand.Add(persona));

            if (cmd.HasErrors)
            {
                return TypedResults.ValidationProblem(cmd.Errores);
            }

            persona = cmd.Persona;


            return TypedResults.Created($"/personas/{persona.Id}", persona);
        }

        public static async Task<Results<NoContent, ValidationProblem>> Update(             
            [FromBody] PersonaDtoUpdate personaDtoUpdate,
            IMapper mapper,
            IOutputCacheStore outputCacheStore,
            IMediator mediator)
        {
            var persona = mapper.Map<Persona>(personaDtoUpdate);

            var cmd = await mediator.Send(new PersonaCommand.Update(persona));


            if (cmd.HasErrors)
            {
                return TypedResults.ValidationProblem(cmd.Errores);
            }

            return TypedResults.NoContent();
        }

        public static async Task<Results<NoContent, ValidationProblem>> Remove(
            string id,
            IMediator mediator)
        {
            var cmd = await mediator.Send(new PersonaCommand.Remove(id));

            if (cmd.HasErrors)
            {
                return TypedResults.ValidationProblem(cmd.Errores);
            }

            return TypedResults.NoContent();
        }


        public static async Task<Ok<List<Persona>>> GetAll(IMediator mediator)
        {
            var cmd = await mediator.Send(new PersonaQuery.GetAll());


            return TypedResults.Ok(cmd.Results);
        }


        public static async Task<Results<NotFound, Ok<Persona>>> GetById(string id, IMediator mediator)
        {
            var cmd = await mediator.Send(new PersonaQuery.GetById(id));


            return cmd.Result == null ? TypedResults.NotFound() : TypedResults.Ok(cmd.Result);
        }



    }
}
