$(document).ready(function () {

    jalaliDatepicker.startWatch();



    const urlParams = new URLSearchParams(window.location.search);
    const userIdFromUrl = urlParams.get('userId');

    if (userIdFromUrl) {
        $.ajax({
            url: `/admin/User/GetById?id=${userIdFromUrl}`,
            type: 'GET',
            success: function (res) {
                const user = res.Value;

                $('#userId').val(user.Id);
                $('#userName').val(user.FirstName);
                $('#userLastName').val(user.LastName);
                $('#birthDay').val(user.BirthDay_Date);
                $('#email_address').val(user.Email);
                $('#phoneNumber').val(user.UserName);
                $(`input[name="gender"][value="${user.Gender}"]`).prop('checked', true);

                $('#cancelEditBtn').show();
                $('#formTitle').text("ویرایش یوزر")
            },
            error: function () {
                alert("خطا در دریافت اطلاعات یوزر");
            }
        })
    }

    $('#UserForm').on('submit', function (e) {
        e.preventDefault();

        

        const userId = $('#userId').val();
        const isUpdate = !!userId;

        const password = $('#password').val();
        const confirmPassword = $('#confirmPassword').val();

        if (!isUpdate && (!password || password !== confirmPassword)) {
            alert("لطفا رمز عبور را وارد کرده و مطمئن شوید با تکرار آن یکسان است.")
            return
        }
        if (password && password !== confirmPassword) {
            alert(" رمز عبور و تکرار آن یکسان نیستند.");
            return;
        }



        const user = {
            Id: userId,
            FirstName: $('#userName').val(),
            LastName: $('#userLastName').val(),
            BirthDay_Date: $('#birthDay').val(),
            Email: $('#email_address').val(),
            UserName: $('#phoneNumber').val(),
            Gender: parseInt($('input[name="gender"]:checked').val()),

        };

        if (password) {
            user.Password = password;
            user.ConfirmPassword = confirmPassword;
        }

        const url = isUpdate ? `/admin/user/Update?UserName=${user.UserName}` : `/admin/user/Create`;

        $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(user),
            success: function () {
                alert(" عملیات با موفقیت انجام شد");
                resetForm();
                loadUserList();
            },
            error: function () {
                alert("خطا در ذخیره اطلاعات کاربر")
            }

        });

    });

    $('#cancelEditBtn').on('click', function () {
        resetForm();
    });

    function resetForm() {
        $('#UserForm')[0].reset();
        $('#userId').val('');
        $('#formTitle').text("➕ افزودن کاربر جدید");
        $('#cancelEditBtn').hide();
    }
    

});