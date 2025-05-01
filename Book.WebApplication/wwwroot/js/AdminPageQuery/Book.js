$(document).ready(function () {

    const $addModal = new bootstrap.Modal(document.getElementById('addBookModal'));
    const $editModal = new bootstrap.Modal(document.getElementById('editBookModal'));
    const $deleteModal = new bootstrap.Modal(document.getElementById('deleteBookModal'));

    // بارگزاری اولیه لیست
    
    loadBooks();

    // 🔹 افزودن کتاب
    $('#addBookBtn').on('click', function () {
        const book = {
            Title: $('#bookTitle').val(),
            Author: $('#bookAuthor').val(),
            Publisher: $('#bookPublisher').val(),
            Publication_Year: parseInt($('#bookYear').val()),
            Isbn: $('#bookIsbn').val(),
            pages: parseInt($('#bookPages').val()),
            Description: $('#bookDescription').val(),
            Language: parseInt($('#bookLanguage').val()),
            CategoryId: parseInt($('#bookCategoryId').val())
        };

        $.ajax({
            url: '/admin/book/Create',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(book),
            success: function () {
                $addModal.hide();
                loadBooks();
            },
            error: function () {
                alert("خطا در افزودن کتاب");
            }
        });
    });

    // 🔹 ذخیره ویرایش
    $('#saveEditBookBtn').on('click', function () {
        const bookId = $('#editBookId').val();
        const book = {
            id: parseInt(bookId),
            title: $('#editBookTitle').val(),
            author: $('#editBookAuthor').val(),
            publisher: $('#editBookPublisher').val(),
            publication_Year: parseInt($('#editBookYear').val()),
            isbn: $('#editBookIsbn').val(),
            pages: parseInt($('#editBookPages').val()),
            description: CKEDITOR.instances['editBookDescription'].getData(),
            language: parseInt($('#editBookLanguage').val()),
            categoryId: parseInt($('#editBookCategoryId').val())
        };

        $.ajax({
            url: '/admin/book/Update',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(book),
            success: function () {
                $editModal.hide();
                loadBooks();
            },
            error: function () {
                alert("خطا در ویرایش کتاب");
            }
        });
    });

    // 🔹 حذف کتاب
    $('#confirmDeleteBookBtn').on('click', function () {
        const bookId = $('#deleteBookId').val();

        $.ajax({
            url: '/admin/book/Delete/' + bookId,
            type: 'POST',
            success: function () {
                $deleteModal.hide();
                loadBooks();
            },
            error: function () {
                alert("خطا در حذف کتاب");
            }
        });
    });

    // 🔹 بارگزاری لیست
    function loadBooks() {
        $.ajax({
            url: '/admin/book/GetAll',
            type: 'GET',
            success: function (books) {
                const $list = $('#bookList');
                $list.empty();

                const value = books.Value
               
                value.forEach(book => {
                    
                    $list.append(`
                        <li class="list-group-item d-flex justify-content-between align-items-center">
                            <div><strong>${book.Title}</strong> - ${book.Author}</div>
                            <div>
                                <button class="btn btn-sm btn-primary me-2" onclick='openEditBookModal(${JSON.stringify(book)})'>ویرایش</button>
                                <button class="btn btn-sm btn-danger" onclick='openDeleteBookModal(${book.Id})'>حذف</button>
                            </div>
                        </li>
                    `);
                });
                debugger
            },
            error: function () {
                alert("خطا در بارگذاری لیست کتاب‌ها");
            }
        });
    }

    // 🔹 توابع گلوبال برای باز کردن مدال‌ها
    window.openAddBookModal = function () {
        $('#bookTitle, #bookAuthor, #bookPublisher, #bookYear, #bookIsbn, #bookPages, #bookDescription, #bookLanguage, #bookCategoryId').val('');
        $addModal.show();
    };

    window.openEditBookModal = function (book) {
        $('#editBookId').val(book.id);
        $('#editBookTitle').val(book.title);
        $('#editBookAuthor').val(book.author);
        $('#editBookPublisher').val(book.publisher);
        $('#editBookYear').val(book.publication_Year);
        $('#editBookIsbn').val(book.isbn);
        $('#editBookPages').val(book.pages);
        $('#editBookLanguage').val(book.language);
        $('#editBookCategoryId').val(book.categoryId);

        if (CKEDITOR.instances['editBookDescription']) {
            CKEDITOR.instances['editBookDescription'].setData(book.description || '');
        }

        $editModal.show();
    };

    window.openDeleteBookModal = function (bookId) {
        $('#deleteBookId').val(bookId);
        $deleteModal.show();
    };
});
