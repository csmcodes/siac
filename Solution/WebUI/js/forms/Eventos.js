$(document).ready(function () {
    $('body').css('background', 'transparent');
    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();

    var calendar = $('#calendar').fullCalendar({
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        buttonText: {
            prev: '&laquo;',
            next: '&raquo;',
            prevYear: '&nbsp;&lt;&lt;&nbsp;',
            nextYear: '&nbsp;&gt;&gt;&nbsp;',
            today: 'hoy',
            month: 'mes',
            week: 'semana',
            day: 'día'
        },
        selectable: true,
        selectHelper: true,
        select: function (start, end, allDay) {
            var title = prompt('Nombre Evento:');
            if (title) {
                calendar.fullCalendar('renderEvent',
						{
						    title: title,
						    start: start,
						    end: end,
						    allDay: allDay
						},
						true // make the event "stick"
					);
            }
            calendar.fullCalendar('unselect');
        },
        editable: true,
        events: [
				{
				    title: 'Visita del Señor Ministro',
				    start: new Date(y, m, 1)
				},
                {
                    title: 'Gabinete',
                    start: new Date(y, m, 2)
                },
                {
                    title: 'Almuerzo directores sectoriales',
                    start: new Date(y, m, 4)
                },
                {
                    title: 'Entrega informes',
                    start: new Date(y, m, 6)
                },
				{
				    title: 'Reunion directores',
				    start: new Date(y, m, d, 10, 30),
				    allDay: false
				},
				{
				    title: 'Bienvenida Señor Presidente',
				    start: new Date(y, m, d, 12, 0),
				    end: new Date(y, m, d, 14, 0),
				    allDay: false
				},
				{
				    title: 'Cumpleaños Director Regional',
				    start: new Date(y, m, d + 1, 19, 0),
				    end: new Date(y, m, d + 1, 22, 30),
				    allDay: false
				},
				{
				    title: 'Buscar información en GOOGLE',
				    start: new Date(y, m, 28),
				    end: new Date(y, m, 29),
				    url: 'http://google.com/'
				}
			]
    });

});