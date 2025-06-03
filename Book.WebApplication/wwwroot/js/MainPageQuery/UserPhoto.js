

$(document).ready(function () {

    $('#profile-picture-file').on('change', function () {
        let fileInput = this;

        if (fileInput.files.length === 0) return;


        let formData = new FormData();

        formData.append('File', fileInput.files[0]);

        console.log(formData);

        debugger
        $.ajax({
            url: '/UserPhoto/Create',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {

                
                debugger
                $('#profilePreview').attr('src', `/img/UserPhoto/${response.Value}.webp`);
                
                location.reload();
            },
            error: function () {
                alert('آپلود عکس با خطا مواجه شد . ')
            }
        });

    });
})