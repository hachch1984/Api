using Api;
using Api.EndPoint;


var builder = WebApplication.CreateBuilder(args);

//area de los servicios - inicio


Program_Services.Configure(builder);


//area de los servicios - fin
var app = builder.Build();


//middleware - inicio


Program_Middleware.Configure(builder, app); 



app.UseCors("AllowAll");
 
app.MapGroup("/personas").MapActorEndPoint(); 

//middleware - fin



app.Run();
 




