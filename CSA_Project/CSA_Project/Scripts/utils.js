//var email = "@User.Identity.Name";
var canv = document.getElementById('mycanvas');
var ctx = canv.getContext('2d');
var new_img = document.createElement('img');
var num_people = document.getElementById('num_people');
var max_people_element = document.getElementById('max_people');
var alert_element = document.getElementById('alert_msg');
var body_element = document.getElementById('app-body');
var title_element = document.getElementById('title');
var status_msg_element = document.getElementById('status_msg');
var alert_txt = 'ALERT!!! Number of people exceeded allowed by ';

var selected_view = "";
var max_people = "";
var server_url = "";
var people_src = "";
var drowsiness_src = "";
var panic_src = "";
var euclid_ip = "";
var euclid_port = "";

var show_stream_itrvl = null;
var draw_rect_itrvl = null;
var audio = new Audio();
var num_detections = 0;
var boxes = [];
var incomingData = false;
var prev_id = 0;
var isDirty = false;
var delta = 0;
var prev_delta = 0;
var flick = null;
var flick_counter = 0;
var alertStatus = false;
var userName = "UserName";
var userRoll = "UserRoll";


$(document).ready(function () {
    userName = getCookie(userName);
    userRoll = getCookie(userRoll);
    getMetaDataThenRun();
    audio.src = 'Sounds/alarm.wav';
    audio.loop = true;
    audio.autoplay = true;
    audio.muted = true;
})
var logAlert = function (delta, code, msg) {

    $.post("http://localhost:53983/api/Logger", {
        Email: userName,
        DateTime: new Date().toISOString(),
        Code: code,
        Key: "Delta",
        Value: delta,
        Message: msg
    });

}

var receivePeopleData = function (numberOfPeople, boxesValue) {
    incomingData = true;
    delta = numberOfPeople - max_people;
    num_people.innerHTML = numberOfPeople;
    if (delta > 0) {
        alert_element.innerText = alert_txt + delta;
    } else {
        alert_element.innerText = "";
    }
    boxes = boxesValue;

      
    if (delta !== prev_delta) {
        prev_delta = delta;
        if (delta <= 0)
            logAlert(delta, 600, "Over Populated System Idle");
        else
            logAlert(delta, 601, "Over Populated Space Alert");
    }

}


var getPanic= function () {

    $.get("http://localhost:53983/api/GetLastPanic", function (data) {
        if (data.id != prev_id) {
            prev_id = data.id;

            isPistol = data.IsPistol;
            num_people.innerHTML = num_detections;
            if (isPistol==true) {
                alert_element.innerText = alert_txt;
                //flicker(true);
            } else {
                alert_element.innerText = "";
                //flicker(false);
            }
            boxes = data.boxesValue;

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
    if (incomingData == false) {
        return;
    }
    ctx.beginPath();
    ctx.lineWidth = 4;
    ctx.strokeStyle = 'red';
    ctx.font = '18px Arial';
    ctx.fillStyle = 'aqua';
    for (var i = 0; i < boxes.length > 0; i++) {
        values = boxes[i].split(':');
        ctx.rect(values[1] * 2, values[2] * 2, values[3] * 2, values[4] * 2);
        ctx.fillText(selected_view+" " + values[0] + "%", parseInt(values[1]) * 2 + 10, parseInt(values[2]) * 2 + 30);
    }
    ctx.stroke();

}

var post_to_euclid = function () {

    $.post(euclid_ip + ":" + euclid_port, {
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

var getMetaDataThenRun = function () {

    $.get("/api/SelectorModelsWebAPI", function (data) {
        selected_view = data[0].selectedValue;
        document.getElementById('selected_view').innerText = selected_view;
        getSettings();
    });

}

var getSettings = function () {

    $.get("/api/SettingsWebAPI", function (data) {
        var http = "http://";
        var port = ":8080";
        max_people = data[0].maxPeopleAllowed;
        server_url = data[0].serverIP;
        euclid_ip = http + data[0].euclidIP ;
        euclid_port = data[0].euclidPort;
        people_src = euclid_ip + port+ data[0].peopleTopic;
        drowsiness_src = euclid_ip + port+ data[0].drowsinessTopic;
        panic_src = euclid_ip + port+ data[0].panicTopic;
        max_people_element.innerText = max_people;
        post_to_euclid();
        run()
    });
}


var run = function () {

    switch (selected_view) {
        case "People":
            new_img.setAttribute('src', people_src);
            show_stream_itrvl = setInterval(showStream, 33);
            draw_rect_itrvl = setInterval(drawRect, 33);

            //ajax get detection information from DB
            //var get_people_itrvl = setInterval(getPeople, 250);
            break;
        case "Drowsiness":
            status_msg_element.innerText = "";
            new_img.setAttribute('src', drowsiness_src);
            //var show_drowsiness_itrvl = setInterval(showDrowsinessStream, 33);
            //var get_drowsiness_itrvl = setInterval(getDrowsiness, 100);
            break;
        case "Panic":
            status_msg_element.innerText = "";
            new_img.setAttribute('src', panic_src);
            //var get_panic_itrvl = setInterval(getPanic, 250);
            //var draw_rect_itrv2 = setInterval(drawRect, 33);
            break;
    }
}

var showStream = function () {
    ctx.drawImage(new_img, 0, 0, 640, 480);
}


function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}
