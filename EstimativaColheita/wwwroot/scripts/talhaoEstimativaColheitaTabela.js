$(document).ready(function () {
    $("#IdContratoEstimativaColheitaTabela").change(function () {
        var value = $("#IdContratoEstimativaColheitaTabela option:selected").val();

        if (value !== "" || value !== undefined) {
            ListarTalhoesEstimativaColheitaTabela(value);
        }
    });
})

function ListarTalhoesEstimativaColheitaTabela(value) {
    var url = "/EstimativaColheita/ListarTalhoesEstimativaColheita";
    var data = { contrato: value }

    $("#IdTalhaoEstimativaColheitaTabela").empty();

    $.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        data: data
    }).done(function (data) {
        console.log(data);
        if (data.resultado.length > 0) {
            var dadosResult = data.resultado;

            $("#IdTalhaoEstimativaColheitaTabela").append('<option value="" disabled selected>Selecione o talhão...</option>');

            $.each(dadosResult, function (indice, item) {
                var option = "";

                option = '<option value="' + item["id"] + '">' + item["descricaoCompleta"] + '</option>';

                $("#IdTalhaoEstimativaColheitaTabela").append(option);
            })
        }
    })
}