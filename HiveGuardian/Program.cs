using HiveGuardian.Data;
using HiveGuardian.Services;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<HiveDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString(nameof(HiveDbContext))));

builder.Services.AddHostedService<MigrationService>();
builder.Services.AddHostedService<SerialReaderService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();
app.MapControllers();
app.Run();