## Scaffold
 * dotnet ef dbcontext scaffold "server=musicagrupos.database.windows.net;database=GrupoB;user=as;password=P0t@t0P0t@t0" Microsoft.EntityFrameworkCore.SqlServer -o Models --no-pluralize --use-database-names --force

## program.cs
* builder.Services.AddDbContext<GrupoBContext>(options => options.UseSqlServer("server=musicagrupos.database.windows.net;database=GrupoB;user=as;password=P0t@t0P0t@t0"));

## Datatables
* https://datatables.net/
* https://cdnjs.com/libraries/moment.js

## FancyTable
* npm install @myspace-nu/jquery.fancytable --save
* https://www.jqueryscript.net/table/sorting-filtering-pagination-fancytable.html
