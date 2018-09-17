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
var video_src = "";
var server_url = "";
var selected_view = "";
var euclid_ip = "";
var euclid_port = "";
var prev_id = 0;
var isDirty = false;
var delta = 0;
var prev_delta = 0;

new_img.setAttribute('id', 'streamer');


$(document).ready(function () {

    var data_element = $('#meta_data');
    selected_view = data_element.attr('selected_view');
    server_url = data_element.attr('url');
    video_src = data_element.attr('video_src');
    euclid_ip = data_element.attr('euclid_ip');
    euclid_port = data_element.attr('euclid_port');
    post_to_euclid(selected_view, server_url, euclid_ip, euclid_port);

    new_img.setAttribute('src', "http://" + video_src);
    new_img.setAttribute('width', "640px");
    new_img.setAttribute('height', "480px");

    switch (selected_view) {
        case "People":
            //ajax get detection information from DB
            var get_people_itrvl = setInterval(getPeople, 250);
            var draw_rect_itrvl = setInterval(drawRect, 33);
            break;
        case "Drowsiness":

            break;
        case "Panic":

            break;
    }

   


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

var getPeople = function () {

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
                logAlert(delta, 600, "Over Populated System Idle");
            else
                logAlert(delta, 601, "Over Populated Space Alert");
        }
    })
}



var drawRect = function () {
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

}


var post_to_euclid = function (selected_view, server_url, euclid_ip, euclid_port) {

    $.post("http://" + euclid_ip + ":" + euclid_port, {
        SelectedView: selected_view,
        ServerUrl: server_url
    });

}