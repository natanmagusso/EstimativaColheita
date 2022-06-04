$(document).ready(function () {
    $("#IdConAp").change(function () {
        var value = $("#IdConAp option:selected").val();

        if (value != "" || value != undefined) {
            ListarTalhoesApontamento(value);
        }
    });
})

function ListarTalhoesApontamento(value) {
    var url = "/ColheitaRealizada/ListarTalhoesApontamento";
    var data = { contrato: value }

    $("#IdTalAp").empty();

    $.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        data: data
    }).done(function (data) {
        console.log(data);
        if (data.resultado.length > 0) {
            var dadosResult = data.resultado;

            $("#IdTalAp").append('<option value="" disabled selected>Selecione...</option>');

            $.each(dadosResult, function (indice, item) {
                var option = "";

                option = '<option value="' + item["id"] + '">' + item["descricaoCompleta"] + '</option>';

                $("#IdTalAp").append(option);
            })
        }
    })
}