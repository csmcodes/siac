$(document).ready(function () {

    // simple chart
    var flash = [[0, 11], [1, 9], [2, 12], [3, 8], [4, 7], [5, 3], [6, 1]];
    var html5 = [[0, 5], [1, 4], [2, 4], [3, 1], [4, 9], [5, 10], [6, 13]];
    var css3 = [[0, 6], [1, 1], [2, 9], [3, 12], [4, 10], [5, 12], [6, 11]];

    function showTooltip(x, y, contents) {
        $('<div id="tooltip" class="tooltipflot">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 5
        }).appendTo("body").fadeIn(200);
    }


    /*var plot = $.plot($("#chartplace"),
			   [{ data: flash, label: "Flash(x)", color: "#6fad04" },
              { data: html5, label: "HTML5(x)", color: "#06c" },
              { data: css3, label: "CSS3", color: "#666"}], {
                  series: {
                      lines: { show: true, fill: true, fillColor: { colors: [{ opacity: 0.05 }, { opacity: 0.15}]} },
                      points: { show: true }
                  },
                  legend: { position: 'nw' },
                  grid: { hoverable: true, clickable: true, borderColor: '#666', borderWidth: 2, labelMargin: 10 },
                  yaxis: { min: 0, max: 15 }
              });

    var previousPoint = null;
    $("#chartplace").bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0].toFixed(2),
					y = item.datapoint[1].toFixed(2);

                showTooltip(item.pageX, item.pageY,
									item.series.label + " of " + x + " = " + y);
            }

        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }

    });

    $("#chartplace").bind("plotclick", function (event, pos, item) {
        if (item) {
            $("#clickdata").text("You clicked point " + item.dataIndex + " in " + item.series.label + ".");
            plot.highlight(item.series, item.datapoint);
        }
    });
    */

    //datepicker
    $('#datepicker').datepicker();

    // tabbed widget
    $('.tabbedwidget').tabs();

    //SetMenuOption();
    $('body').css('background', 'transparent');

    $(".newcomment").on("keydown", Comments);  //

});

function currentTime() {
    var now = new Date();
    now = now.getHours() + ':' + now.getMinutes() + ':' + now.getSeconds();
    return now;
}

function Comments(event) {
    var id = document.activeElement.id;
    var code = event.keyCode;
    var char = event.char;
    if (code == 13) //ENTER
    {
        var lista = $(this)[0].parentNode.parentNode.parentNode.parentNode.parentNode;
        var op = $(this)[0].parentNode.parentNode.parentNode.parentNode;

        var li = $("<li></li>");
        var html = "<div><img src=\"images/photos/thumb1.png\" alt=\"\" class=\"pull-left\" /><div class=\"uinfo\"><h5>Administrador</h5><span class=\"pos\">" + $(this).val() + "</span> <span>Hoy a las "+currentTime()+"</span></div></div>";
        $(li).html(html);

        $(li).insertBefore($(op));
        $(this).val("");
        //$(lista).prepend($(li))        
        event.preventDefault();
    }
}

