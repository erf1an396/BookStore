$(document).ready(function () {

    loadUsers();

    function loadUsers() {
        $.get('/admin/userroles/getall', function (res) {
            const users = res.Value;
            const $list = $('#userList');
            $list.empty();

           
            for (let user of users) {
                const html = `
                <div class="border rounded p-3 mb-2 d-flex justify-content-between align-items-center">
                        <div>
                          (${user.Email})   <strong>${user.FirstName} - ${user.LastName}</strong> 
                        </div>
                        
                        <div>
                            <button class="btn btn-sm btn-secondary" onclick="openRoleModal('${user.Id}')">نقش‌ها</button>
                        </div>
                    </div>

                `;
                $list.append(html);
            };
        });
    };


    window.openRoleModal = function (userId) {
        $('#selectedUserId').val(userId);
        
        $.get('/admin/userroles/getallroles', function (res) {

            const roles = res.Value;
            const $container = $('#rolesCheckboxList');
            $container.empty();


            for (let role of roles) {
                const checkbox = `

                <div class="col-md-12 ">
                        <div class="form-check form-check-inline ">
                            <input class="form-check-input role-checkbox" type="checkbox" value="${role.Name}" id="chk_${role.Id}">
                            <label class="form-check-label" for="chk_${role.Id}">${role.Name}</label>
                        </div>
                    </div>
                `;
                $container.append(checkbox);
              
            };

        });

        $.get(`/admin/userroles/GetAllUserRoles?userId=${userId}`, function (res2) {
            const userRoles = res2.Value;

            

            for (let role of userRoles) {
                $(`#chk_${role}`).prop('checked', true);
                debugger
            };

            $('#rolesModal').modal('show');
            
        })
    };

    $('#saveRolesBtn').on('click', function () {
        const userId = $('#selectedUserId').val();
        const selectedRoles = [];

        $('.role-checkbox:checked').each(function () {
            selectedRoles.push($(this).val());
        });

        const data = {
            UserId: userId,
            SelectedRoles: selectedRoles
        }

        $.ajax({
            url: '/admin/userroles/update',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function () {

                alert('نقش‌ها با موفقیت ذخیره شدند.');
                $('#roleModal').modal('hide');
            },
            error: function () {

                alert('خطا در ذخیره نقش‌ها');
            }
        });

    });

})