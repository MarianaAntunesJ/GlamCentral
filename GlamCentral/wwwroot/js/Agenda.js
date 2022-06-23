/*btnSave.click(function () {
    let campoHidden = $(this).parent().find("input[name=imagem]");
    let imagem = $(this).parent().find(".img-upload");
    let btnExcluir = $(this).parent().find(".btn-imagem-excluir");
    let inputFile = $(this).parent().find(".input-file");

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
    let events = [];
    let selectedEvent = null;

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
                let $description = $('<div />');
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
                let data = {
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
        let data = { 'id': selectedEvent.Id };
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
        //	let startDate = moment($('#txtStart').val(), "DD/MM/YYYY HH:mm A").toDate();
        //	let endDate = moment($('#dtDuracao').val(), "DD/MM/YYYY HH:mm A").toDate();
        //	if (startDate > endDate) {
        //		alert('Invalid end date');
        //		return;
        //	}
        //}

        /*let formulario = new FormData();

        let agendamentos = {
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

        /*let agendamentos = {
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

        let urlencoded = new URLSearchParams();
        let token = $('input[name="__RequestVerificationToken"]').val();
        urlencoded.append("__RequestVerificationToken", token);

        /*let agendamentos = {
            ClienteId: $('#clienteSelected').val(),
            FuncionarioId: $('#funcionarioSelected').val()
        };
        urlencoded.append("agendamentos", agendamentos);*/

        let id = $('#hdEventID').val();
        urlencoded.append("id", id);

        let clienteId = $('#clienteSelected').val();
        urlencoded.append("clienteId", clienteId);

        let funcionarioId = $('#funcionarioSelected').val();
        urlencoded.append("funcionarioId", funcionarioId);

        let procedimentoId = $('#procedimentoSelected').val();
        urlencoded.append("procedimentoId", procedimentoId);

        let description = $('#txtDescription').val();
        urlencoded.append("description", description);

        let start = $('#txtStart').val().trim();
        urlencoded.append("start", start);

        let themeColor = $('#ddThemeColor').val();
        urlencoded.append("themeColor", themeColor);

        let isFullDay = $('#chkIsFullDay').is(':checked');
        urlencoded.append("isFullDay", isFullDay);

        let horasAgendamento = $('#Dthoras').val();
        urlencoded.append("horasAgendamento", horasAgendamento);

        let minutosAgendamento = $('#Dtminutos').val();
        urlencoded.append("minutosAgendamento", minutosAgendamento);

        let horasDuracao = $('#horasDuracao').val();
        urlencoded.append("horasDuracao", horasDuracao);

        let minutosDuracao = $('#minutosDuracao').val();
        urlencoded.append("minutosDuracao", minutosDuracao);

        SaveEvent(urlencoded);
        // call function for submit data to the server

        /*let data = {
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
    horaAtual();
})

function datas() {
    $("#txtStart").datepicker({
        dateFormat: 'dd/mm/yy',
        minDate: 0,
        maxDate: "+1m",
        setDate: "+0d"
    });
}


function horaAtual() {
    const horario = formataHora();

    document.querySelector('#Dthoras').value = horario.hora;
    document.querySelector('#Dtminutos').value = horario.minuto;
}

function formatSaidaHoraMinuto(horas, minutos) {
    return {
        hora: horas,
        minuto: minutos
    };
}

function formataHora() {
    let horaAtual = new Date();
    let horas = ('0' + horaAtual.getHours()).slice(-2);
    let minutos = ('0' + horaAtual.getMinutes()).slice(-2);

    let penultimoNumeroMinuto = minutos.slice(0, -1);

    let horario = horas + ':' + minutos;

    const naoTemQueAlterar = (/:[0-5](5|0)/g);
    if (naoTemQueAlterar.test(horario)) {
        return formatSaidaHoraMinuto(horas, minutos);
    }

    const alterarUltimoNumero = (/:[0-5][1-4]/g);
    const alterarMinutos = (/:[^5][6-9]/g);
    const alterarHoras = (/[0-2][0-3]:/g);
    const zerarHoras = (/23:5[6-9]/g);
    const zerarMinutos = (/:5[6-9]/g);

    horario = horario.replace(alterarUltimoNumero, ':' + penultimoNumeroMinuto + '5');
    horario = horario.replace(alterarMinutos, ':' + (++penultimoNumeroMinuto) + '0');
    horario = horario.replace(zerarHoras, "00:00");
    horario = zerarMinutos.test(horario)
        ? horario.replace(alterarHoras, (++horas) + ':').replace(zerarMinutos, ':00')
        : horario;
    
    return formatSaidaHoraMinuto(horario.slice(0, 2), horario.slice(3, 5))
}

function duracao() {
    let formulario = new FormData();

    let procedimentoId = document.getElementById("procedimentoSelected").value;
    formulario.append("procedimentoId", procedimentoId);

    let token = $('input[name="__RequestVerificationToken"]').val();
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