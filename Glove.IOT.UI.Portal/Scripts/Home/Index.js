var ApiUrl = "http://localhost:63305/";
$(function () {
    $.ajax({
        type: "get",
        url: ApiUrl + "api/UserInfo/GetAllChargingData",
        data: {},
        success: function (data, status) {
            if (status == "success") {
                $("#div_test").html(data);
            }
        },
        error: function (e) {
            $("#div_test").html("Error");
        },
        complete: function () {

        }

    });
});

