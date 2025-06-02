$(document).ready(function() {

    jalaliDatepicker.startWatch();

    //jalaliDatepicker.show();
   

    let originalDate = {};
    debugger

    $.ajax({
        url: "/User/UserGetById",
        type: "GET",
        
        success: function (response) {
            if (response.IsSuccess) {
                const user = response.Value;

                $("#firstName").val(user.FirstName);
                $("#lastName").val(user.LastName);
                $("#birthDate").val(user.BirthDay_Date);
                $("#email_address").val(user.Email);
                $("#phone").val(user.UserName);
                debugger

                if (user.Gender === 0) {
                    $("#genderMale").prop('checked', true);
                } else if (user.gender === 1) {
                    $("#genderFemale").prop('checked', true);
                }
                debugger

                originalDate = {
                    firstName : user.FirstName,
                    lastName : user.LastName,
                    birthDate : user.BirthDay_Date,
                    email  : user.Email,
                    phone : user.UserName,
                    gender : user.Gender
                }
            } else {
                alert('خطا در دریافت اطلاعات' + [response.message[0]]);
            }

        },
        error: function (error) {
            alert('خطا در ارتباط با سرور ');
        }
    });


    $("#userProfileForm input").on("input change", function () {


        const hasChanged =
            $("#firstName").val() !== originalDate.firstName ||
            $("#lastName").val() !== originalDate.lastName ||
            $("#birthDate").val() !== originalDate.birthDate ||
            $("#email_address").val() !== originalDate.email ||
            $("#phone").val() !== originalDate.phone ||
            $("input[name='gender_radio']:checked").val() != originalDate.gender ||
            $("#password").val() !== "";


        $("#saveBtn").prop("disabled", !hasChanged);
    })


    $("#userProfileForm").on("submit", function (e) {
        e.preventDefault();

        const newPassword = $("#password").val();
        const confirmPassword = $("#confirmPassword").val();

        if (newPassword !== confirmPassword) {
            alert("رمز عبور و تایید رمز عبور مطابقت ندارند.");
            return;
        }

        debugger
        const dataToSend = {
            FirstName: $("#firstName").val(),
            LastName: $("#lastName").val(),
            BirthDay_Date: $("#birthDate").val(),
            Email: $("#email_address").val(),
            UserName: $("#phone").val(),
            Gender: parseInt($("input[name='gender_radio']:checked").val()),
            Password: newPassword
        }
        $.ajax({
            url: "User/UserUpdate",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(dataToSend),
            success: function (response) {
                if (response.IsSuccess) {
                    alert('اطلاعات با موفقیت به‌روزرسانی شد.');
                    $("#saveBtn").prop("disabled", true);
                    $("#password").val("");
                    $("#confirmPassword").val("");
                    originalData = { ...dataToSend };
                    debugger
                } else {
                    alert("خطا: " + response.message[0]);
                }
            },
            error: function (error) {
                alert('خطا در ارتباط با سرور ');
            }
        });

    });


});