namespace Api
{
    public static class Program_Middleware
    {
        public static void Configure(WebApplicationBuilder builder, WebApplication app)
        {
            if (builder.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(
                  c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Henry Alberto Chavez Chavez v1")
                    );


            }

         
             
             

        }
    }
}
