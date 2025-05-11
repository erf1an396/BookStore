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
            CategoryId: parseInt($('#bookCategoryId').val())


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

    //function loadBook() {
    //    $.ajax({
    //        url: '/admin/book/GetAll',
    //        type: 'GET',
    //        success: function (books) {



    //            const $list = $('#bookList');
    //            $list.empty();
    //            const value = books.Value;


    //            for (let book of books.Value) {
    //                $list.append(
    //                    `
    //                      <li class="list-group-item d-flex justify-content-between align-items-center">
    //                        <div><strong>${book.Title}</strong> - ${book.Author}</div>
    //                        <div>
    //                            <button class="btn btn-sm btn-primary me-2" onclick='editBook(${JSON.stringify(book)})'>ویرایش</button>
    //                            <button class="btn btn-sm btn-danger" onclick='deleteBook(${book.Id})'>حذف</button>
    //                        </div>
    //                      </li>
                        
    //                    `
    //                );
    //            }

    //        },
    //        error: function () {
    //            alert('خط در برگزاری لیست کتاب ها ')
    //        }
    //    });
    //}

    

    //window.editBook = function (book) {

    //    $('#bookId').val(book.Id);
    //    $('#bookTitle').val(book.Title);
    //    $('#bookAuthor').val(book.Author);
    //    $('#bookPublisher').val(book.Publisher);
    //    $('#bookYear').val(book.Publication_Year);
    //    $('#bookIsbn').val(book.Isbn);
    //    $('#bookPages').val(book.Pages);
    //    /*$('#bookDescription').val(book.Description);*/
    //    CKEDITOR.instances['bookDescription'].setData(book.Description);
    //    $('#bookLanguage').val(book.Language);
    //    $('#bookCategoryId').val(book.CategoryId);
    //    $('#cancelEditBtn').show();

    //}

    //window.deleteBook = function (bookId) {
    //    if (!confirm('آیا مطمئن هستید')) return;

    //    debugger
    //    $.ajax({
    //        url: '/admin/book/Delete/' + bookId,
    //        type: 'POST',


    //        success: function () {
    //            loadBook();
                

    //        },
    //        error: function () {
    //            alert('خطا در حذف کتاب')
    //        }

    //    });


    //};

    function resetForm() {
        $('#bookForm')[0].reset();
        $('#bookId').val('');
        $('#cancelEditBtn').hide();
    }
})