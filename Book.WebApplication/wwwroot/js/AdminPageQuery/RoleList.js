$(document).ready(function () {
    loadRoleList();

    function loadRoleList() {
        $.ajax({
            url: '/admin/role/getall',
            type: 'GET',
            success: function (response) {
                const $list = $('#roleListMain');
                $list.empty();

                for (let role of response.Value) {
                    $list.append(`
                        <li class="list-group-item d-flex justify-content-between align-items-center">
                            <div><strong>${role.Name}</strong></div>
                            <div>
                                <a href="/admin/role/index?roleId=${role.Id}">
                                    <button class="btn btn-sm btn-primary me-2"> ویرایش</button>
                                </a>
                                <button class="btn btn-sm btn-danger" onclick="deleteRole('${role.Id}')"> حذف</button>
                            </div>
                        </li>
                    `);
                }
            },
            error: function () {
                alert('خطا در دریافت لیست نقش‌ها');
            }
        });
    }

    window.deleteRole = function (roleId) {
        if (!confirm("آیا از حذف نقش اطمینان دارید؟")) return;

        $.ajax({
            url: '/admin/role/delete/' + roleId,
            type: 'POST',
            success: function () {
                loadRoleList();
            },
            error: function () {
                alert('خطا در حذف نقش');
            }
        });
    };
});
