$(document).ready(function () {

    let currentPhotoId = null;
    const addPhotoModal = new bootstrap.Modal(document.getElementById('addPhotoModal'));
    const deletePhotoModal = new bootstrap.Modal(document.getElementById('deletePhotoModal'));

    loadPhotos();

    //OPEN modallllllllllllllllll --------------------------------------------------------------------------------------
    $('#btnOpenAddPhotoModal').on('click', function () {
        addPhotoModal.show();
    });


    // ADD modalllllllllllllllllllllllllllllllllllllll-------------------------------------------------------------------------
    $('#addPhotoBtn').on('click', function () {
        const file = $('#photoFile')[0].files[0];
        const entityName = $('#entityName').val();
        const authorId = $('#hd_authorId').val();
        debugger

        if (!file) {
            alert("لطفاً یک فایل انتخاب کنید");
            return;
        }

        const formData = new FormData();
        formData.append('File', file);
        formData.append('AuthorId', authorId);
        formData.append('Name', entityName);

        debugger
        $.ajax({
            url: '/admin/AuthorPhoto/Create',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (res) {
                if (res.isSuccess !== false) {
                    addPhotoModal.hide();
                    $('#photoFile').val('');
                    $('#entityName').val('');
                    loadPhotos();
                }
            },
            error: function () {
                alert("خطا در آپلود عکس");
            }
        });
    });

    //DELETE BTN -------------------------------------------------------------------------------------------------------
    $('#deletePhotoBtn').on('click', function () {
        $.ajax({
            url: '/admin/AuthorPhoto/Delete/' + currentPhotoId,
            type: 'POST',
            success: function (res) {
                if (res.isSuccess !== false) {
                    deletePhotoModal.hide();
                    loadPhotos();
                }
            }
        });
    });

    // LOAD ------------------------------------------------------------------------------------------------------------------------------
    function loadPhotos() {
        const authorId = $('#hd_authorId').val();
        debugger
        $.get('/admin/AuthorPhoto/GetAll?authorId=' + authorId, function (response) {
            const list = response.Value;
            let html = '';
            debugger

            console.log(response);
            list.forEach(p => {
                html += `
                    <li class="list-group-item d-flex justify-content-between align-items-center">
                        <span>${p.Name}.${p.Extenstion}</span>
                        <button class="btn btn-sm btn-danger delete-btn" data-id="${p.Id}"> حذف</button>
                    </li>
                `;
            });

            $('#photoList').html(html);

            //DELETE CONNECTED BTN -----------------------------------------------------------------------------------------
            $('.delete-btn').on('click', function () {
                currentPhotoId = $(this).data('id');
                deletePhotoModal.show();
            });
        });
    }

});
