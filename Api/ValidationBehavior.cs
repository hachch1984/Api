using FluentValidation;
using MediatR;

namespace MinimalApiPeliculas
{


    public enum CommandAction
    {
        Add,
        Update
    }

 
    /// <summary>
    /// create, update, delete
    /// </summary>
    public class CmdBase
    {
        public Dictionary<string, string[]> Errores { get; set; } = [];
        public bool HasErrors => Errores.Count > 0;
    }
     
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
      where TRequest : IRequest<TResponse>
       where TResponse : CmdBase
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }


        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            

            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    _validators.Select(v =>
                        v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults
                    .Where(r => r.Errors.Any())
                    .SelectMany(r => r.Errors)
                    .Select(f => new { f.PropertyName, f.ErrorMessage })
                    .GroupBy(x=>x.PropertyName)
                    .ToDictionary(f => f.Key.First().ToString().ToLower() + f.Key.Substring(1), f => f.Select(x=>x.ErrorMessage).ToArray());   


                if (failures.Any())
                {
                    var cmdBase = request as CmdBase;
                    cmdBase.Errores = failures;
                    return cmdBase as TResponse;
                }                               
            }

            return await next();
        }
    }
     
}
