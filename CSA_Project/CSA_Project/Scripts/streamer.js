
$(function () {
    // Declare a proxy to reference the hub.
    var dataStream = $.connection.myHub;
    // Create a function that the hub can call to broadcast messages.
    dataStream.client.broadcastMessage = function (view, param1, param2, param3) {
        incomingData = true;
        switch (view) {
            case "People":
                handlePeopleData(param1, param2);
                break;
            case "Drowsiness":
                handleDrowsinessData(param1);
                break;
            case "Panic":
                handlePanicData(param1, param2);
                break;
            default:
                showErrorInCanvas();
                break;
        }
    };
    
    $.connection.hub.start().done(function () {
       
    });
});