var email = "@User.Identity.Name";
var canv = document.getElementById('mycanvas');
var ctx = canv.getContext('2d');
var new_img = document.createElement('img');
var num_people = document.getElementById('num_people');
var max_people = document.getElementById('max_people').innerText;
var alert_txt = 'ALERT!!! Number of people exceeded allowed by ';
var alert_element = document.getElementById('alert_msg');
var num_detections = 0;
var boxes = [];
var new_data = true;
new_img.setAttribute('id', 'streamer');
new_img.setAttribute('src', 'http://192.168.1.100:8080/stream?topic=/camera/color/image_raw');
var itrvl = setInterval(function () {
    ctx.drawImage(new_img, 0, 0);
    ctx.beginPath();
    ctx.lineWidth = 4;
    ctx.strokeStyle = 'red';
    ctx.font = '14px Arial';
    ctx.fillStyle = 'aqua';
    for (var i = 0; i < boxes.length > 0; i++) {
        values = boxes[i].split(':');
        ctx.rect(values[1], values[2], values[3], values[4]);
        ctx.fillText("Person " + values[0] + "%", parseInt(values[1]) + 10, parseInt(values[2]) + 15);
    }
    ctx.stroke();

}, 33)

$(document).ready(function () {
    var prev_id = 0;
    var isDirty = false;
    var delta = 0;
    var prev_delta = 0;
    //ajax get detection information from DB
    var get_itrvl = setInterval(function () {

        $.get("http://localhost:53983/api/GetLastDetection", function (data) {
            if (data.id != prev_id) {
                delta = data.numberOfPeople - max_people;
                prev_id = data.id;
                num_detections = data.numberOfPeople;
                num_people.innerHTML = num_detections;
                if (delta > 0) {
                    alert_element.innerText = alert_txt + delta;
                } else {
                    alert_element.innerText = "";
                }
                boxes = data.boxesValue;

            }
            if (delta != prev_delta) {
                prev_delta = delta;
                if (delta <= 0)
                    logAlert(delta, 600, "System Idle");
                else
                    logAlert(delta, 601, "Over Populated Space Alert");
            }


        })
    }, 100)

})
var logAlert = function (delta, code, msg) {

    $.post("http://localhost:53983/api/Logger", {
        Email: email,
        DateTime: new Date().toISOString(),
        Code: code,
        Key: "Delta",
        Value: delta,
        Message: msg
    });

}