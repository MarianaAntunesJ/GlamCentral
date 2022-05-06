$(document).ready(function () {
    if (document.getElementById("pesquisa") != null && document.getElementById("pesquisa") != undefined) {
        var pesquisa = document.getElementById("pesquisa");
        pesquisa.selectionStart = pesquisa.value.length;
        pesquisa.focus();
    }

    Pergunta();
    AjaxUploadImagemProduto();
    MudarOrdenacao();
    SelecionaDropDownOrd();
});


function Pergunta() {
    $(".btn-red").click(function (e) {
        var resultado = confirm("Tem certeza que deseja realizar esta operação?");

        if (resultado == false) {
            e.preventDefault();
        }
    });
}

function Pesquisar() {
    var Pesquisa = document.getElementById("pesquisa").value;

    if (Pesquisa.length >= 3) {
        Redirecionar();
    }
    if (Pesquisa.length == 0) {
        Redirecionar();
    }
}

function SelecionaDropDownOrd() {
    var queryString = new URLSearchParams(window.location.search);
    if (queryString.has("ordenacao")) {
        document.getElementById("ordenacao").value = queryString.get("ordenacao");
    }

    if (queryString.has("status")) {
        document.getElementById("status").value = queryString.get("status");
    }
}

function MudarOrdenacao() {
    $(".ordenacao").change(function () {
        Redirecionar();
    });

    $(".status").change(function () {
        Redirecionar();
    });
}

function Redirecionar() {
    var Pagina = 1;
    var Pesquisa = ""
    var Ordenacao = $(".ordenacao").val();
    var Status = $(".status").val();

    if (document.getElementById("pesquisa") != null && document.getElementById("pesquisa") != undefined) {
        var Pesquisa = document.getElementById("pesquisa").value != undefined ? document.getElementById("pesquisa").value : "";
    }

    var queryString = new URLSearchParams(window.location.search);

    if (queryString.has("pagina")) {
        Pagina = queryString.get("pagina");
    }

    /*if (queryString.has("pesquisa")) {
        Pesquisa = queryString.get("pesquisa");
    }*/

    var URL = window.location.protocol + "//" + window.location.host + window.location.pathname;
    var URLComParametros = URL + "?pagina=" + Pagina + "&pesquisa=" + Pesquisa + "&ordenacao=" + Ordenacao + "&status=" + Status + "#ordenacao";
    window.location.href = URLComParametros;
}

function AjaxUploadImagemProduto() {
    $(".img-upload").click(function () {
        $(this).parent().parent().find(".input-file").click();
    });

    $(".btn-imagem-excluir").click(function () {
        var campoHidden = $(this).parent().find("input[name=imagem]");
        var imagem = $(this).parent().find(".img-upload");
        var btnExcluir = $(this).parent().find(".btn-imagem-excluir");
        var inputFile = $(this).parent().find(".input-file");

        $.ajax({
            type: "GET",
            url: "/Funcionario/Imagem/Deletar?caminho=" + campoHidden.val(),
            error: function () {
            },
            success: function (data) {
                imagem.attr("src", "/img/imagem-padrao.png");
                btnExcluir.addClass("btn-ocultar");
                campoHidden.val("");
                inputFile.val("");
            }
        })

    });

    $(".input-file").change(function () {
        var formulario = new FormData();
        var binario = $(this)[0].files[0];
        formulario.append("file", binario);

        var campoHidden = $(this).parent().find("input[name=imagem]");
        var imagem = $(this).parent().find(".img-upload");
        var btnExcluir = $(this).parent().find(".btn-imagem-excluir");

        imagem.attr("src", "/img/loading.gif");

        $.ajax({
            type: "POST",
            url: "/Funcionario/Imagem/Armazenar",
            data: formulario,
            contentType: false,
            processData: false,
            error: function () {
                alert("Erro no envio do arquivo");
                imagem.attr("src", "/img/imagem-padrao.png");
            },
            success: function (data) {
                var caminho = data.caminho;
                imagem.attr("src", caminho);
                campoHidden.val(caminho);
                btnExcluir.removeClass("btn-ocultar");
            }
        })
    });
}
