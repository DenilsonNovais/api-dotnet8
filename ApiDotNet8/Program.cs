using ApiDotNet8.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

