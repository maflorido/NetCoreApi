using Agenda.Api.Middlewares;
using Agenda.Data;
using Agenda.Data.Repository;
using Agenda.Domain.Repository;
using Agenda.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ITaskService, TaskService>();
builder.Services.AddDbContext<AgendaContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AgendaDb"));
});
builder.Services.AddTransient<ITaskRepository, TaskRepository>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<AgendaContext>();
    context.Database.EnsureCreated();
}

app.UseAuthorization();

app.MapControllers();

app.UseGlobalMiddleware();

app.Run();
