var socket;

window.onload = function () {
    socket = new WebSocket("ws://172.27.108.170:9001");
    socket.onopen = function () {
        Exito("Nos conectamos al socket");
    }
    socket.onclose = function () {
        Error("Se cerró la conexión");
    }
    socket.onerror = function () {
        Error("Ocurrió un error en el socket");
    }

    socket.onmessage = function (respuesta) {
        var mensaje = respuesta.data;

        if (mensaje == "eliminarProducto" || mensaje == "guardarProducto") {
            BuscarDatos(indicePagina, indiceBloque);
        }        
    }
    ListarProductos();
    previewImage("fotoParam", "imgFoto");
}

function ListarProductos() {
    pintar({
        url: "Productos/ListarProductos",
        cabeceras: ["Descripcion Prod", "Precio Prod", "Stock Prod"],
        propiedades: ["description", "preciocadena", "stockcadena"],
        editar: true,
        eliminar: true,
        propiedadId: "iidproducto"
    })
}

function BuscarDatos(indPag = 0, indBloq = 0) {
    var frmBusqueda = document.getElementById("frmBusqueda");
    var frn = new FormData(frmBusqueda);
    fetchPost("Productos/ListarProductos", "json", frn, function (respuesta) {
        indicePagina = indPag;
        indiceBloque = indBloq;
        document.getElementById("divContenedorTabla").innerHTML = generarTabla(respuesta)
        configurarPaginacion();
    })
}

function Editar(id) {
    document.getElementById("btnNuevo").click();
    LimpiarDatos("frmProducto");
    document.getElementById("imgFoto").src = "";
    recuperarGenerico("Productos/RecuperarProducto/?id=" + id, "frmProducto")
}

function Eliminar(id) {
    Confirmacion("Confirmación", "Deseas eliminar este registro?", function () {
        fetchGet("Productos/EliminarProducto/?id=" + id, "text", function (respuesta) {
            if (respuesta == 1) {
                socket.send("eliminarProducto");
                Exito("Se eliminó correctamente!");
                BuscarDatos(indicePagina, indiceBloque);
            } else {
                Error("Ocurrió un error");
            }
        })
    })
}

function Guardar() {
    var frmProducto = document.getElementById("frmProducto");
    var frn = new FormData(frmProducto);

    Confirmacion("Confirmación", "Deseas guardar este registro?", function () {
        fetchPost("Productos/GuardarProducto", "text", frn, function (respuesta) {
            if (respuesta == 1) {
                socket.send("guardarProducto");
                Exito("Se guardó correctamente!");
                document.getElementById("btnCancelar").click();
                BuscarDatos(indicePagina, indiceBloque);
            } else {
                Error("Ocurrió un error");
            }
        })
    })    
}

function Nuevo() {
    LimpiarDatos("frmProducto");
    document.getElementById("imgFoto").src = "";
}