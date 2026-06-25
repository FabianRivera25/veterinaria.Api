using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;
using Microsoft.OpenApi;
using Veterinaria.api.Middleware;
using Veterinaria.Application.Interface.Repository;
using Veterinaria.Application.Interface.Service;
using Veterinaria.Application.Mappings;
using Veterinaria.Application.Service;
using Veterinaria.Domain.Entities;
using Veterinaria.Infreaestructure.Data;
using Veterinaria.Infreaestructure.Repository;

var builder = WebApplication.CreateBuilder(args);
// Cargar variables de entorno
DotNetEnv.Env.Load();
builder.Configuration.AddEnvironmentVariables();

// Leer las variables de entorno
var host = Environment.GetEnvironmentVariable("HOST");
var port = Environment.GetEnvironmentVariable("PORT");
var database = Environment.GetEnvironmentVariable("DATABASE");
var user = Environment.GetEnvironmentVariable("USER");
var password = Environment.GetEnvironmentVariable("PASSWORD");
var key = Environment.GetEnvironmentVariable("JWT_KEY");
var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

// Validar variables de entorno 
var variablesFaltantes = new List<string>();
if (string.IsNullOrEmpty(host)) variablesFaltantes.Add("HOST");
if (string.IsNullOrEmpty(port)) variablesFaltantes.Add("PORT");
if (string.IsNullOrEmpty(database)) variablesFaltantes.Add("DATABASE");
if (string.IsNullOrEmpty(user)) variablesFaltantes.Add("USER");
if (string.IsNullOrEmpty(password)) variablesFaltantes.Add("PASSWORD");


if (variablesFaltantes.Any())
{
    throw new Exception($"Faltan variables de entorno: {string.Join(", ", variablesFaltantes)}");
}

// Construir la cadena de conexión para PostgreSQL
var connectionString =
    $"Host={host};" +
    $"Port={port};" +
    $"Database={database};" +
    $"Username={user};" +
    $"Password={password};" +
    $"SSL Mode=Require;" +
    $"Trust Server Certificate=true;";

//definir reglas de seguridad
builder.Services.AddIdentity<ApplicationUser, IdentityRole>( options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configurar la autenticación
builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
).AddJwtBearer(options =>
{
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        RoleClaimType = ClaimTypes.Role,
        ValidIssuer = issuer,
        ValidAudience = audience
    };

    options.Events = new JwtBearerEvents
    {
        OnChallenge = async context =>
        {
            context.HandleResponse();

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                status = 401,
                detail = "No autenticado. El token es inválido o no fue enviado."
            }));
        },

        OnForbidden = async context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                status = 403,
                detail = "Acceso denegado. No tiene permisos para acceder a este recurso."
            }));
        }
    };
});




// Registrar repositorios con su interface
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IVeterinarioRepository, VeterinarioRepository>();
builder.Services.AddScoped<IMascotaRepository, MascotaRepository>();
builder.Services.AddScoped<IDuenoRepository, DuenoRepository>();
builder.Services.AddScoped<ICitaRepository, CitaRepository>();
builder.Services.AddScoped<IdbSeederRepository, DbSeederRepository>();
// Registrar servicios con su interface
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IVeterinarioService, VeterinarioService>();
builder.Services.AddScoped<IMascotaService, MascotaService>();
builder.Services.AddScoped<IDuenoService, DuenoService>();
builder.Services.AddScoped<ICitaService, CitaService>();
//registrarIunitofwork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Registrar aplication dbcontextby
builder.Services.AddDbContext<ApplicationDbContext>(optios => { optios.UseNpgsql(connectionString); });



//registrar automaper
builder.Services.AddAutoMapper(cgf => { }, typeof(MappingProfile).Assembly);

// Add services to the container.

builder.Services.AddControllers();


// Swagger / OpenAPI configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Ecommerce API",
        Description = """
        #### **Infraestructura escalable para la gestión de comercio digital.**

        Esta API proporciona un conjunto robusto de herramientas para administrar operaciones comerciales complejas, garantizando seguridad, velocidad y una experiencia de usuario optimizada.

        ---

        #### Módulos del Sistema
        * **Catálogo:** Gestión dinámica de productos con control de stock en tiempo real.
        * **Ventas:** Administración integral de pedidos y seguimiento del ciclo de vida de compra.
        * **Finanzas:** Procesamiento de pagos y auditoría de transacciones.
        * **Soporte IA:** Chatbot de asistencia para búsqueda inteligente y recomendaciones personalizadas.

        #### Características Técnicas
        * **Seguridad:** Autenticación de grado industrial mediante **JWT**.
        * **Eficiencia:** Consumo de recursos optimizado con soporte para **paginación y filtrado**.
        * **Integración:** Salidas JSON estandarizadas para una fácil implementación en entornos Web y Mobile.

        ---
        """,

        Contact = new OpenApiContact
        {
            Name = "Mario Garcia (Soporte Técnico)",
            Email = "mrgmairena@gmail.com",
            Url = new Uri("https://github.com/MGarcia7783/E-commerce")
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    // Configuración de seguridad para Swagger (JWT)

    // 1. Definir el esquema de seguridad que Swagger usará para UI
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT. Ejemplo: eyJhbGciOiJIUzI1NiIsInR5..."
    });

    // 2. Aplicar el esquema de seguridad a toso los endpoint protegidos de la API
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference(referenceId: "Bearer", hostDocument: document),
            new List<string>()
        }
    });
});

// configuracion de CORS
var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
            policy.WithOrigins(
               "http://localhost:4200", //Angular
               "http://localhost:3000" //React
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
        }
        else
        {
            //solo para desarrollo si no hay configuracion
            policy.AllowAnyHeader()
                  .AllowAnyMethod();
        }
    });
});


builder.Services.AddOpenApi();
//construit la aplicacion

var app = builder.Build();



//Registrar middleware con manejo de exepciones
app.UseMiddleware<ExceptionMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        if (await context.Database.CanConnectAsync())
        {
            var seeder = services.GetRequiredService<IdbSeederRepository>();
            await seeder.SeederAsync();
            logger.LogInformation("El seeder se ejecutó correctamente");
        }
        else
        {
            logger.LogWarning("No se pudo conectar a la base de datos .Asegúrese de que la base de datos haya sido creada y que el servcio este activo. El seeder no se pudo ejecutar");

        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Se ha producido un erro al ejecutar la base de datos");
    }
}

// configuracion para entornos de desarrollo y produccion
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Veterinaria API v1");
});

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger/index.html");
    return Task.CompletedTask;
});

app.UseCors("FrontendPolicy");

//if (app.Environment.IsDevelopment())
//{
//    //app.MapOpenApi();
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
//app.UseHttpsRedirection();

//soporte para la autenticacion
app.UseAuthorization();
app.UseAuthorization();

//mapear controladores
app.MapControllers();

if(app.Environment.IsDevelopment())
{
    app.Run();
}
else
{
    var apiPort = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    app.Run($"http://0.0.0.0:{apiPort}");
}

