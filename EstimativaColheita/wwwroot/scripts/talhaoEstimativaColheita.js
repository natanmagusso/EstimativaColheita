$(document).ready(function () {
    $("#IdContratoEstimativaColheita").change(function () {
        var value = $("#IdContratoEstimativaColheita option:selected").val();

        if (value !== "" || value !== undefined) {
            ListarTalhoesEstimativaColheita(value);
        }
    });
})

function ListarTalhoesEstimativaColheita(value) {
    var url = "/EstimativaColheita/ListarTalhoesEstimativaColheita";
    var data = { contrato: value }

    $("#IdTalhaoEstimativaColheita").empty();

    $.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        data: data
    }).done(function (data) {

        console.log(data);
        if (data.resultado.length > 0) {
            var dadosResult = data.resultado;

            $("#IdTalhaoEstimativaColheita").append('<option value="" disabled selected>Selecione...</option>');

            $.each(dadosResult, function (indice, item) {
                var option = "";

                option = '<option value="' + item["id"] + '">' + item["descricaoCompleta"] + '</option>';

                $("#IdTalhaoEstimativaColheita").append(option);
            })
        }
    })
}