var target_date = new Date();

function setMinutesToFinish(minutes) {
    target_date.setMinutes(target_date.getMinutes() + minutes);
}

setMinutesToFinish(parseInt($("#timeLeft").text()));

var hours, minutes, seconds;

var countdown = document.getElementById("timeLeft");

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
        $("#finishTestButton").trigger('click');
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

$("#finishTestButton").click(function (event) {
    event.preventDefault();
    event.stopPropagation();

    $(this).attr('disabled', 'disabled');
    stopTimer();
    countdown.innerHTML = "No time left.";

    var evaluation = {};

    var evaluationId = parseInt($("#evaluation").attr('data-id'));

    var givenAnswers = [];
    var givenJustifications = [];

    $(".question").each(function () {
        var questionId = $(this).attr('data-id');

        $(".question[data-id = '" + questionId + "'] .questionAnswer").each(function () {
            var answerId = $(this).attr('data-id');
            var givenAnswer = $(this).children('.answerCheck').first().is(":checked");

            givenAnswers.push({
                'EvaluationId': evaluationId,
                'QuestionId': questionId,
                'AnswerId': answerId,
                'GivenAnswer': givenAnswer
            });
        });

        var justification = $(".question[data-id = '" + questionId + "'] .questionJustification").val();

        givenJustifications.push({
            'EvaluationId': evaluationId,
            'QuestionId': questionId,
            'Justification': justification
        });
    });

    evaluation["EvaluationAnswers"] = givenAnswers;
    evaluation["EvaluationJustifications"] = givenJustifications;

    //console.log(JSON.stringify(evaluation));

    $.ajax({
        method: "POST",
        url: "http://localhost:7029/Evaluation/FinishTest/" + evaluationId,
        data: JSON.stringify(evaluation),
        contentType: 'application/json',
        success: function (data) {
            $("#correctAnswers > strong").text(data.CorrectlyAnsweredQuestionsCount);
            $("#incorrectAnswers > strong").text(data.IncorrectlyAnsweredQuestionsCount);
            $("#score > strong").text(data.Score);
            $("#result").removeClass("hidden");
        },
        error: function () {
            console.log("Something baad happened");
        },
        statusCode: {
            400: function () {
                console.log("Error at inserting a new question");
            }
        }

    });
});