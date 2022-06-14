/*btnSave.click(function () {
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

});*/