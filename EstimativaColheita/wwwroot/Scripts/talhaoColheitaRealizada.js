$(document).ready(function () {
    $("#IdContrato").change(function () {
        var value = $("#IdContrato option:selected").val();

        if (value !== "" || value !== undefined)
        {
            ListarTalhoes(value);
        }
    });
})

function ListarTalhoes(value)
{
    var url = "/ColheitaRealizada/ListarTalhoesColheitaRealizada";
    var data = { contrato: value };

    $("#IdTalhao").empty();

    $.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        data: data
    }).done(function (data) {
        if (data.Resultado.length > 0) {
            var dadosResult = data.Resultado;

            $("#IdTalhao").append('<option value="" disabled selected>Selecione...</option>');

            $.each(dadosResult, function (indice, item) {
                var option = "";

                option = '<option value="' + item["Id"] + '">' + item["DescricaoCompleta"] + '</option>';

                $("#IdTalhao").append(option);
            })
        }
    })
}