$(document).ready(function () {

    //Replaces data-rel attribute to rel.
    //We use data-rel because of w3c validation issue
    $('a[data-rel]').each(function () {
        $(this).attr('rel', $(this).data('rel'));
    });

    $("#medialist a").colorbox();
    $('body').css('background', 'transparent');

});
$(window).load(function () {
    $('#medialist').isotope({
        itemSelector: 'li',
        layoutMode: 'fitRows'
    });

    // Media Filter
    $('#mediafilter a').click(function () {

        var filter = ($(this).attr('href') != 'all') ? '.' + $(this).attr('href') : '*';
        $('#medialist').isotope({ filter: filter });

        $('#mediafilter li').removeClass('current');
        $(this).parent().addClass('current');

        return false;
    });
});