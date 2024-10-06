using BusinessLogic.Exstentions;
using DataAccess;
using WebComp.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var connStr = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddTelegramDbContext(connStr);
builder.Services.AddBusinessLogicServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();
