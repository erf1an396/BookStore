$(document).ready(function () {

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
                $('#bookAuthor').val(book.Author);
                $('#bookPublisher').val(book.Publisher);
                $('#bookYear').val(book.Publication_Year);
                $('#bookIsbn').val(book.Isbn);
                $('#bookPages').val(book.Pages);
                CKEDITOR.instances['bookDescription'].setData(book.Description);
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




    $('#bookForm').on('submit', function (e) {
        e.preventDefault();

        const bookId = $('#bookId').val();
        const book = {
            Id: $('#bookId').val(),
            Title: $('#bookTitle').val(),
            Author: $('#bookAuthor').val(),
            Publisher: $('#bookPublisher').val(),
            Publication_Year: parseInt($('#bookYear').val()),
            Isbn: $('#bookIsbn').val(),
            Pages: parseInt($('#bookPages').val()),
            Description: CKEDITOR.instances['bookDescription'].getData(),
            Language: parseInt($('#bookLanguage').val()),
            CategoryId: parseInt($('#bookCategoryId').val()),
            Price: parseInt($('#bookPrice').val())


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

const description = document.getElementById("bookPrice");
description.addEventListener("keypress", function (e) {
    if (!/[0-9]/.test(e.key)) {
        e.preventDefault();
    }
});