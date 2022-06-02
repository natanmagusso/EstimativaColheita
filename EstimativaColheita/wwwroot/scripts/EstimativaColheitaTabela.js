$(document).ready(function () {
    $("#BuscarDados").click(function () {
        var valueContrato = $("#IdContratoEstimativaColheitaTabela option:selected").val();
        var valueTalhao = $("#IdContratoEstimativaColheitaTabela option:selected").val();

        if ((valueContrato !== "" || valueContrato !== undefined) &&  (valueTalhao !== "" || valueTalhao !== undefined)) {
            Index(valueContrato, valueTalhao);
        }
    });
})

function Index(valueContrato, valueTalhao) {
    $.get("/EstimativaColheita/?contrato=" + valueContrato + "&talhao=" + valueTalhao, {}, function (data) {
        var tblEstimativas = $("#tableEstimativas");
        $.each(data, function (index, item) {
            var tr = $("<tr></tr>");
            tr.html(("<td>" + item.DataLancamento + "</td>")
                + " " + ("<td>" + item.Contrato.DescricaoCompleta + "</td>")
                + " " + ("<td>" + item.Talhao.CodigoInterno + "</td>")
                + " " + ("<td>" + item.Caixas + "</td>")
                + " " + ("<td>" + item.EstimativaMotivo.DescricaoCompleta + "</td>"));
            tblEstimativas.append(tr);
        });
    });

    /*var url = "/EstimativaColheita/?contrato=" + valueContrato + "&talhao=" + valueTalhao;

    $.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        success: function (viewModel) {
            $("#tableEstimativas tbody").empty();
            var tbody = "<tbody>";
            tr = "";
            for (var index in viewModel) {
                tr += "<td>" + viewModel[index].DataLancamento + "</td>";
                tr += "<td>" + viewModel[index].Contrato.DescricaoCompleta + "</td>";
                tr += "<td>" + viewModel[index].Talhao.CodigoInterno + "</td>";
                tr += "<td>" + viewModel[index].Caixas + "</td>";
                tr += "<td>" + viewModel[index].EstimativaMotivo.DescricaoCompleta + "</td>";

            }
            $('#tableEstimativas tbody').append(tbody + tr);
        },
    });*/
}