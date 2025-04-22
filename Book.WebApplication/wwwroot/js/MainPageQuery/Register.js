$(document).ready(function () {

    $("#registerForm").on('submit', function(e) {



        e.preventDefault();

        var formData = {

            Username : $('input[name="phoneNumber"]').val(),
            Password : $('input[name="password"]').val(),
            FirstName : $('input[name="firstName"]').val(),
            LastName : $('input[name="lastName"').val()

        };
        debugger

        $.ajax({
            url: '/register',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),

            success: function (response) {

                if (response.IsSuccess ) {
                    console.log("Login successful:", response);
                    window.location.href = "/home"
                } else {
                    console.warn('Login failed:', response.message);
                    alert('ثبت نام ناموفق بود: ');
                }
            },

            error: function (xhr, status, error) {
                console.log("Login failed : ", error)
                alert("لطفا موارد را به درستی پر کنید.")
            }

        });

    });


});