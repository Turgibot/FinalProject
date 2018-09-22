
$(function () {
    // Declare a proxy to reference the hub.
    var dataStream = $.connection.myHub;
    // Create a function that the hub can call to broadcast messages.
    dataStream.client.broadcastMessage = function (view, param1, param2, param3) {
        switch (view) {
            case "People":
                receivePeopleData(param1, param2);
                break;
        }
    };
    // Get the user name and store it to prepend to messages.
    $('#displayname').val(prompt('Enter your name:', ''));
    // Set initial focus to message input box.
    $('#message').focus();
    // Start the connection.
    $.connection.hub.start().done(function () {
        $('#sendmessage').click(function () {
            // Call the Send method on the hub.
            dataStream.server.send($('#displayname').val(), $('#message').val());
            // Clear text box and reset focus for next comment.
            $('#message').val('').focus();
        });
    });
});