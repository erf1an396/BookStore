

$(document).ready(function () {

    
    $("#loginForm").submit(function (e) {
        

        e.preventDefault();

        var formData = {
            email: $('input[name="phoneNumber"]').val(),
            password: $('input[name="password"]').val()

        };

        $.ajax({
            url: '/login',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),
            success: function (response) {
                console.log("Login successful:", response);
                window.location.href = "/home"
            },
            error: function (xhr, status, error) {
                console.log("Login failed : ", error)
                alert("ورود ناموفق بود, لطفا ایمیل و پسوورد خود را بررسی کنید .")
            }

        });
    });
});