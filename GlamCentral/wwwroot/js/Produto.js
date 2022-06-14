//navbarDropdownProduto.onclick = function () {
//    window.location.href = "https://localhost:44375/Funcionario/Produto/Index";
//}

const input_fileA = $("#inputA");
const input_fileB = $("#inputB");


input_fileA.change(function () {

    var formulario = new FormData();
    var imagemId = 0;
    formulario.append("imagem", imagemId);

    var binario = $(this)[0].files[0];
    formulario.append("file", binario);
    
    var token = $('input[name="__RequestVerificationToken"]').val();
    formulario.append("__RequestVerificationToken", token);


    var imagem = $("#img-inputA");
    var btnExcluir = $("#btn-deleteA");

    imagem.attr("src", "/img/loading.gif");

    
    fetch('/Funcionario/Imagem/Armazenar', { method: "POST", body: formulario })
        .then((response) => {
            return response.json();
        })
        .then((data) => {
            var caminho = data.caminho;
            imagem.attr("src", caminho);
            btnExcluir.removeClass("btn-ocultar");
        })
        .catch((error) => {
            alert(error);
            imagem.attr("src", "/img/imagem-padrao.png");
        });
});

input_fileB.change(function () {

    var formulario = new FormData();
    var imagemId = 1;
    formulario.append("imagem", imagemId);

    var binario = $(this)[0].files[0];
    formulario.append("file", binario);

    var token = $('input[name="__RequestVerificationToken"]').val();
    formulario.append("__RequestVerificationToken", token);


    var imagem = $("#img-inputB");
    var btnExcluir = $("#btn-deleteB");

    imagem.attr("src", "/img/loading.gif");


    fetch('/Funcionario/Imagem/Armazenar', { method: "POST", body: formulario })
        .then((response) => {
            return response.json();
        })
        .then((data) => {
            var caminho = data.caminho;
            imagem.attr("src", caminho);
            btnExcluir.removeClass("btn-ocultar");
        })
        .catch((error) => {
            alert("Erro no envio do arquivo");
            imagem.attr("src", "/img/imagem-padrao.png");
        });
});

function ExcluirA() {
    var inputFile = $("#inputA");
    var btnExcluir = $("#btn-deleteA");

    var formulario = new FormData();
    var imagemId = 0;
    formulario.append("imagem", imagemId);

    var token = $('input[name="__RequestVerificationToken"]').val();
    formulario.append("__RequestVerificationToken", token);

    var imagem = $("#img-inputA");
    imagem.attr("src", "/img/loading.gif");

    fetch('/Funcionario/Imagem/Deletar', { method: "POST", body: formulario })
        .then(() => {
            imagem.attr("src", "/img/imagem-padrao.png");
            btnExcluir.addClass("btn-ocultar");
            inputFile.val("");
        })
}

function ExcluirB() {

    var inputFile = $("#inputB");
    var btnExcluir = $("#btn-deleteB");

    var formulario = new FormData();
    var imagemId = 1;
    formulario.append("imagem", imagemId);

    var token = $('input[name="__RequestVerificationToken"]').val();
    formulario.append("__RequestVerificationToken", token);

    var imagem = $("#img-inputB");
    imagem.attr("src", "/img/loading.gif");

    fetch('/Funcionario/Imagem/Deletar', { method: "POST", body: formulario })
        .then(() => {
            imagem.attr("src", "/img/imagem-padrao.png");
            btnExcluir.addClass("btn-ocultar");
            inputFile.val("");
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