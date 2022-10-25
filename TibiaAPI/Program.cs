using DB1;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using TibiaAPI.Security;
using TibiaModels.BL;
using TibiaModels.BL.Security;
using TibiaRepositories.BL;
using TibiaRepositories.BL.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
//Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                      });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
}).AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtToken:key"])),

        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JwtToken:issuer"],

        ValidateAudience = true,
        ValidAudience = builder.Configuration["JwtToken:audience"],

        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(Convert.ToDouble(builder.Configuration["JwtToken:minutestoexpiration"]))
    };
});
builder.Services.AddAuthorization(options =>
{
    var claims = new AppClaim().GetType().GetProperties().ToList();
    foreach(var claim in claims)
    {
        options.AddPolicy(claim.Name, pol => pol.RequireClaim(claim.Name, "True"));
    }
});
builder.Services.AddControllers();
//builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PubContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:TibiaDB"]));

builder.Services.AddSingleton<JwtSettings>(new JwtSettings()
{
    Key = builder.Configuration["JwtToken:key"],
    Audience = builder.Configuration["JwtToken:audience"],
    Issuer = builder.Configuration["JwtToken:issuer"],
    MinutesToExpiration = Convert.ToInt32(builder.Configuration["JwtToken:minutestoexpiration"])
});
builder.Services.AddScoped<SecurityManager>();
builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<INpcRepository, NpcRepository>();
builder.Services.AddScoped<IMonsterRepository, MonsterRepository>();
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IItemInstanceRepository, ItemInstanceRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();