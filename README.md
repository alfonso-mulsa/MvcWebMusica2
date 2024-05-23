* Scaffold:
 dotnet ef dbcontext scaffold "server=musicagrupos.database.windows.net;database=GrupoB;user=as;password=P0t@t0P0t@t0" Microsoft.EntityFrameworkCore.SqlServer -o Models --no-pluralize --use-database-names --force

* program.cs:
builder.Services.AddDbContext<GrupoBContext>(options => options.UseSqlServer("server=musicagrupos.database.windows.net;database=GrupoB;user=as;password=P0t@t0P0t@t0"));

* FancyTable:
https://www.jqueryscript.net/table/sorting-filtering-pagination-fancytable.html
