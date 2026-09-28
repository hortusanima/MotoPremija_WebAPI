using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MotoPremija_WebAPI.Filteri.GlobalniFilteri;
using MotoPremija_WebAPI.SlojPodataka.ADO.Repozitorijumi;
using MotoPremija_WebAPI.SlojPodataka.EFCore.Kontekst;
using MotoPremija_WebAPI.SlojPodataka.EFCore.Repozitorijumi;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.ADO;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.EFCore;
using MotoPremija_WebAPI.SlojServisa.PomocneFunkcije.PomocniModeli;
using MotoPremija_WebAPI.SlojServisa.PomocneMetode;
using MotoPremija_WebAPI.SlojServisa.Servisi.KorisnikServisi;
using MotoPremija_WebAPI.SlojServisa.Servisi.MotociklServisi;
using MotoPremija_WebAPI.SlojServisa.Servisi.OsiguranjeServisi;
using MotoPremija_WebAPI.SlojServisa.Servisi.TipOsiguranjaServisi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var konekcioniString = builder.Configuration
    .GetConnectionString("MotoPremijeBaza");

builder.Services
    .AddDbContext<KontekstBazeAplikacije>(opcije =>
    {
        opcije.UseNpgsql(konekcioniString);
    });

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opcije =>
    {
        var kljuc = Encoding.ASCII.GetBytes(builder.Configuration["JWT:TajniKljuc"]!);

        opcije.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(kljuc),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

builder.Services
    .AddScoped<IKorisnikRepozitorijum, KorisnikRepozitorijum>();
builder.Services
    .AddScoped<IMotociklRepozitorijum, MotociklRepozitorijum>();
builder.Services
    .AddScoped<IOsiguranjeRepozitorijum, OsiguranjeRepozitorijum>();
builder.Services
    .AddScoped<ITipOsiguranjaRepozitorijum, TipOsiguranjaRepozitorijum>();

builder.Services.AddScoped<IKorisnikADORepozitorijum>(sp =>
    new KorisnikADORepozitorijum(konekcioniString!));

builder.Services.AddScoped<IMotociklADORepozitorijum>(sp =>
new MotociklADORepozitorijum(konekcioniString!));

builder.Services.AddScoped<IOsiguranjeADORepozitorijum>(sp =>
    new OsiguranjeADORepozitorijum(konekcioniString!));

builder.Services.AddScoped<ITipOsiguranjaADORepozitorijum>(sp =>
    new TipOsiguranjaADORepozitorijum(konekcioniString!));

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<RegistracijaServis>();
builder.Services.AddScoped<PrijavaServis>();
builder.Services.AddScoped<VratiKorisnikaServis>();

builder.Services.AddScoped<DodajMotociklServis>();
builder.Services.AddScoped<VratiMotociklServis>();
builder.Services.AddScoped<AzurirajMotociklServis>();
builder.Services.AddScoped<ObrisiMotociklServis>();

builder.Services.AddScoped<VratiSvaOsiguranjaServis>();
builder.Services.AddScoped<KreirajOsiguranjeServis>();

builder.Services.AddScoped<VratiTipoveOsiguranjaServis>();

builder.Services
    .Configure<JWTOpcije>(builder.Configuration
    .GetSection("JWT"));

builder.Services.AddScoped<FunkcijeJWTokena>();

builder.Services.AddControllers(opcije =>
{
    //opcije.Filters
    //    .Add(typeof(Filter_ResiIzuzetakServerskeGreske));
    opcije.Filters
        .Add(typeof(Filter_ResiIzuzetakSaPovezivanjemPodataka));
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opcije =>
{
    opcije.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    opcije.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
