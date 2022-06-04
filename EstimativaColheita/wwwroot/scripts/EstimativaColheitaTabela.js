$(document).ready(function () {
    $("#IdTalFiltro").change(function () {
        var talhao = $("#IdTalFiltro option:selected").val();

        if (talhao != "") {
            var valueContrato = $("#IdConFiltro option:selected").val();
            var valueTalhao = $("#IdTalFiltro option:selected").val();

            Index(valueContrato, valueTalhao);
        }
    })

    /*$("#IdConFiltro").change(function(){
        var estimativas = $("#estimativas");
        estimativas.html("<table id='tableEstimativas' class='table table-sm table-hover table caption-top'><caption /><thead><tr><th>Lançamento</th><th>Contrato</th><th>Talhão</th><th>Caixas</th><th>Motivo</th><th>Informações</th></tr></thead></table><div class='card-footer'><div><h6><span card-title style='color:black'>Estimado: 0</span><span>&nbsp;|&nbsp;</span><span card-title style='color:green'>Colhido: 0</span><span>&nbsp;|&nbsp;</span><span card-title style='color:blue'>Saldo a colher: 0</span></h6></div></div>");
    })*/
});

function Index(valueContrato, valueTalhao) {
    $.get("/EstimativaColheita/ListarEstimativas?contrato=" + valueContrato + "&talhao=" + valueTalhao, {}, function (data) {
        var estimativas = $("#estimativas");
        estimativas.html("");
        estimativas.html(data);
    });
}