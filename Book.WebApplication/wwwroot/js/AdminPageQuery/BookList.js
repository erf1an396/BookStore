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
})