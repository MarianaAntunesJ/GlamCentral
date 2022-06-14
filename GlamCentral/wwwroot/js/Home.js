$(document).ready(function () {
    if (document.getElementById("pesquisa") != null && document.getElementById("pesquisa") != undefined) {
        var pesquisa = document.getElementById("pesquisa");
        pesquisa.selectionStart = pesquisa.value.length;
        pesquisa.focus();
    }

    Pergunta();
    MudarOrdenacao();
    SelecionaDropDownOrd();
});

navbarDropdownAgenda.onclick = function () {
    window.location.href = "https://localhost:44375/Funcionario/Agenda/Index";
};

navbarDropdownPagamento.onclick = function () {
    window.location.href = "https://localhost:44375/Funcionario/Pagamento/Index";
};

navbarDropdownProcedimento.onclick = function () {
    window.location.href = "https://localhost:44375/Funcionario/Procedimento/Index";
};

navbarDropdownProduto.onclick = function () {
    window.location.href = "https://localhost:44375/Funcionario/Produto/Index";
};

navbarDropdownCliente.onclick = function () {
    window.location.href = "https://localhost:44375/Funcionario/Cliente/Index";
};

navbarDropdownFuncionario.onclick = function () {
    window.location.href = "https://localhost:44375/Funcionario/Funcionario/Index";
};


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
