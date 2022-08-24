using DB1;
using Microsoft.EntityFrameworkCore;
using TibiaRepositories.BL;
using TibiaRepositories.BL.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PubContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:TibiaDB"]));

builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<INpcRepository, NpcRepository>();
builder.Services.AddScoped<IMonsterRepository, MonsterRepository>();
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IItemInstanceRepository, ItemInstanceRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
