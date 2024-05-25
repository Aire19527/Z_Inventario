// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// ** configuracion Data Table ** 
var idioma = {
    lengthMenu: 'Mostrar _MENU_ Registros',
    zeroRecords: 'No se encontró información - lo sentimos',
    //info: 'Página _PAGE_ de _PAGES_',
    info: 'Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros',
    infoEmpty: 'No hay información disponible',
    search: "Buscar",
    infoFiltered: '(filtrado de un total de _MAX_ registros)',
    emptyTable: "Ningún dato disponible en esta tabla",
    paginate: {
        next: 'Siguiente',
        first: 'Primero',
        last: 'Último',
        previous: 'Anterior'
    },
    buttons: {
        collection: "Coleccion",
        colvis: "Columna Visible",
        colvisRestore: "Restaurar Columnas Visibles",
        copy: "Copiar",
        copyKeys: "presiones inicio + c para copiar la infrocion de la tabla.  click en este mensaje para salir o esc.",
        copySuccess: {
            "_": "Copiado con exito",
            "1": "Fila copiada con exito"
        },
        copyTitle: "Tabla Copiada",
        createState: "Crear estado",
        pageLength: {
            "_": "ver %d filas",
            "-1": "Ver todas las Filas"
        },
        print: "Impresion",
        removeAllStates: "Remover todos los estados",
        removeState: "Remover",
        renameState: "Renombrar",
        savedStates: "Guardar Estado",
        stateRestore: "Restaurar %d",
        updateState: "Actualizar"
    },
    datetime: {
        hours: "hora",
        minutes: "minuto",
        months: {
            "0": "Enero",
            "1": "Febrero",
            "10": "Noviembre",
            "11": "Diciembre",
            "2": "Marzo",
            "3": "Abril",
            "4": "Mayo",
            "5": "Junio",
            "6": "Julio",
            "7": "Agosto",
            "8": "Septiembre",
            "9": "Octubre"
        },
        next: "siguiente",
        previous: "anterior",
        seconds: "segundo",
        weekdays: [
            "Dom",
            "Lun",
            "Mar",
            "Mir",
            "Jue",
            "Vie",
            "sab"
        ]
    },
    loadingRecords: 'Cargando...'
};

function fillDataTable(nombreTabla) {
    $('#' + nombreTabla).DataTable({
        "iDisplayLength": 25,
        "aLengthMenu": [[5, 10, 25, 50, 100, -1], [5, 10, 25, 50, 100, "Todos"]],
        language: idioma,
        responsive: true,
        /*dom: "lfrtiBp",*/
    });
}

function formatNumberWith2Decimal(number) {
    var valor = number.toLocaleString("es-CO", { minimumFractionDigits: 2 });
    return valor;
}

function onlyNumber(evt) {

    // code is the decimal ASCII representation of the pressed key.
    var code = (evt.which) ? evt.which : evt.keyCode;

    if (code == 8) { // backspace.
        return true;
    } else if (code >= 48 && code <= 57) { // is a number.
        return true;
    } else { // other keys.
        return false;
    }
}

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}