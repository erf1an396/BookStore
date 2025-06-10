$(document).ready(function () {

    loadUserList();

    function loadUserList() {
        $.ajax({
            url: `/admin/User/GetAll`,
            type: 'GET',
            success: function (res) {
                const users = res.Value || [];
                const $list = $('#userListMain');
                $list.empty();

                debugger
                users.forEach(user => {

                    $list.append(
                        `
                        <li class="list-group-item d-flex justify-content-between align-items-center">
                            <div><strong>${user.FirstName} ${user.LastName}</strong> - ${user.Email}</div>
                            
                            <div>

                                
                            

                                <a href="/admin/user/index?userId=${user.Id}">
                                    <button class="btn btn-sm btn-primary me-2" onclick='editUser(${JSON.stringify(user)})'>ویرایش</button>
                                </a>
                                
                                <button class="btn btn-sm btn-danger" onclick="deleteUser('${user.UserName}')">حذف</button>
                                  
                            </div>
                        </li>

                        `
                    );
                });
            },
            error: function () {
                alert(" خطا در بارگذاری لیست کاربران");
            }
        });

    }

    window.editUser = function (user) {

        $('#userId').val(user.Id);
        $('#userName').val(user.FirstName);
        $('#userLastName').val(user.LastName);
        $('#birthDay').val(user.BirthDay_Date);
        $('#email_address').val(user.Email);
        $('#phoneNumber').val(user.UserName);
        $(`input[name="gender"][value="${user.Gender}"]`).prop('checked', true);

        debugger
        $('#cancelEditBtn').show();
    }

    window.deleteUser = function (userName) {
        if (!confirm("آیا از حذف این کاربر مطمئن هستید؟")) return;

        $.ajax({
            url: `/admin/user/Delete`,
            type: 'POST',
            data: { userName },
            success: function () {
                alert("کاربر با موفقیت حذف شد");
                loadUserList();
                debugger    
            },
            error: function () {
                alert("خطا در حذف کاربر")
            }
        })
    }
})