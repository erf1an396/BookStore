$(document).ready(function () {

    loadAuthorList();
    function loadAuthorList() {
        $.ajax({
            url: '/admin/author/GetAll',
            type: 'GET',

            success: function (authors) {
                const $list = $('#authorListMain');
                $list.empty();
                const value = authors.Value;

                for (let author of authors.Value) {
                    $list.append(
                        `
                         <li class="list-group-item d-flex justify-content-between align-items-center">
                            <div><strong>${author.Name}</strong></div>

                            <div>
                                

                                <a href="/admin/author/Index?authorId=${author.Id}">

                               <button class="btn btn-sm btn-primary me-2" onclick='editBook(${JSON.stringify(author)})'>ویرایش</button>
                                
                                </a>


                                <button class="btn btn-sm btn-danger" onclick='deleteBook(${author.Id})'>حذف</button>

                                <a href="/admin/authorPhoto/Index?authorId=${author.Id}">

                                
                                <button class="btn btn-sm btn-warning me-2">افزودن عکس</button>
                                
                                </a>
                                
                            </div>
                             

                         </li>



                        `
                    );

                }
            },
            error: function () {
                alert('خطا در برگزاری لیست نویسنده ها')
            }

        });
    }

    window.editBook = function (author) {
        $('#authorId').val(author.Id);
        $('#authorName').val(author.Name);
        $('#authorBorn').val(author.Born_Year);
        $('#authorBook').val(author.Book_Count);
        $('#authorPrize').val(author.Prize_Count);
        $('#bookLanguage').val(author.Language);
        CKEDITOR.instances['authorDescription'].setData(author.Description);
        $('#cancelEditBtn').show();
    }

    window.deleteBook = function (authorId) {
        if (!confirm('آیا مطمئن هستید')) return;

        debugger
        $.ajax({
            url: '/admin/author/Delete/' + authorId,
            type: 'POST',


            success: function () {
                loadAuthorList();


            },
            error: function () {
                alert('خطا در حذف نویسنده')
            }

        });


    };
})