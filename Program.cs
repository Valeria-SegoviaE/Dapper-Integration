using Microsoft.AspNetCore.Mvc;
using Orders.Api.helpers;
using Orders.Api.models;
using Orders.Api.services;
using Orders.Api.repositories;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>(optional: true);
}

// Add services to the container.
builder.Services.AddOptions<StudentInfo>()
    .BindConfiguration(StudentInfo.sectionName);


builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IDapperService, DapperService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddControllers()
.ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
            new BadRequestObjectResult(new ApiErrorResponse(
                StatusCodes.Status400BadRequest,
                "The request contains invalid data.",
                ValidationErrors.FromModelState(context.ModelState)));
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
       options.SwaggerEndpoint("/openapi/v1.json", "Orders.API"); 
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
