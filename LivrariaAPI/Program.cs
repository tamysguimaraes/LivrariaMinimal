using LivrariaAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LivrariaContext>(opt =>
                opt.UseInMemoryDatabase("LivrariDB"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/products", async (LivrariaContext db) =>
            await db.Products.ToListAsync() is List<Product> products && products.Count > 0 ? Results.Ok(products) : Results.NoContent()
)
    .Produces<List<Product>>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status204NoContent);

app.MapPost("/products", async (Product product, LivrariaContext db) =>
{
    db.Products.Add(product);
    await db.SaveChangesAsync();
    return Results.Created($"/product/{product.Id}", product);
}).Produces(StatusCodes.Status201Created);

app.MapGet("/products/{id}", async (int Id, LivrariaContext db) =>
await db.Products.FindAsync(Id) is Product product ? Results.Ok(product) : Results.NotFound())
    .Produces<Product>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound);

app.MapPut("/products/{id}", async (int Id, Product productUpdate, LivrariaContext db) =>
{
    if (await db.Products.FindAsync(Id) is Product product)
    {
        product.Name = productUpdate.Name;
        product.Quantity = productUpdate.Quantity;
        product.Price = productUpdate.Price;
        product.Category= productUpdate.Category;
        product.Img = productUpdate.Img;
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
})
    .Produces(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status404NotFound);

app.MapDelete("/products/{Id}", async (int Id, LivrariaContext db) =>
{
    if (await db.Products.FindAsync(Id) is Product product)
    {
        db.Products.Remove(product);
        await db.SaveChangesAsync();
        return Results.Ok(product);
    }
    return Results.NotFound();
})
    .Produces(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound);

app.Run();

class LivrariaContext : DbContext
{
    public LivrariaContext(DbContextOptions<LivrariaContext> option)
         : base(option)
    {
        
    }

    public DbSet<Product> Products => Set<Product>();
}