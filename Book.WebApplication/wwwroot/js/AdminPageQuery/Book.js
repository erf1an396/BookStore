$(document).ready(function () {

    updateCharCount();

    const urlParams = new URLSearchParams(window.location.search);
    const bookId = urlParams.get('bookId');

    if (bookId) {

        debugger

        $.ajax({
            url: `/admin/book/GetById?id=${bookId}`,
            type: 'GET',

            
            success: function (books) {
                console.log(books)
                const book = books.Value;


                $('#bookId').val(book.Id);
                $('#bookTitle').val(book.Title);
                $('#bookAuthor').val(book.AuthorId);
                $('#bookPublisher').val(book.Publisher);
                $('#bookYear').val(book.Publication_Year);
                $('#bookIsbn').val(book.Isbn);
                $('#bookPages').val(book.Pages);
                CKEDITOR.instances['bookDescription'].setData(book.Description);
                CKEDITOR.instances['bookReview'].setData(book.Review);
                $('#bookLanguage').val(book.Language);
                $('#bookCategoryId').val(book.CategoryId);
                $('#bookPrice').val(book.Price);
                $('#cancelEditBtn').show();
                $('#formTitle').text("✏️ ویرایش کتاب");


            },
            error: function () {

                //console.error("Status:", xhr.status);
                //console.error("Response:", xhr.responseText);
                //alert("خطا: " + xhr.responseText);
                alert("خطا در دریافت اطلاعات کتاب");
            }
        });
    }


     CKEDITOR.instances['bookDescription'].on('change', function () {
        updateCharCount();
    }); 


    $('#bookForm').on('submit', function (e) {
        e.preventDefault();

       
        var content = CKEDITOR.instances['bookDescription'].getData();
        var length = getPlainTextLength(content);

        if (length > maxChars) {
            $('#editor-error').text('حداکثر 150 کاراکتر').show();
            return;
        }

        $('#editor-error').hide();

        

        debugger
        const bookId = $('#bookId').val();
        const book = {
            Id: $('#bookId').val(),
            Title: $('#bookTitle').val(),
            AuthorId: parseInt($('#bookAuthor').val()),
            Publisher: $('#bookPublisher').val(),
            Publication_Year: parseInt($('#bookYear').val()),
            Isbn: $('#bookIsbn').val(),
            Pages: parseInt($('#bookPages').val()),
            Description: CKEDITOR.instances['bookDescription'].getData(),
            Language: parseInt($('#bookLanguage').val()),
            CategoryId: parseInt($('#bookCategoryId').val()),
            Price: parseInt($('#bookPrice').val()),
            Review: CKEDITOR.instances['bookReview'].getData()


        };

        debugger

        const url = bookId ? '/admin/book/Update' : '/admin/book/Create';

        $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(book),

            success: function () {

                debugger
                resetForm();


            },
            error: function () {
                alert("خطا در ذخیره کتاب")
            }
        });
    });

    $('#cancelEditBtn').on('click', function () {
        resetForm();
    })



    function resetForm() {
        $('#bookForm')[0].reset();
        $('#bookId').val('');
        $('#cancelEditBtn').hide();
    }

   



})





////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// FOR FORM CUSTOMIZE //


var maxChars = 150;

function getPlainTextLength(html) {
    var div = document.createElement("div");
    div.innerHTML = html ;
    return div.textContent.trim().length;
}

function updateCharCount() {
    var content = CKEDITOR.instances['bookDescription'].getData();
    var length = getPlainTextLength(content);
    var $counter = $('#char-count');

    $counter.text(length + ' / ' + maxChars);

    if (length > maxChars) {
        $counter.css('color', 'red');
        $('#editor-error').text('حداکثر تعداد مجاز کاراکتر 150 تاست').show();
    } else {
        $counter.css('color', 'black');
        $('#editor-error').hide();
    }
}