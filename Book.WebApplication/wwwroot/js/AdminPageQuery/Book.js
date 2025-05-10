$(document).ready(function () {

    loadBook();
    loadBookMain();

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


        const url = bookId ? '/admin/book/Update' : '/admin/book/Create';

        $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(book),

            success: function () {

                resetForm();
                loadBook();
                loadBookMain();
            },
            error: function () {
                alert("خطا در ذخیره کتاب")
            }
        });
    });

    $('#cancelEditBtn').on('click', function () {
        resetForm();
    })

    function loadBook() {
        $.ajax({
            url: '/admin/book/GetAll',
            type: 'GET',
            success: function (books) {



                const $list = $('#bookList');
                $list.empty();
                const value = books.Value;


                for (let book of books.Value) {
                    $list.append(
                        `
                          <li class="list-group-item d-flex justify-content-between align-items-center">
                            <div><strong>${book.Title}</strong> - ${book.Author}</div>
                            <div>
                                <button class="btn btn-sm btn-primary me-2" onclick='editBook(${JSON.stringify(book)})'>ویرایش</button>
                                <button class="btn btn-sm btn-danger" onclick='deleteBook(${book.Id})'>حذف</button>
                            </div>
                          </li>
                        
                        `
                    );
                }

            },
            error: function () {
                alert('خط در برگزاری لیست کتاب ها ')
            }
        });
    }

    function loadBookMain() {
        $.ajax({
            url: '/admin/book/GetAll',
            type: 'GET',
            success: function (books) {



                const $list = $('#bookListMain');
                $list.empty();
                const value = books.Value;


                for (let book of books.Value) {
                    $list.append(
                        `
                          <li class="list-group-item d-flex justify-content-between align-items-center">
                            <div><strong>${book.Title}</strong> - ${book.Author}</div>

                            <div>
                                <a href="/admin/bookPhoto/index?bookId=${book.Id}">

                                <button class="btn btn-sm btn-warning me-2">افزودن عکس</button>
                                
                                </a>
                                
                            </div>

                          </li>

                         
                        
                        `
                    );
                }

            },
            error: function () {
                alert('خط در برگزاری لیست کتاب ها ')
            }
        });
    }

    window.editBook = function (book) {

        $('#bookId').val(book.Id);
        $('#bookTitle').val(book.Title);
        $('#bookAuthor').val(book.Author);
        $('#bookPublisher').val(book.Publisher);
        $('#bookYear').val(book.Publication_Year);
        $('#bookIsbn').val(book.Isbn);
        $('#bookPages').val(book.Pages);
        /*$('#bookDescription').val(book.Description);*/
        CKEDITOR.instances['bookDescription'].setData(book.Description);
        $('#bookLanguage').val(book.Language);
        $('#bookCategoryId').val(book.CategoryId);
        $('#cancelEditBtn').show();

    }

    window.deleteBook = function (bookId) {
        if (!confirm('آیا مطمئن هستید')) return;


        $.ajax({
            url: '/admin/book/Delete/' + bookId,
            type: 'POST',


            success: function () {
                loadBook();
                loadBookMain();

            },
            error: function () {
                alert('خطا در حذف کتاب')
            }

        });


    };

    function resetForm() {
        $('#bookForm')[0].reset();
        $('#bookId').val('');
        $('#cancelEditBtn').hide();
    }
})