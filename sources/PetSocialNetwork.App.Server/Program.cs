var app = WebApplication
    .CreateBuilder(args)
    .Build();

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.Run();