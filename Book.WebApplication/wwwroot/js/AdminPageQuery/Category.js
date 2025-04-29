let currentCategoryId = null;


$(document).ready(function () {
    loadCategories();

    
    $('#addCategoryBtn').on('click', function () {
        const Title = $('#addCategoryName').val();
        
        if (Title.trim()) {
            $.ajax({
                url: '/admin/category/Create',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ Title }),
                success: function () {

                    $('#addCategoryModal').modal('hide');
                    $('#addCategoryName').val('');
                    loadCategories();
                }
            });
        }
    });

    
    $('#deleteCategoryBtn').on('click', function () {
        debugger
        const Id = currentCategoryId
        $.ajax({
            url: '/admin/category/Delete/' + Id,
            type: 'POST',
            contentType: 'application/json',
            success: function (response) {
                if (response.IsSuccess) {
                    $('#deleteCategoryModal').modal('hide');
                    loadCategories();
                } else {
                    alert(response.Message);
                }
                
            }
        });
    });

    //close delete modal

    

   
    $('#editCategoryBtn').on('click', function () {

        const Title = $('#editCategoryName').val();
        const Id = currentCategoryId
        debugger
        $.ajax({
            url: '/admin/category/Update/'  + Id ,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({  Title }),
            success: function (response) {
                debugger
                if (response.IsSuccess) {
                    $('#editCategoryModal').modal('hide');
                    loadCategories();
                }
                debugger
            }
        });
    });

    
    $('#addChildBtn').on('click', function () {
        const Title = $('#childCategoryName').val();
        if (Title.trim()) {
            $.ajax({
                url: '/admin/category/Create',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ Title, ParentId: currentCategoryId }),
                success: function (response) {

                    if (response.IsSuccess) {
                        $('#addChildModal').modal('hide');
                        $('#childCategoryName').val('');
                        loadCategories();
                    }
                    
                }
            });
        }
    });

    
    function loadCategories() {
        
        $.get('/admin/category/GetAll', function (data) {

            const tree = buildTree(data.Value);
            
            $('#categoryTree').html(`<ul class="list-group">${renderTree(tree)}</ul>`);
        });
    }

    
    function buildTree(list, parentId = null) {
        
        return list
            .filter(c => c.ParentId == parentId)
            .map(c => ({
                ...c,
                children: buildTree(list, c.Id)
            }));
    }

    
    function renderTree(categories) {
        let html = '';
        categories.forEach(cat => {
            html += `
                <li class="list-group-item">
                    <div class="d-flex justify-content-between align-items-center">
                        <span>${cat.Title}</span>
                        <div>
                            <button class="btn btn-sm btn-primary me-1" onclick="openEdit(${cat.Id}, '${cat.Title}')">ویرایش</button>
                            <button class="btn btn-sm btn-danger me-1" onclick="openDelete(${cat.Id})">حذف</button>
                            <button class="btn btn-sm btn-warning" onclick="openAddChild(${cat.Id})">افزودن فرزند</button>
                        </div>
                    </div>
                    ${cat.children.length > 0 ? `<ul class="list-group mt-2 ms-4">${renderTree(cat.children)}</ul>` : ''}
                </li>
                <hr>
            `;
        });
        return html;
    }
});


function openEdit(id, name) {
    currentCategoryId = id;
    $('#editCategoryName').val(name);
    new bootstrap.Modal(document.getElementById('editCategoryModal')).show();
}

function openDelete(id) {
    currentCategoryId = id;
    new bootstrap.Modal(document.getElementById('deleteCategoryModal')).show();
}

function openAddChild(id) {
    currentCategoryId = id;
    $('#childCategoryName').val('');
    new bootstrap.Modal(document.getElementById('addChildModal')).show();
}
