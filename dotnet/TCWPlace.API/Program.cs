using System.Text.RegularExpressions;
using TCWPlace.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

var place = new PlaceArray();
//var place = new PlaceList();

app.MapGet("/change", (int? x, int? y, string col) =>
{
    if (!x.HasValue || !y.HasValue || col is null)
    {
        return Results.BadRequest(new { msg = "ERROR: missing argument. Valid example: /change?x=5&y=10&col=ff0000 " });
    }

    if (x < 0 || x >= PlaceList.MAX_X)
    {
        return Results.BadRequest(new { msg = $"ERROR: x must be between 0 and {PlaceList.MAX_X - 1}. Valid example: /change?x=5&y=10&col=ff0000" });
    }

    if (y < 0 || y >= PlaceList.MAX_Y)
    {
        return Results.BadRequest(new { msg = $"ERROR: y must be between 0 and {PlaceList.MAX_Y - 1}. Valid example: /change?x=5&y=10&col=ff0000" });
    }

    if (!ColorRegex().IsMatch(col))
    {
        return Results.BadRequest(new { msg = $"ERROR: col must contain exactly 6 hex characters. Valid example: /change?x=5&y=10&col=ff0000" });
    }

    place.Change(x.Value, y.Value, col);

    return Results.Ok(new { msg = "OK" });
});

app.MapGet("/get", () => new { canvas = place.Get() });

app.Run();

partial class Program
{
    [GeneratedRegex("[0-9a-f]{6}")]
    private static partial Regex ColorRegex();
}