var target_date = new Date();

function setMinutesToFinish(minutes) {
    target_date.setMinutes(target_date.getMinutes() + minutes);
}

setMinutesToFinish(1);

var hours, minutes, seconds;

var countdownContainer = document.getElementById("timeleftContainer");

var timer = setInterval(function () { startTimer(); }, 100);

function startTimer() {

    var current_date = new Date().getTime();
    var seconds_left = (target_date - current_date) / 1000;

    seconds_left = seconds_left % 86400;
    hours = parseInt(seconds_left / 3600);
    seconds_left = seconds_left % 3600;
    minutes = parseInt(seconds_left / 60);
    seconds = parseInt(seconds_left % 60);

    if (hours != 0) {
        countdown.innerHTML = hours + "h, "
        + minutes + "m, " + seconds + "s";
    } else if (hours == 0 && minutes != 0) {
        countdown.innerHTML = minutes + "m, " + seconds + "s";
    } else if (hours == 0 && minutes == 0 && seconds != 0) {
        countdown.innerHTML = seconds + "s";
    } else if (hours == 0 && minutes == 0 && seconds == 0) {
        stopTimer();
        countdown.innerHTML = "No time left.";
    }
};

function stopTimer() {
    clearInterval(timer);

    $.each($("textarea"), function () {
        $(this).attr("readonly", "readonly");
    });


    $.each($(".answerCheck"), function () {
        $(this).attr("onclick", "return false");
    });

};