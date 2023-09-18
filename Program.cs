using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Configuracion para Validar Autenticacion.
builder.Services.AddAuthentication(options  =>
{
	options.DefaultAuthenticateScheme  =  JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme  =  JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options  =>
{
	// Agregar URL de su cuenta de Auth0 de cada uno.
	options.Authority  =  "https://dev-g2i16bq5wmu7y3j4.us.auth0.com/";
	// Agregar audiencia de la API.
	options.Audience  =  "https://api.example.com/estudiantes";
});

//Configuracion para Validar Autorización. 
builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("read:estudiantes", policy => policy.Requirements.Add(new HasScopeRequirement("read:estudiantes", "https://dev-g2i16bq5wmu7y3j4.us.auth0.com/")));
        options.AddPolicy("write:estudiantes", policy => policy.Requirements.Add(new HasScopeRequirement("write:estudiantes", "https://dev-g2i16bq5wmu7y3j4.us.auth0.com/")));
    });
    
builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Habilitamos Autenticacion.
app.UseAuthentication();

//Habilitamos Autorización. 
app.UseAuthorization();

app.MapControllers();

app.Run();
