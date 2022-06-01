//navbarDropdownProduto.onclick = function () {
//    window.location.href = "https://localhost:44375/Funcionario/Produto/Index";
//}

const input_fileA = $("#inputA");
const input_fileB = $("#inputB");


input_fileA.change(function () {

    var formulario = new FormData();
    var imagemId = 1;
    var binario = $(this)[0].files[0];
    formulario.append("file", binario);
    formulario.append("imagem", imagemId)

    var imagem = $("#img-inputA");
    var btnExcluir = $("#btn-deleteA");

    imagem.attr("src", "/img/loading.gif");


    fetch('/Funcionario/Imagem/Armazenar', { method: "POST", body: formulario })
        .then((data) => {
            var caminho = data.caminho;
            imagem.attr("src", caminho);
            btnExcluir.removeClass("btn-ocultar");
        })
        .catch((error) => {
            alert("Erro no envio do arquivo");
            imagem.attr("src", "/img/imagem-padrao.png");
        });

    //$.ajax({
    //    type: "POST",
    //    url: "/Funcionario/Imagem/Armazenar",
    //    dataType: 'json',
    //    data: {
    //        file: formulario,
    //        id: imagemId
    //    },
    //    processData: false,
    //    success: function (data) {
    //        var caminho = data.caminho;
    //        imagem.attr("src", caminho);
    //        btnExcluir.removeClass("btn-ocultar");
    //    },
    //    error: function () {
    //        alert("Erro no envio do arquivo");
    //        imagem.attr("src", "/img/imagem-padrao.png");
    //    }
    //})
});

input_fileB.change(function () {

    var formulario = new FormData();
    var imagemId = 1;
    var binario = $(this)[0].files[0];
    formulario.append("file", binario);

    var imagem = $("#img-inputB");
    var btnExcluir = $("#btn-deleteB");

    imagem.attr("src", "/img/loading.gif");

    $.ajax({
        type: "POST",
        url: "/Funcionario/Imagem/Armazenar",
        data: {
            formulario,
            imagemId
        },
        contentType: false,
        processData: false,
        success: function (data) {
            var caminho = data.caminho;
            imagem.attr("src", caminho);
            btnExcluir.removeClass("btn-ocultar");
        },
        error: function () {
            alert("Erro no envio do arquivo");
            imagem.attr("src", "/img/imagem-padrao.png");
        }
    })
});

function ExcluirA() {
    var imagem = $("#img-inputA");
    var imagemId = "0";
    var btnExcluir = $("#btn-deleteA");
    var inputFile = $("#inputA");

    $.ajax({
        type: "GET",
        url: "/Funcionario/Imagem/Deletar",
        data: imagemId,
        error: function () {
        },
        success: function (data) {
            imagem.attr("src", "/img/imagem-padrao.png");
            btnExcluir.addClass("btn-ocultar");l
            inputFile.val("");
        }
    })
}

function ExcluirB() {
    var imagem = $("#img-inputB");
    var imagemId = "1";
    var btnExcluir = $("#btn-deleteB");
    var inputFile = $("#inputB");

    $.ajax({
        type: "GET",
        url: "/Funcionario/Imagem/Deletar",
        data: imagemId,
        error: function () {
        },
        success: function (data) {
            imagem.attr("src", "/img/imagem-padrao.png");
            btnExcluir.addClass("btn-ocultar");
            inputFile.val("");
        }
    })
}

//const boxes = document.querySelectorAll('.img-thumbnail');

//boxes.forEach(box => {
//    box.addEventListener('click', function handleClick(event) {
//        console.log('box clicked', event);
//        alert("aaaaaaaaB")
//        test(box.value)
//    });
//});

//function test(id) {
//    console.log(id);
//    var formulario = new FormData();
//    var binario = $(this)[0].files[0];
//    formulario.append("file", binario);

//    var imagem = $(".img-input1");
//    var btnExcluir = $(".btn-deleteA");

//    imagem.attr("src", "/img/loading.gif");

//    $.ajax({
//        type: "POST",
//        url: "/Funcionario/Imagem/Armazenar",
//        data: formulario,
//        contentType: false,
//        processData: false,
//        success: function (data) {
//            var caminho = data.caminho;
//            imagem.attr("src", caminho);
//            campoHidden.value = caminho;
//            btnExcluir.removeClass("btn-ocultar");
//        },
//        error: function () {
//            alert("Erro no envio do arquivo");
//            imagem.attr("src", "/img/imagem-padrao.png");
//        }
//    })
//}