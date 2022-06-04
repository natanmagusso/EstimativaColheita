$(document).ready(function () {
    $("#IdConFiltro").change(function () {
        var value = $("#IdConFiltro option:selected").val();

        if (value != "" || value != undefined) {
            ListarTalhoesEstimadoFiltro(value);
        }
    });
})

function ListarTalhoesEstimadoFiltro(value) {
    var url = "/EstimativaColheita/ListarTalhoesEstimado";
    var data = { contrato: value }

    $("#IdTalFiltro").empty();

    $.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        data: data
    }).done(function (data) {
        console.log(data);
        if (data.resultado.length > 0) {
            var dadosResult = data.resultado;

            $("#IdTalFiltro").append('<option value="" selected>Todos os talhões</option>');

            $.each(dadosResult, function (indice, item) {
                var option = "";

                option = '<option value="' + item["id"] + '">' + item["descricaoCompleta"] + '</option>';

                $("#IdTalFiltro").append(option);
            })
        }
    })
}