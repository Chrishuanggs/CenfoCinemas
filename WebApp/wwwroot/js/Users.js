// js que maneja todo el comportamiento de la vista de usuarios

//definir una clase JS, usando prototype

function UsersViewController() {

    this.ViewName = "Users";
    this.ApiEndPointName = "User";

    //Metodo constructor

    this.InitView = function () {

        console.log("User Init View --> ok");
        this.LoadTable();
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
    }
}

$(document).ready(function () {
    var vc = new UsersViewController();
    vc.InitView();
})