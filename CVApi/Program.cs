using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using BlazorApp3.Data;
using CVClassLibrary.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

//Added HttpClient as a service to make the connection work..?
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//------------------------ Skills ------------------------

//Read, All 
app.MapGet("/skills", async (ApplicationDbContext dbContext) =>
{
    try
    {
        var skills = await dbContext.Skills.ToListAsync();
        return Results.Ok(skills);
    }
    catch
    {
        return Results.Problem("Server Error");
    }
});

//Read, By ID
app.MapGet("/skills/{id}", async (ApplicationDbContext dbContext, Guid id) =>
{
    try
    {
        var product = await dbContext.Skills.FirstOrDefaultAsync(x => x.Id == id);
        if (product != null)
        {
            return Results.Ok(product);
        }
        else
        {
            return Results.NotFound("No product with this ID was found");
        }

    }
    catch
    {
        return Results.Problem("Server Error");
    }
});

//Create
app.MapPost("/skill", async (ApplicationDbContext dbContext, Skill skill) =>
{
    try
    {
        await dbContext.Skills.AddAsync(skill);
        await dbContext.SaveChangesAsync();

        return Results.Ok();
    }
    catch (Exception)
    {
        return Results.StatusCode(500);
    }
});


//Update
app.MapPut("/Skills/{id}", async (ApplicationDbContext dbContext, Guid id, Skill skills) =>
{
    try
    {
        var skillsToUpdate = await dbContext.Skills.FirstOrDefaultAsync(x => x.Id == id);

        if (skillsToUpdate != null)
        {
            skillsToUpdate.Title = skills.Title;
            skillsToUpdate.pictureURL = skills.pictureURL;
            skillsToUpdate.Description = skills.Description;
            skillsToUpdate.SkillLevel = skills.SkillLevel;
            skillsToUpdate.YearsOfExperience = skills.YearsOfExperience;


            await dbContext.SaveChangesAsync();
            return Results.Ok(skillsToUpdate);
        }
        else
        {
            return Results.NotFound("No product with this ID was found");
        }

    }
    catch
    {
        return Results.Problem("Server Error");

    }

});

//Delete
app.MapDelete("/skills/{id}", async (ApplicationDbContext dbContext, Guid id) =>
{
    try
    {
        var skillsToDelete = await dbContext.Skills.FirstOrDefaultAsync(x => x.Id == id);

        if (skillsToDelete == null)
        {
            return Results.NotFound("Product was not found.");
        }

        dbContext.Skills.Remove(skillsToDelete);

        await dbContext.SaveChangesAsync();
        return Results.Ok(skillsToDelete);

    }
    catch
    {
        return Results.Problem("Server Error");
    }

});

//------------------------ Projects ------------------------

//Read, All 
app.MapGet("/projects", async (ApplicationDbContext dbContext) =>
{
    try
    {
        var projects = await dbContext.Projects.ToListAsync();
        return Results.Ok(projects);
    }
    catch
    {
        return Results.Problem("Server Error");
    }
});

//Read, By ID
app.MapGet("/projects/{id}", async (ApplicationDbContext dbContext, Guid id) =>
{
    try
    {
        var product = await dbContext.Projects.FirstOrDefaultAsync(x => x.Id == id);
        if (product != null)
        {
            return Results.Ok(product);
        }
        else
        {
            return Results.NotFound("No product with this ID was found");
        }

    }
    catch
    {
        return Results.Problem("Server Error");
    }
});

//Create
app.MapPost("/project", async (ApplicationDbContext dbContext, Project projects) =>
{
    try
    {
        await dbContext.Projects.AddAsync(projects);
        await dbContext.SaveChangesAsync();

        return Results.Ok();
    }
    catch (Exception)
    {
        return Results.StatusCode(500);
    }
});


//Update
app.MapPut("/projects/{id}", async (ApplicationDbContext dbContext, Guid id, Project project) =>
{
    try
    {
        var projectsToUpdate = await dbContext.Projects.FirstOrDefaultAsync(x => x.Id == id);

        if (projectsToUpdate != null)
        {
            projectsToUpdate.Title = project.Title;
            projectsToUpdate.GithubLink = project.GithubLink;
            projectsToUpdate.Description = project.Description;
            


            await dbContext.SaveChangesAsync();
            return Results.Ok(projectsToUpdate);
        }
        else
        {
            return Results.NotFound("No product with this ID was found");
        }

    }
    catch
    {
        return Results.Problem("Server Error");

    }

});

//Delete
app.MapDelete("/projects/{id}", async (ApplicationDbContext dbContext, Guid id) =>
{
    try
    {
        var projectToDelete = await dbContext.Projects.FirstOrDefaultAsync(x => x.Id == id);

        if (projectToDelete == null)
        {
            return Results.NotFound("Product was not found.");
        }

        dbContext.Projects.Remove(projectToDelete);

        await dbContext.SaveChangesAsync();
        return Results.Ok(projectToDelete);

    }
    catch
    {
        return Results.Problem("Server Error");
    }

});

app.Run();
