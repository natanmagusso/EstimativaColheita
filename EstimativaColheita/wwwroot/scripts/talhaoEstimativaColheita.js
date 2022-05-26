$(document).ready(function () {
    $("#IdContrato").change(function () {
        var value = $("#IdContrato option:selected").val();

        if (value !== "" || value !== undefined) {
            ListarTalhoes(value);
        }
    });
})

function ListarTalhoes(value) {
    var url = "/EstimativaColheita/ListarTalhoesEstimativaColheita";
    var data = { contrato: value }

    $("#IdTalhao").empty();

    $.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        data: data
    }).done(function (data) {

        console.log(data);
        if (data.resultado.length > 0) {
            var dadosResult = data.resultado;

            $("#IdTalhao").append('<option value="" disabled selected>Selecione...</option>');

            $.each(dadosResult, function (indice, item) {
                var option = "";

                option = '<option value="' + item["id"] + '">' + item["descricaoCompleta"] + '</option>';

                $("#IdTalhao").append(option);
            })
        }
    })
}