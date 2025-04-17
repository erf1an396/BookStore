$(document).ready(function () {

   
    CKEDITOR.replace('ckEditor4');
    LoadCkEditor4();

});


function LoadCkEditor4() {


    if (!document.getElementById("ckEditor4"))
        return;

    //$("body").append("<script src='/ckeditor4/ckeditor/ckeditor.js'></script>");
    

    CKEDITOR.replace('ckEditor4', {
        customConfig:'/ckeditor4/ckeditor/config.js'
    });
}