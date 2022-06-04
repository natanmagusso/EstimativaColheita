$(document).ready(function () {
    $("#IdConEstimado").change(function () {
        var value = $("#IdConEstimado option:selected").val();

        if (value != "" || value != undefined) {
            ListarTalhoesEstimado(value);
        }
    });
})

function ListarTalhoesEstimado(value) {
    var url = "/EstimativaColheita/ListarTalhoesEstimado";
    var data = { contrato: value }

    $("#IdTalEstimado").empty();

    $.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        data: data
    }).done(function (data) {
        console.log(data);
        if (data.resultado.length > 0) {
            var dadosResult = data.resultado;

            $("#IdTalEstimado").append('<option value="" disabled selected>Selecione...</option>');

            $.each(dadosResult, function (indice, item) {
                var option = "";

                option = '<option value="' + item["id"] + '">' + item["descricaoCompleta"] + '</option>';

                $("#IdTalEstimado").append(option);
            })
        }
    })
}