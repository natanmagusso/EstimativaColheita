$(document).ready(function () {
    $("#IdContratoColheitaRealizada").change(function () {
        var value = $("#IdContratoColheitaRealizada option:selected").val();

        if (value !== "" || value !== undefined) {
            ListarTalhoesColheitaRealizada(value);
        }
    });
})

function ListarTalhoesColheitaRealizada(value) {
    var url = "/ColheitaRealizada/ListarTalhoesColheitaRealizada";
    var data = { contrato: value }

    $("#IdTalhaoColheitaRealizada").empty();

    $.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        data: data
    }).done(function (data) {

        console.log(data);
        if (data.resultado.length > 0) {
            var dadosResult = data.resultado;

            $("#IdTalhaoColheitaRealizada").append('<option value="" disabled selected>Selecione...</option>');

            $.each(dadosResult, function (indice, item) {
                var option = "";

                option = '<option value="' + item["id"] + '">' + item["descricaoCompleta"] + '</option>';

                $("#IdTalhaoColheitaRealizada").append(option);
            })
        }
    })
}