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
        const bookId = $('#hd_bookId').val();
        debugger

        if (!file) {
            alert("لطفاً یک فایل انتخاب کنید");
            return;
        }

        const formData = new FormData();
        formData.append('File', file);
        formData.append('BookId', bookId);
        formData.append('Name', entityName);

        debugger
        $.ajax({
            url: '/admin/BookPhoto/Create',
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
            url: '/admin/bookphoto/Delete/' + currentPhotoId,
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
        const bookId = $('#hd_bookId').val();
        debugger
        $.get('/admin/bookphoto/GetAll?bookId=' + bookId, function (response) {
            const list = response.Value;
            let html = '';
            debugger
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
