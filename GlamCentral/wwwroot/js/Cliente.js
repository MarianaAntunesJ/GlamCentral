////navbarDropdownCliente.onclick = function () {
////    window.location.href = "https://localhost:44375/Funcionario/Cliente/Index";
////}

function DropDownPequisaResponsavel() {
    let pesquisa = document.getElementById("responsavelSelected").value;
    let qntCaracteres = pesquisa.length;

    if (qntCaracteres === 3) {
        PesquisaTresCaracteres(pesquisa);
    }
    else if (qntCaracteres > 3) {
        PesquisaQuatroOuMaisCaracteres(pesquisa, qntCaracteres);
    }
    else {
        LimpaOptionsResponsavel();
    }
}

function PesquisaTresCaracteres(pesquisa) {
    let formulario = new FormData();

    let token = $('input[name="__RequestVerificationToken"]').val();
    formulario.append("__RequestVerificationToken", token);

    formulario.append("nomeTresCaracteres", pesquisa);

    fetch('/Funcionario/Cliente/BuscaClientes', { method: "POST", body: formulario })
        .then((response) => {
            return response.json();
        })
        .then((clientes) => {
            PreencheClientes(clientes);
        });
}

function PesquisaQuatroOuMaisCaracteres(pesquisa, qntCaracteres) {
    let clientes = [];
    clientes = GetListaClientes(clientes, pesquisa, qntCaracteres)

    PreencheClientes(clientes);
}

function GetListaClientes(clientes, pesquisa, qntCaracteres) {
    let responsavel = GetResponsavel();

    for (let i = 0; i < responsavel.tamanhoLista; i++) {
        let option = responsavel.campoResponsavel.options[i].value;
        let caracteres = option.substring(0, qntCaracteres);

        if (caracteres === pesquisa) {
            clientes.push(option);
        }
    }
    return clientes;
}

function PreencheClientes(clientes) {
    let responsavel = GetResponsavel();

    LimpaOptionsResponsavel(responsavel.campoResponsavel, responsavel.tamanhoLista);

    clientes.forEach(function (item) {
        let option = document.createElement('option');
        option.value = item;
        responsavel.campoResponsavel.appendChild(option);
    });
}

function LimpaOptionsResponsavel() {
    let responsavel = GetResponsavel();

    for (let i = 0; i < responsavel.tamanhoLista; i++) {
        responsavel.campoResponsavel.options[i].value = null;
    }
}

function GetResponsavel() {
    let responsavel = document.getElementById('clientes');

    return {
        campoResponsavel: responsavel,
        tamanhoLista: responsavel.options.length
    };
}