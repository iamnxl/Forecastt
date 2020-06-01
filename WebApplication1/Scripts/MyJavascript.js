function getEmployees() {
    $.ajax({
        type: "POST",
        url: 'Home/UpdateGraph',
        data: {},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('#img1').attr('src',@Url.Action("Chart"))
        },
        complete: function () {
            Hide(); // Hide loader icon  
        },
        failure: function (jqXHR, textStatus, errorThrown) {
            alert("HTTP Status: " + jqXHR.status + "; Error Text: " + jqXHR.responseText); // Display error message  
        }
    });
}