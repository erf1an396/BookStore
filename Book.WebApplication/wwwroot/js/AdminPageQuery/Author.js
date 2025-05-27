$(document).ready(function () {
    const urlParams = new URLSearchParams(window.location.search);
    const bookId = urlParams.get('authorId');

    

    debugger

    if (bookId) {
        $.ajax({
            url: `/admin/author/GetById?id=${bookId}`,
            type: 'GET',

            success: function (authors) {
                const author = authors.Value;

                $('#authorId').val(author.Id);
                $('#authorName').val(author.Name);
                $('#authorBorn').val(author.Born_Year);
                $('#authorBook').val(author.Book_Count);
                $('#authorPrize').val(author.Prize_Count);
                $('#bookLanguage').val(author.Language);
                CKEDITOR.instances['authorDescription'].setData(author.Description);
                $('#cancelEditBtn').show();
                $('#formTitle').text("✏️ ویرایش کتاب");
            },

            error: function () {
                alert("خطا در دریافت اطلاعات نویسنده ")
            }

        });
    }

    $('#authorForm').on('submit', function (e) {
        e.preventDefault();

        debugger
        const authorId = $('#authorId').val();
        const author =
        {
            Id: $('#authorId').val(),
            Name: $('#authorName').val(),
            Born_Year: parseInt($('#authorBorn').val()),
            Book_Count: parseInt($('#authorBook').val()),
            Prize_Count: parseInt($('#authorPrize').val()),
            Language: parseInt($('#bookLanguage').val()),
            Description: CKEDITOR.instances['authorDescription'].getData(),

        }

        const url = bookId ? '/admin/author/Update' : '/admin/author/Create';

        $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(author),

            success: function () {
                resetForm();
            },
            error: function () {
                alert('خطا در ذخیره نویسنده')
            }

        });

    });

    $('#cancelEditBtn').on('click', function () {
        resetForm();
    })


    function resetForm() {
        $('#authorForm')[0].reset();
        $('#authorId').val('');
        $('#cancelEditBtn').hide();
    }


})

