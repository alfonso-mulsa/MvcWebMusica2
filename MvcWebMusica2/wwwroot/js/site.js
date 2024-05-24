$(function () {
    // ***************** Crear y definir propiedades de tabla '.tabla-indice' *******************
    DataTable.datetime('DD/MM/YYYY');
    $('.tabla-indice').DataTable({
        search: {
            return: false
        },
        language: {
            lengthMenu: 'Mostrar _MENU_ entradas por página',
            emptyTable: 'No hay datos disponibles',
            info: 'Mostrando _START_ a _END_ de _TOTAL_ entradas',
            infoEmpty: 'Mostrando 0 a 0 de 0 entradas',
            infoFiltered: '(filtradas de _MAX_ entradas totales)',
            search: '',
            searchPlaceholder: 'Filtrar entradas por...',
            zeroRecords: 'No se encontraron entradas coincidentes.',
        },
        layout: {
            topStart: 'search',
            topEnd: 'pageLength',
            bottomStart: {
                paging: {
                    type: 'full_numbers'
                }
            },
            bottomEnd: 'info',
        },
        colReorder: true,
    });

    // ***************** Carrusel de fondo *******************
    const imagenes = ['./img/fondos/fondo1-byn.jpg', './img/fondos/fondo2-byn.jpg', './img/fondos/fondo3-byn.jpg', './img/fondos/fondo4-byn.jpg'];
    var numFondo = 1;
    setInterval(cambiarFondo, 5000);

    function cambiarFondo() {
        $("body").css("background-image", "url('" + imagenes[numFondo] + "')");
        numFondo += 1;
        if (numFondo === imagenes.length) { numFondo = 0 };
    }

});
