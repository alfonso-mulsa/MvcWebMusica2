$(function () {
    $(".tabla-indice").fancyTable({
        sortColumn: 0, // column number for initial sorting
        sortOrder: 'ascending', // 'desc', 'descending', 'asc', 'ascending', -1 (descending) and 1 (ascending)
        sortable: true,
        pagination: true, // default: false
        paginationClass: "btn btn-primary",
        paginationClassActive: "active",
        pagClosest: 3,
        perPage: 15,
        searchable: true,
        globalSearch: true, // use global search for all columns
        //globalSearchExcludeColumns: [2, 5]// exclude column 2 & 5
        //matchCase: true, // use case sensitive search
        //exactMatch: true, // use exact match
        inputStyle: "",
        inputPlaceholder: "Buscar...",
        //sortFunction: function (a, b, o) {
        //    if (o.sortAs[o.sortColumn] == 'numeric') {
        //        return ((o.sortOrder > 0) ? parseFloat(a) - parseFloat(b) : parseFloat(b) - parseFloat(a));
        //    } else {
        //        return ((a < b) ? -o.sortOrder : (a > b) ? o.sortOrder : 0);
        //    }
        //},
    });
});
