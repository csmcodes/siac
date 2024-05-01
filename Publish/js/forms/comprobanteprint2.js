$(document).ready(function () {


    $('#txtdiv').load('../../print/fac.txt', function () {
        window.print(); //prints when text is loaded
    });

})