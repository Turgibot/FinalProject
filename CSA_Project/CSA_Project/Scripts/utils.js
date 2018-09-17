var email = "@User.Identity.Name";
var canv = document.getElementById('mycanvas');
var ctx = canv.getContext('2d');
var new_img = document.createElement('img');
var num_people = document.getElementById('num_people');
var max_people = document.getElementById('max_people').innerText;
var alert_txt = 'ALERT!!! Number of people exceeded allowed by ';
var alert_element = document.getElementById('alert_msg');
var body_element = document.getElementById('app-body');
var title_element = document.getElementById('title');
var status_msg_element = document.getElementById('status_msg');
var data_element = $('#meta_data');
var selected_view = data_element.attr('selected_view');
var server_url = data_element.attr('url');
var video_src = data_element.attr('video_src');
var people_src = data_element.attr('people_src');
var drowsiness_src = data_element.attr('drowsiness_src');
var panic_src = data_element.attr('panic_src');
var euclid_ip = data_element.attr('euclid_ip');
var euclid_port = data_element.attr('euclid_port');
var audio = new Audio();
var num_detections = 0;
var boxes = [];
var new_data = true;
var prev_id = 0;
var isDirty = false;
var delta = 0;
var prev_delta = 0;
var flick = null;
var flick_counter = 0;
var alertStatus = false;


$(document).ready(function () {

    audio.src = 'Sounds/alarm.wav';
    audio.loop = true;
    audio.autoplay = true;
    audio.muted = true;
    post_to_euclid(selected_view, server_url, euclid_ip, euclid_port);
    
    switch (selected_view) {
        case "People":
            new_img.setAttribute('src', people_src);
            //ajax get detection information from DB
            var get_people_itrvl = setInterval(getPeople, 250);
            var draw_rect_itrvl = setInterval(drawRect, 33);
            break;
        case "Drowsiness":
            status_msg_element.innerText = "";
            new_img.setAttribute('src', drowsiness_src);
            var show_drowsiness_itrvl = setInterval(showDrowsinessStream, 33);
            var get_drowsiness_itrvl = setInterval(getDrowsiness, 250);
            break;
        case "Panic":
            status_msg_element.innerText = "";
            new_img.setAttribute('src', panic_src);
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

var getDrowsiness = function () {
    $.get("http://localhost:53983/api/GetLastDrowsiness", function (data) {
        if (data.isAwake == false) {
            if (alertStatus == false) {
                flicker(true);
                alertStatus = true;
                audio.muted = false;
                status_msg_element.innerText = "WAKE UP!!!";
                alert_element.innerText = "HEY!!!!";
            }

        } else {
            if (alertStatus == true) {
                alert_element.innerText = "";
                status_msg_element.innerText = "";
                audio.muted = true;
                flicker(false);
                alertStatus = false;
            }


        }
    })
}

var drawRect = function () {
    ctx.drawImage(new_img, 0, 0, 640, 480);
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

var showDrowsinessStream = function () {
    ctx.drawImage(new_img, 0, 0, 640, 480);
    ctx.beginPath();
    ctx.stroke();
}
var post_to_euclid = function (selected_view, server_url, euclid_ip, euclid_port) {

    $.post("http://" + euclid_ip + ":" + euclid_port, {
        SelectedView: selected_view,
        ServerUrl: server_url
    });

}
var flicker = function (start) {
    if (start == true) {
        flick = setInterval(flash, 50);
    } else {
        if(flick != null)
            clearInterval(flick);
            body_element.setAttribute('style', 'background-color:white;');
            title_element.setAttribute('style', 'background-color:white;');
    }
}
var flash = function () {
    var color = makeRandomColor();
    body_element.setAttribute('style', 'background-color:' + color + ';');
    title_element.setAttribute('style', 'background-color:' + color + ';');
}

var makeRandomColor = function(){
    var c = '';
    while (c.length < 6) {
        c += (Math.random()).toString(16).substr(-6).substr(-1)
    }
    return '#' + c;
}