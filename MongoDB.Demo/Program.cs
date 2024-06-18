using Microsoft.AspNetCore.Mvc;
using MongoDB.Demo;
using MongoDB.DependencyInjection;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddMongoClient(options => options.UseConnectionString("mongodb://localhost:27017"))
    .AddDatabase(MongoContext.DatabaseKey);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var group = app.MapGroup("/users");

group.MapGet("/", ([FromKeyedServices(MongoContext.DatabaseKey)] IMongoDatabase db) =>
{
    var result = db.GetCollection<User>(MongoContext.Users).Find(FilterDefinition<User>.Empty).ToList();
    return Results.Ok(result);
});

group.MapGet("/{id}", ([FromKeyedServices(MongoContext.DatabaseKey)] IMongoDatabase db, string id) =>
{
    var result = db.GetCollection<User>(MongoContext.Users).Find(user => user.Id == id);
    return Results.Ok(result.Single());
});

group.MapPost("/", ([FromKeyedServices(MongoContext.DatabaseKey)] IMongoDatabase db, [FromBody] User user) =>
{
    db.GetCollection<User>(MongoContext.Users).InsertOne(user);
    return Results.Created();
});

group.MapPut("/{id}", ([FromKeyedServices(MongoContext.DatabaseKey)] IMongoDatabase db, string id, [FromBody] User user) =>
{
    db.GetCollection<User>(MongoContext.Users).ReplaceOne(user => user.Id == id, user);
    return Results.Ok();
});

group.MapDelete("/{id}", ([FromKeyedServices(MongoContext.DatabaseKey)] IMongoDatabase db, string id) =>
{
    db.GetCollection<User>(MongoContext.Users).DeleteOne(user => user.Id == id);
    return Results.Ok();
});

app.Run();
