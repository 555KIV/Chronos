using Chronos.Extensions;
using Chronos.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

app.ConfigureApp();

app.Run();