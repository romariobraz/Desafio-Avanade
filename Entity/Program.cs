#region Imports
using Avanade.Contexto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Avanade.DTOs.DadosLogin;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Avanade.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
#endregion

#region Builders, key e swagger
//Inicio das builder
var builder = WebApplication.CreateBuilder(args);
//Key para JWT
var key = Encoding.ASCII.GetBytes(Settings.Secret);

// Configura o DbContext com PostgreSQL
builder.Services.AddDbContext<ProdutoContexto>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConexaoPostgres1"))
);
builder.Services.AddDbContext<VendasContexto>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConexaoPostgres1"))
);
//Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minha API", Version = "v1" });

    // Adiciona o esquema de autenticação Bearer
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Auth header Bearer.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    // Aplica o esquema globalmente
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        //Configurações do token Jwt
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = string.Empty;
    });
}
#endregion

#region Middlewares
// Middleware (redirecionamento, autenticação, autorização e mapeamento de controladores)
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "API de Vendas e Produtos");
#endregion

#region Usuários, login e JWT
// Endpoint de login que gera JWT
app.MapPost("/login", ([FromBody] Users loginDTO) =>
{
    // Repositório de usuários fixos
    var repo = new UserRep();
    var user = repo.UserData(loginDTO.Usuario, loginDTO.Senha);

    if (user == null)
        return Results.Unauthorized();
    // Geração do token JWT
    var key = Encoding.ASCII.GetBytes(Settings.Secret);
    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, user.Usuario),
            new Claim(ClaimTypes.Role, user.Role)
        }),
        Expires = DateTime.UtcNow.AddHours(1),
        SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    var jwt = tokenHandler.WriteToken(token);

    // Retorna o token para o cliente
    return Results.Ok(new { token = jwt });
});
#endregion

#region Endpoint do admin e teste rápido
//Apenas para admin
app.MapGet("/admin", [Authorize(Roles = "Admin")] () => Results.Ok("Acesso liberado para Admin!"));

//Endpoint protegido de teste rápido
app.MapGet("/teste-rapido-auth", [Authorize] () =>
{
    return Results.Ok(new[] { "Você está autenticado!" });
});
#endregion

app.Run();
