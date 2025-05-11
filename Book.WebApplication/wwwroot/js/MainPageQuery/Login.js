

$(document).ready(function () {

    
    $("#loginForm").submit(function (e) {
        

        e.preventDefault();

        var formData = {
            Username: $('input[name="phoneNumber"]').val(),
            Password: $('input[name="password"]').val()
            

        };

        debugger 

        console.log("hello");
        $.ajax({
            url: '/login',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),
            success: function (response) {

                if (response.IsSuccess) {
                    console.log("Login successful:", response);
                    //fetch('/Auth/SetToken', {
                    //    method: 'POST',
                    //    headers: {
                    //        'Content-Type': 'application/json'
                    //    },
                    //    body: JSON.stringify({ token: response.value })
                    //}).then(() => {
                    //    window.location.href = "/admin";
                    //});
                    window.location.href = "/admin";
                } else {
                    console.warn('Login failed:', response.message);
                    alert('ورود ناموفق بود: ');
                }
                
            },

            error: function (xhr, status, error) {
                console.log("Login failed : ", error)
                alert("ورود ناموفق بود, لطفا ایمیل و پسوورد خود را بررسی کنید .")
            }

        });
    });
});