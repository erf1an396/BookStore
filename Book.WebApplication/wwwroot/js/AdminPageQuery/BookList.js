$(document).ready(function () {

    loadBookMain();

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
                            <div><strong>${book.Title}</strong></div>

                            <div>
                                

                                <a href="/admin/book/addbook?bookId=${book.Id}">

                               <button class="btn btn-sm btn-primary me-2" onclick='editBook(${JSON.stringify(book)})'>ویرایش</button>
                                
                                </a>


                                <button class="btn btn-sm btn-danger" onclick='deleteBook(${book.Id})'>حذف</button>

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
                alert('خطا در برگزاری لیست کتاب ها ')
            }
        });
    }



    window.editBook = function (book) {

        $('#bookId').val(book.Id);
        $('#bookTitle').val(book.Title);
        $('#bookAuthor').val(book.AuthorId);
        $('#bookPublisher').val(book.Publisher);
        $('#bookYear').val(book.Publication_Year);
        $('#bookIsbn').val(book.Isbn);
        $('#bookPages').val(book.Pages);
        /*$('#bookDescription').val(book.Description);*/
        CKEDITOR.instances['bookDescription'].setData(book.Description);
        CKEDITOR.instances['bookReview'].setData(book.Review);
        $('#bookLanguage').val(book.Language);
        $('#bookCategoryId').val(book.CategoryId);
        $('#bookPrice').val(book.Price);
        $('#cancelEditBtn').show();

    }

    window.deleteBook = function (bookId) {
        if (!confirm('آیا مطمئن هستید')) return;

        debugger
        $.ajax({
            url: '/admin/book/Delete/' + bookId,
            type: 'POST',


            success: function () {
                loadBookMain();


            },
            error: function () {
                alert('خطا در حذف کتاب')
            }

        });


    };
})