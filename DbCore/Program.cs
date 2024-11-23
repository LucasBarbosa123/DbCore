using DbCore.Services;
using DbCoreDatabase;
using DbManagerService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOCPPDbContext(builder.Configuration);
builder.Services.AddTransient<IContextInteractor, ContextInteractor>();
builder.Services.AddTransient<IDbManager, DbManager>();
builder.Services.AddTransient<IValidator, Validator>();

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
