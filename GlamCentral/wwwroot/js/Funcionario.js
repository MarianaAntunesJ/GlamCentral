$(".submit-btn-enviar").click(function () {

    var email = $(".user-email").val();

    $.ajax({
        type: "GET",
        url: "Home/Login",
        data: email,
        error: function () {
            //Todo: fazer tratamento de erro
        },
        success: function (data) {
            //Todo: fazer tratamento de sucesso
        }
    })
});
    