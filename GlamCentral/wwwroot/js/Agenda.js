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

$(document).ready(function () {
    var events = [];
    var selectedEvent = null;

    //selectOverlap: function(event) {
    //			return event.rendering === 'background';
    //		}

    FetchEventAndRenderCalendar();
    function FetchEventAndRenderCalendar() {
        events = [];
        $.ajax({
            type: "GET",
            url: "/Funcionario/Agenda/Buscar",
            success: function (data) {
                $.each(data, function (i, v) {
                    events.push({
                        Id: v.id,
                        title: v.subject,
                        description: v.description,
                        start: moment(v.start),
                        end: v.end != null ? moment(v.end) : null,
                        color: v.themeColor,
                        allDay: v.isFullDay
                    });
                })

                GenerateCalender(events);
            },
            error: function (error) {
                alert('failed');
            }
        })
    }

    function GenerateCalender(events) {
        $('#calender').fullCalendar('destroy');
        $('#calender').fullCalendar({
            //contentHeight: auto,
            defaultDate: new Date(),
            monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
            monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
            dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sabado'],
            dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
            timeFormat: 'h(:mm)a',
            buttonText: {
                today: 'hoje',
                month: 'mês',
                week: 'semana',
                day: 'dia'
            },
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,basicWeek,basicDay,agenda'
            },
            eventLimit: true,
            eventColor: '#eb9195',
            events: events,
            eventClick: function (calEvent, jsEvent, view) {
                selectedEvent = calEvent;
                $('#myModal #eventTitle').text(calEvent.title);
                var $description = $('<div />');
                $description.append($('<p />').html('<br /><br /><b>    Data: </b>' + calEvent.start.format("DD-MMM-YYYY HH:mm a")));
                if (calEvent.end != null) {
                    $description.append($('<p/>').html('<b>    Duração: </b>' + calEvent.end.format("DD-MMM-YYYY HH:mm a")));
                }
                $description.append($('<p />').html('<b>    Descrição: </b>' + calEvent.description));
                $('#myModal #pDetails').empty().html($description);

                $('#myModal').modal();
            },
            selectable: true,
            select: function (start, end) {
                selectedEvent = {
                    id: 0,
                    title: '',
                    description: '',
                    start: start,
                    end: end,
                    allDay: false,
                    color: ''
                };
                openAddEditForm();
                $('#calendar').fullCalendar('unselect');
            },
            editable: true,
            eventDrop: function (event) {
                var data = {
                    Id: event.id,
                    Subject: event.title,
                    Start: event.start.format('DD/MM/YYYY HH:mm A'),
                    End: event.end != null ? event.end.format('DD/MM/YYYY HH:mm A') : null,
                    Description: event.description,
                    ThemeColor: event.color,
                    IsFullDay: event.allDay
                };
                SaveEvent(data);
            }
        })
    }

    $('#btnEdit').click(function () {
        //Open modal dialog for edit event
        openAddEditForm();
        $('#calendar').unselect()
    })
    $('#btnDelete').click(function () {
        var data = { 'id': selectedEvent.Id };
        data.__RequestVerificationToken = $('input[name="__RequestVerificationToken"]').val();
        if (selectedEvent != null) {
            $.ajax({
                type: "POST",
                url: '/Funcionario/Agenda/Excluir',
                data: data,
                success: function (status) {
                    if (status) {
                        //Refresh the calender
                        FetchEventAndRenderCalendar();
                        $('#myModal').modal('hide');
                    }
                },
                error: function () {
                    alert('Failed');
                }
            })
        }
    })

    //$('#dtp1,#dtp2').datetimepicker({
    //	format: 'DD/MM/YYYY HH:mm A'
    //});

    //$('#chkIsFullDay').change(function () {
    //	if ($(this).is(':checked')) {
    //		$('#divDuration').hide();
    //	}
    //	else {
    //		$('#divDuration').show();

    //	}
    //});

    function openAddEditForm() {
        if (selectedEvent != null) {
            $('#hdEventID').val(selectedEvent.Id);
            $('#txtStart').val(selectedEvent.start.format('DD/MM/YYYY'));
            $('#chkIsFullDay').prop("checked", selectedEvent.allDay || false);
            $('#chkIsFullDay').change();
            $('#dtDuracao').val(selectedEvent.end != null ? selectedEvent.end.format('DD/MM/YYYY HH:mm A') : '');
            $('#txtDescription').val(selectedEvent.description);
            $('#ddThemeColor').val(selectedEvent.color);
        }
        $('#myModal').modal('hide');
        $('#myModalSave').modal();
    }

    $('#btnSave').click(function () {
        //Validation/
        if ($('#txtStart').val().trim() == "") {
            alert('Start date required');
            return;
        }
        //if ($('#chkIsFullDay').is(':checked') == false && $('#dtDuracao').val().trim() == "") {
        //	alert('End date required');
        //	return;
        //}
        //else {
        //	var startDate = moment($('#txtStart').val(), "DD/MM/YYYY HH:mm A").toDate();
        //	var endDate = moment($('#dtDuracao').val(), "DD/MM/YYYY HH:mm A").toDate();
        //	if (startDate > endDate) {
        //		alert('Invalid end date');
        //		return;
        //	}
        //}

        /*var formulario = new FormData();

        var agendamentos = {
            "agenda": {
                "Id": $('#hdEventID').val(),
                "ClienteId": $('#clienteSelected').val(),
                "FuncionarioId": $('#funcionarioSelected').val(),
                "ProcedimentoId": $('#procedimentoSelected').val(),
                "Description": $('#txtDescription').val(),
                "Start": $('#txtStart').val().trim(),
                //Duracao: $('#chkIsFullDay').is(':checked') ? null : $('#dtDuracao').val().trim(),
                "ThemeColor": $('#ddThemeColor').val(),
                "IsFullDay": $('#chkIsFullDay').is(':checked')
            }
        };*/

        /*var agendamentos = {
            Id: $('#hdEventID').val(),
            ClienteId: $('#clienteSelected').val(),
            FuncionarioId: $('#funcionarioSelected').val(),
            ProcedimentoId: $('#procedimentoSelected').val(),
            Description: $('#txtDescription').val(),
            Start: $('#txtStart').val().trim(),
            //Duracao: $('#chkIsFullDay').is(':checked') ? null : $('#dtDuracao').val().trim(),
            ThemeColor: $('#ddThemeColor').val(),
            IsFullDay: $('#chkIsFullDay').is(':checked')
        };*/

        var urlencoded = new URLSearchParams();
        var token = $('input[name="__RequestVerificationToken"]').val();
        urlencoded.append("__RequestVerificationToken", token);

        /*var agendamentos = {
            ClienteId: $('#clienteSelected').val(),
            FuncionarioId: $('#funcionarioSelected').val()
        };
        urlencoded.append("agendamentos", agendamentos);*/

        var id = $('#hdEventID').val();
        urlencoded.append("id", id);

        var clienteId = $('#clienteSelected').val();
        urlencoded.append("clienteId", clienteId);

        var funcionarioId = $('#funcionarioSelected').val();
        urlencoded.append("funcionarioId", funcionarioId);

        var procedimentoId = $('#procedimentoSelected').val();
        urlencoded.append("procedimentoId", procedimentoId);

        var description = $('#txtDescription').val();
        urlencoded.append("description", description);

        var start = $('#txtStart').val().trim();
        urlencoded.append("start", start);

        var themeColor = $('#ddThemeColor').val();
        urlencoded.append("themeColor", themeColor);

        var isFullDay = $('#chkIsFullDay').is(':checked');
        urlencoded.append("isFullDay", isFullDay);

        var horasAgendamento = $('#Dthoras').val();
        urlencoded.append("horasAgendamento", horasAgendamento);

        var horasAgendamento = $('#Dthoras').val();
        urlencoded.append("horasAgendamento", horasAgendamento);

        var minutosAgendamento = $('#Dtminutos').val();
        urlencoded.append("minutosAgendamento", minutosAgendamento);

        var horasDuracao = $('#horasDuracao').val();
        urlencoded.append("horasDuracao", horasDuracao);

        var minutosDuracao = $('#minutosDuracao').val();
        urlencoded.append("minutosDuracao", minutosDuracao);

        SaveEvent(urlencoded);
        // call function for submit data to the server

        /*var data = {
            Id: $('#hdEventID').val(),
            FuncionarioId: $('#funcionarioSelected').val(),
            ClienteId: $('#clienteSelected').val(),
            ProcedimentoId: $('#procedimentoSelected').val(),
            Start: $('#txtStart').val().trim(),
            //Duracao: $('#chkIsFullDay').is(':checked') ? null : $('#dtDuracao').val().trim(),
            Description: $('#txtDescription').val(),
            ThemeColor: $('#ddThemeColor').val(),
            IsFullDay: $('#chkIsFullDay').is(':checked'),
            Horas: $('#Dthoras').val(),
            Minutos: $('#Dtminutos').val(),
            HorasDuracao: $('#horasDuracao').val(),
            MinutosDuracao: $('#minutosDuracao').val()
        }
        data.__RequestVerificationToken = $('input[name="__RequestVerificationToken"]').val();
        SaveEvent(data);*/
    })

    /*function SaveEvent(data) {
        $.ajax({
            type: "POST",
            url: '/Funcionario/Agenda/Cadastrar',
            data: data,
            success: function (status) {
                if (status) {
                    //Refresh the calender
                    FetchEventAndRenderCalendar();
                    $('#myModalSave').modal('hide');
                }
            },
            error: function () {
                alert('Failed');
            }
        })
    }*/

    function SaveEvent(urlencoded) {
        fetch('/Funcionario/Agenda/Cadastrar', {
            method: "POST",
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            body: urlencoded
        })
            .then((response) => {
                return response.json();
            })
            .then((status) => {
                if (status) {
                    //Refresh the calender
                    FetchEventAndRenderCalendar();
                    $('#myModalSave').modal('hide');
                }
            })
            .catch(() => {
                alert('Failed');
            });
    }

    duracao();
    datas();
})

function datas() {
    $("#txtStart").datepicker({
        dateFormat: 'dd/mm/yy',
        minDate: 0,
        maxDate: "+1m",
        setDate: "+0d"
    });
}

function duracao() {
    var formulario = new FormData();

    var procedimentoId = document.getElementById("procedimentoSelected").value;
    formulario.append("procedimentoId", procedimentoId);

    var token = $('input[name="__RequestVerificationToken"]').val();
    formulario.append("__RequestVerificationToken", token);

    fetch('/Funcionario/Agenda/Duracao', { method: "POST", body: formulario })
        .then((response) => {
            return response.json();
        })
        .then((duration) => {
            let horas = document.getElementById("horasDuracao");
            horas.value = duration.at(0);
            let minutos = document.getElementById("minutosDuracao");
            minutos.value = duration.at(1);
        })
        .catch((error) => {
            alert(error);
        });
}