///////
//upload.js
//Permite subir archivos al servidor
//Desarrollado por: Cristhian Sanmartin 
//Fecha: 15 Marzo 2017
//////


//////////////////IMAGES UPLOAD////////////////////////////

function ImgFileUpload(inputfile, obj) {

    var files = $("#" + inputfile).get(0).files;
    var data = new FormData();
    for (i = 0; i < files.length; i++) {
        data.append("file" + i, files[i]);
    }
    data.append("obj", JsonObjString(obj));

    $.ajax({
        type: "POST",
        url: '/handlers/ImgFileUploader.ashx',
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            if (result) {
                try {
                    ImgUploadResult(result);
                }
                catch (err) {
                }
             
            }
        }
    });

}




function XmlFileUpload(inputfile, obj) {

    var files = $("#" + inputfile).get(0).files;
    var data = new FormData();
    for (i = 0; i < files.length; i++) {
        data.append("file" + i, files[i]);
    }
    data.append("obj", JsonObjString(obj));

    $.ajax({
        type: "POST",
        url: '/handlers/XmlFileUploader.ashx',
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            if (result) {
                try {
                    XmlUploadResult(result);
                }
                catch (err) {
                }


            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var errorData = $.parseJSON(XMLHttpRequest.responseText);
            BootBoxAlert(errorData.Message);
        }
    });

}

function XmlFileImport(inputfile, obj) {

    var files = $("#" + inputfile).get(0).files;
    var data = new FormData();
    for (i = 0; i < files.length; i++) {
        data.append("file" + i, files[i]);
    }
    data.append("obj", JsonObjString(obj));

    $.ajax({
        type: "POST",
        url: '/handlers/XmlFileUploader.ashx?import=1',
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            if (result) {
                try {
                    XmlUploadResult(result);
                }
                catch (err) {
                }


            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var errorData = $.parseJSON(XMLHttpRequest.responseText);
            BootBoxAlert(errorData.Message);
        }
    });

}