
function SetDualSelect(id) {

    var db = $('#'+id).find('.ds_arrow button'); //get arrows of dual select
    var sel1 = $('#'+id+' select:first-child'); 	//get first select element
    var sel2 = jQuery('#'+id+' select:last-child'); 		//get second select element

    db.click(function () {
        var t = ($(this).hasClass('ds_prev')) ? 0 : 1; // 0 if arrow prev otherwise arrow next
        if (t) {
            sel1.find('option').each(function () {
                if ($(this).is(':selected')) {
                    $(this).attr('selected', false);
                    var op = sel2.find('option:first-child');
                    sel2.append($(this));
                }
            });
        } else {
            sel2.find('option').each(function () {
                if ($(this).is(':selected')) {
                    $(this).attr('selected', false);
                    sel1.append($(this));
                }
            });
        }
        return false;
    });	
}