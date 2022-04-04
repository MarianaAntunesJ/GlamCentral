$(document).ready(function () {
    google.charts.load('current', { packages: ['corechart', 'bar'] });
    GerarGraficos();
});

function GerarGraficoCategoria() {
    google.charts.setOnLoadCallback(CarregaDados);
}

function GerarGraficoQuantidade() {
    google.charts.setOnLoadCallback(CarregaDadosProdutoQuantidade);
}

function GerarGraficos() {
    GerarGraficoCategoria();
    GerarGraficoQuantidade();
};

function CarregaDados() {
    var categoriaId = document.getElementById("categoria").value;
    var urlBase = window.location.protocol + "//" + window.location.host + window.location.pathname + "CategoriaJson?" + "categoriaId=" + categoriaId;
    $.ajax({
        url: urlBase,
        dataType: "json",
        type: "GET",
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            toastr.error(err.message);
        },
        success: function (data) {
            GraficoProduto(data);
            return false;
        }
    });
    return false;
};

function CarregaDadosProdutoQuantidade() {
    var min = document.getElementById("min").value;
    var max = document.getElementById("max").value;
    var urlBase = window.location.protocol + "//" + window.location.host + window.location.pathname + "QuantidadeJson?" + "min=" + min + "&max=" + max;
    $.ajax({
        url: urlBase,
        dataType: "json",
        type: "GET",
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            toastr.error(err.message);
        },
        success: function (data) {
            GraficoPorQuantidade(data);
            return false;
        }
    });
    return false;
}

function GraficoProduto(data) {
    var dataArray = [
        ['Nome', 'Quantidade']
    ];
    $.each(data, function (i, item) {
        dataArray.push([item.nome, item.quantidade]);
    });
    var data = google.visualization.arrayToDataTable(dataArray);
    var options = {
        hAxis: {
            title: 'Quantidade'
        },
        title: 'Total de Produtos na categoria selecionada',
        chartArea: {
            width: '50%'
        }
    };
    var chart = new google.visualization.PieChart(document.getElementById('produtosCategorias'));
    chart.draw(data, options);
    return false;
}

function GraficoPorQuantidade(data) {
    var dataArray = [
        ['Produto', 'Quantidade']
    ];
    $.each(data, function (i, item) {
        dataArray.push([item.nome, item.quantidade]);
    });
    var data = google.visualization.arrayToDataTable(dataArray);
    var options = {
        is3D: true,
        title: 'Quantidade Total de Produtos',
        chartArea: {
            width: '50%'
        }
    };
    var chart = new google.visualization.ColumnChart(document.getElementById('produtosQuantidade'));
    chart.draw(data, options);
    return false;
}