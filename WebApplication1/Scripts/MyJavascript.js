$.connection.hub.start()
    .done(function () { alert("Worked") })
    .fail(function () { alert("Not Worked") });