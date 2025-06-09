$(document).ready(function () {
    const urlParams = new URLSearchParams(window.location.search);
    const roleId = urlParams.get('roleId');

   

    function tickLoad() {
        if (roleId) {
            $.ajax({
                url: `/admin/role/GetById?id=${roleId}`,
                type: 'GET',
                success: function (res) {

                    const role = res.Value;
                    $('#roleId').val(role.Id);
                    $('#roleName').val(role.Name);
                    debugger

                    const selectedClaims = role.RoleClaimVM.map(x => x.ClaimValue);
                    $('#permissionList input[type="checkbox"]').each(function () {
                        if (selectedClaims.includes($(this).val())) {
                            $(this).prop('checked', true);
                        }

                    });




                    $('#cancelEditBtn').show();
                    $('#formTitle').text("ویرایش نقش");


                },
                error: function () {
                    alert("خطا در دریافت اطلاعات نقش")

                }
            });
        }
    }

    $('#roleForm').on('submit', function (e) {
        e.preventDefault();

        const id = $('#roleId').val();
        const name = $('#roleName').val();
        const selectedClaims = $('#permissionList input:checked').map(function () {
            return this.value;
        }).get();


        if (!name || name.length >= 50) {
            alert("لطفا نام نقش را به درستی وارد کنید");
            return;

        };
        

        const roleData = {
            Id: id,
            Name: name,
            ClaimValue: selectedClaims

        };

        const url = id ? '/admin/role/Update' : '/admin/role/Insert';

        $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(roleData),
            success: function () {
                alert("نقش ذخیره شد");
                resetForm();
                debugger
                window.location.href = '/admin/role/list'

            },
            error: function () {
                alert("خطا در ذخیره نقش");
            }
        })
    });

    $('#cancelEditBtn').on('click', function () {
        resetForm();
    })

    function resetForm() {
        $('#roleForm')[0].reset();
        $('#roleId').val('');
        $('#cancelEditBtn').hide();
        $('#permissionList input[type="checkbox"]').prop('checked', false);
    }
    loadPermissions();
    
    function loadPermissions() {
        $.ajax({
            url: `/admin/role/GetById?id=00000000-0000-0000-0000-000000000000`,
            type: 'GET',
            success: function (res) {
                const permisstions = res.Value.AllControllerVms;
                const actionId = res.Value.AllControllerVms[0].MvcActions[0].ActionId;
                console.log(actionId);
                const $list = $('#permissionList');
                $list.empty();
                debugger

                

                for (let item of permisstions) {
                    let html = `
        <div class="col-md-12 mb-3">
            <div class="item-content border p-3 rounded ">
                <label class="fw-bold d-block mb-2">${item.ControllerDisplayName}</label>
    `;

                    for (let action of item.MvcActions) {
                        html += `
            <div class="form-check form-check-inline me-4">
                <input class="form-check-input" type="checkbox" value="${action.ActionId}" id="chk_${action.ActionId}">
                <label class="form-check-label" for="chk_${action.ActionId}">
                    ${action.ActionDisplayName}
                </label>
            </div>
        `;
                    }

                    html += `
            </div>
        </div>
    `;

                    $list.append(html);
                }

                tickLoad();
            },
            error: function () {
                alert("خطا در دریافت لیست دسترسی‌ها");
            }
        })
    }
})