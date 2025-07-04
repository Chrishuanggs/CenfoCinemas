// js que maneja todo el comportamiento de la vista de usuarios

//definir una clase JS, usando prototype

function UsersViewController() {

    this.ViewName = "Users";
    this.ApiEndPointName = "User";

    //Metodo constructor

    this.InitView = function () {

        console.log("User Init View --> ok");
        this.LoadTable();

        //Asociar el evento al boton
        $('#btnCreate').click(function () {
            var vc = new UsersViewController();
            vc.Create();

        })

        //Asociar el evento al boton
        $('#btnUpdate').click(function () {
            var vc = new UsersViewController();
            vc.Update();

        })

        //Asociar el evento al boton
        $('#btnDelete').click(function () {
            var vc = new UsersViewController();
            vc.Delete();

        })
    }

    //Metodo para la carga de una tabla
    this.LoadTable = function () {

        //URL Del API
        //https://localhost:7163/api/User/RetrieveAll

        var ca = new ControlActions();
        var service = this.ApiEndPointName + "/RetrieveAll";

        var urlService = ca.GetUrlApiService(service)

        var columns = [];
        columns[0] = { 'data': 'id' }
        columns[1] = { 'data': 'userCode' }
        columns[2] = { 'data': 'name' }
        columns[3] = { 'data': 'email' }
        columns[4] = { 'data': 'birthDate' }
        columns[5] = { 'data': 'status' }

        //Invocamos a datatables para convertir la tabla mas simple en html en una tabla mas robusta
        $("#tblUsers").dataTable({
            "ajax": {
                url: urlService,
                "dataSrc": ""

            },
            columns:columns
        })
        //Asignar eventos de carga de datos o binding segun el clic en la tabla
        $('#tblUsers tbody').on('click', 'tr', function () {

            //extraemos la fila
            var row = $(this).closest('tr');

            //extraemos el dto
            //Esto nos devuelve el json de la fila seleccionada por el usuario
            //Segun la data devuelta por el API
            var userDTO = $('#tblUsers').DataTable().row(row).data();

            //Binding con el form
            $('#txtId').val(userDTO.id);
            $('#txtUserCode').val(userDTO.userCode);
            $('#txtname').val(userDTO.name);
            $('#txtEmail').val(userDTO.email);
            $('#txtStatus').val(userDTO.status);

            //Fecha tiene un formato

            var onlyDate = userDTO.birthDate.split("T");
            $('#txtBirthDate').val(onlyDate[0]);

        })
    }

    this.Create = function () {

        var userDTO = {};
        //Atributos con valores default, que son controlados por el API
        userDTO.id = 0;
        userDTO.created = "2025-01-01";
        userDTO.updated = "2025-01-01";

        //Valores capturados en pantalla
        userDTO.userCode = $('#txtUserCode').val();
        userDTO.name = $('#txtName').val();
        userDTO.email = $('#txtEmail').val();
        userDTO.status = $('#txtStatus').val();
        userDTO.birthDate = $('#txtBirthDate').val();
        userDTO.passwowrd = $('#txtPassword').val();

        //Enviar la data al API
        var ca = new ControlActions();
        var urlService = this.ApiEndPointName + "/Create";

        ca.PostToAPI(urlService, userDTO, function () {
            //recargo de la tabla
            $('#tblUsers').DataTable.ajax.relod();
        })

    }
}

$(document).ready(function () {
    var vc = new UsersViewController();
    vc.InitView();
})