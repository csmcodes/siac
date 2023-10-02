$(document).ready(function () {
    $('body').css('background', 'transparent');
    $("#nuevomsg").on("click", New); 
    $('.msglist li').click(function () {
        $('.msglist li').each(function () { $(this).removeClass('selected') });
        $(this).addClass('selected');

        // for mobile
        $('.msglist').click(function () {
            if ($(window).width() < 480) {
                $('.messageright, .messagemenu .back').show();
                $('.messageleft').hide();
            }
        });

        $('.messagemenu .back').click(function () {
            if ($(window).width() < 480) {
                $('.messageright, .messagemenu .back').hide();
                $('.messageleft').show();
            }
        });
    });
});

function New() {
    window.location = "wfMensaje.aspx";
}

function ShowMessage(numero) {
    var html = "";
    if (numero == 1) {

        html = '<h1 class="subject">Comunicado Ministerio Coordinador de Sectores Estratégicos</h1>' +
               '<div class="msgauthor"> '+
               '<div class="thumb"><img src="images/photos/thumb1.png" alt="" /></div>' +
                '<div class="authorinfo">' +
                 '<span class="date pull-right">April 03, 2012</span>' +
                  '<h5><strong>Director Regional</strong> <span>directorreg@sectoresestrategicos.gob.ec</span></h5>' +
                   '<span class="to">to admin@sectoresestrategicos.gob.ec</span>' +
                    '</div><!--authorinfo-->' +
                     '</div><!--msgauthor-->' +
                      '<div class="msgbody">' +                       
                       '<p>A todos los funcionarios publicos, se les comunica que</p>' +
                        '<p>El día de hoy miércoles 14 de noviembre de 2012, a las 11h00 (hora de Río de Janeiro), con la' +
                        'presencia del Ministro de Coordinación de los Sectores Estratégicos, Dr. Rafael Poveda Bonilla; y, ' +
                    'del Embajador de la República del Ecuador en la República Federativa de Brasil, Horacio Sevilla, ' +
                    'se suscribió el contrato para el financiamiento del Proyecto Hidroeléctrico Manduriacu entre la ' +
                    'República del Ecuador y el Banco Nacional de Desarrollo de Brasil –BNDES-, por un valor de 90,2 ' +
                    'millones de dólares equivalentes al 72% del valor total del proyecto que asciende a 124,8 millones ' +
                    'de dólares. ' +
                    'El proyecto Hidroeléctrico Manduriacu está situado entre las provincias de Imbabura y Pichincha, ' +
                    'es desarrollado por la empresa pública CELEC EP y es ejecutado por la constructora Odebrecht; ' +
                    'tendrá una capacidad instalada de 60 MW, registra en la actualidad un 12,2% de avance en la ' +
                    'ejecución de la obra y tiene su arranque de operaciones programado para el último trimestre de ' +
                    '2014. ' +
                    'En el año 2015, el Proyecto Hidroeléctrico Manduriacu evitará la emisión de 1,88 millones de ' +
                    'toneladas de CO2 y la importación de combustibles para generación eléctrica por 24 millones de ' +
                    'dólares. ' +
                    'El Ministro Rafael Poveda expresó que “La suscripción de este instrumento ocurre dentro del ' +
                    'marco de fortalecimiento de las relaciones entre Ecuador y Brasil y de profundización de los ' +
                    'marcos de cooperación bilateral, abriendo nuevas oportunidades para ambos países.” </p>' +
                    '<p>' +
                    'DIRECCIÓN DE COMUNICACIÓN SOCIAL ' +
                    'MINISTERIO DE COORDINACIÓN ' +
                    'DE LOS SECTORES ESTRATÉGICOS ' +
                    'Contacto: 02 2260670 ext. 420 - 425' +
                    '</p></div><!--msgbody-->"';
    }
    if (numero == 2) {
        html = '<h1 class="subject">Invitacion almuerzo institucional</h1>' +
               '<div class="msgauthor"> ' +
               '<div class="thumb"><img src="images/photos/thumb1.png" alt="" /></div>' +
                '<div class="authorinfo">' +
                 '<span class="date pull-right">April 03, 2012</span>' +
                  '<h5><strong>Director Regional</strong> <span>directorreg@sectoresestrategicos.gob.ec</span></h5>' +
                   '<span class="to">to admin@sectoresestrategicos.gob.ec</span>' +
                    '</div><!--authorinfo-->' +
                     '</div><!--msgauthor-->' +
                      '<div class="msgbody">' +
                       '<p>El dia 20 del presente se resolvio:</p>' +
                        '<p>Invitar a todos los funcionarios del Ministerio Coordianor de Sectores Estrategicos a un almuerzo de integracion,' +
                        ' a realizarse en las instalaciones del ministerio a las 19:00. ' +                    
                    '<p>' +
                    'DIRECCIÓN DE COMUNICACIÓN SOCIAL ' +
                    'MINISTERIO DE COORDINACIÓN ' +
                    'DE LOS SECTORES ESTRATÉGICOS ' +
                    'Contacto: 02 2260670 ext. 420 - 425' +
                    '</p></div><!--msgbody-->"';
    }
    if (numero == 3) {
        html = '<h1 class="subject">Documentos solicitados</h1>' +
               '<div class="msgauthor"> ' +
               '<div class="thumb"><img src="images/photos/thumb1.png" alt="" /></div>' +
                '<div class="authorinfo">' +
                 '<span class="date pull-right">April 03, 2012</span>' +
                  '<h5><strong>Maria Perez</strong> <span>mperez@sectoresestrategicos.gob.ec</span></h5>' +
                   '<span class="to">to admin@sectoresestrategicos.gob.ec</span>' +
                    '</div><!--authorinfo-->' +
                     '</div><!--msgauthor-->' +
                      '<div class="msgbody">' +
                       '<p>Estimado Juan te adjunto los documentos solicitaados</p>' +
                        '<p>Espero los mismos te sirvan, cualquier cosa porfavor no dudes en comunicarte' +
                        ' Saludos' +
                    '<p>' +
                    'Maria Perez<br>' +
                    'MINISTERIO DE COORDINACIÓN ' +
                    'DE LOS SECTORES ESTRATÉGICOS ' +
                    
                    '</p></div><!--msgbody-->"';
    }
    if (numero == 4) {
        html = '<h1 class="subject">Informacion importante</h1>' +
               '<div class="msgauthor"> ' +
               '<div class="thumb"><img src="images/photos/thumb1.png" alt="" /></div>' +
                '<div class="authorinfo">' +
                 '<span class="date pull-right">April 03, 2012</span>' +
                  '<h5><strong>MICSE</strong> <span>micse@sectoresestrategicos.gob.ec</span></h5>' +
                   '<span class="to">to admin@sectoresestrategicos.gob.ec</span>' +
                    '</div><!--authorinfo-->' +
                     '</div><!--msgauthor-->' +
                      '<div class="msgbody">' +
                       '<p>El Presidente de la República, Rafael Correa Delgado posesionó el ' +
'miércoles 28 de noviembre al doctor Rafael Poveda Bonilla como Ministro ' +
'Coordinador de los Sectores Estratégicos, en reemplazo del ingeniero Jorge ' +
'Glas Espinel quien fue elegido para que lo acompañe como su binomio en ' +
'las próximas elecciones. ' +
'El actual Ministro Coordinador de los Sectores Estratégicos es quiteño de ' +
'39 años, casado con María Gabriela Alarcón, tienen dos hijas Rafaela e ' +
'Isabela de ocho y seis años, respectivamente. ' +
' </p><p>' +
'Abogado y Doctor en Jurisprudencia, por la Pontificia Universidad Católica ' +
'del Ecuador con Experiencia en Derecho Corporativo, Contratación ' +
'Pública, Seguros, Energía y Telecomunicaciones. Es Especialista Superior' + 
'en Modernización del Estado y Contratación Pública y Máster en ' +
'Administración de Empresas MBA, en la Escuela de Negocios IDE. ' +
' </p><p>' +
'</p></div><!--msgbody-->"';
    }
        $("#msgcontent").html(html)
}
