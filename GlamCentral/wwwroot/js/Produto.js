$(document).ready(function () {
    AjaxUploadImagemProduto();
});

//navbarDropdownProduto.onclick = function () {
//    window.location.href = "https://localhost:44375/Funcionario/Produto/Index";
//}

const image_input = document.querySelector(".input-file");

image_input.addEventListener("change", function () {
    //const reader = new FileReader();
    //reader.addEventListener("load", () => {
    //    const uploaded_image = reader.result;
    //    document.querySelector(".img-upload").style.backgroundImage = `url(${uploaded_image})`;
    //});
    //reader.readAsDataURL(this.files[0]);

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

function AjaxUploadImagemProduto() {

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

    document.getElementById('img-input').change(function () {
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