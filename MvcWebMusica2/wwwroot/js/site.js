$(function () {
    $("#tabla-indice").fancyTable({
        sortColumn: 0, // column number for initial sorting
        sortOrder: 'descending', // 'desc', 'descending', 'asc', 'ascending', -1 (descending) and 1 (ascending)
        sortable: true,
        pagination: true, // default: false
        searchable: true,
        globalSearch: true,
        inputStyle: "",
        inputPlaceholder: "Buscar..."
    });
});
