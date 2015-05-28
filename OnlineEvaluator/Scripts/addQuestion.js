(function () {
    $('#addQuestionModal').on('hidden.bs.modal', function () {
        clearModal();
    })
    $("#addQuestionButton").click(function (event) {
        event.preventDefault();
        event.stopPropagation();

        $("#addQuestion").text('Add question');
        $("#addQuestionModal").modal("show");
    });

    $('#allQuestions').on('click', 'i[class ~= "glyphicon-edit"]', function () {
        var parentContainer = $(this).parent();

        var id = parseInt($(parentContainer).attr("data-id"));

        $.ajax({
            method: "GET",
            url: "http://localhost:7029/Question/Details/" + id,
            success: function (data) {
                populateModal(data);
                $("#addQuestion").text('Edit question');
                $("#addQuestionModal").modal("show");
            },
            error: function () {
                console.log("Something baad happened");
            },
            statusCode: {
                404: function () {
                    console.log("Error at inserting a new question");
                }
            }
        });
    });

    function attachDeleteAnswerEvent() {
        $(".deleteAnswer").click(function () {

            var answersContainer = $("#qAnswersTableBody");
            var caller = $(this);
            var answerRow = caller.parents(".aRow");

            answerRow.remove();
        });

    };

    $("#addAnswer").click(function () {

        var answerBodyContainer = $("#aBody");
        var answerTypeContainer = $("#aType");
        var answersContainer = $("#qAnswersTableBody");

        var answerBody = answerBodyContainer.val();

        var answerType;
        if (answerTypeContainer.prop('checked'))
            answerType = "true";
        else
            answerType = "false";

        if (answerBody == "")
            alert("All fields required !");
        else {
            answersContainer.append("<tr class='aRow'><td>" + answerBody + "</td><td>" + answerType + "</td><td><button class='btn deleteAnswer' type='button'>Remove</button></tr>");
            attachDeleteAnswerEvent();
        }

    });

    $("#addQuestion").click(function () {
        var subdomainId = $('#allSubdomains a[class ~= "active"]').attr('data-id');

        var question = {};

        question["Text"] = $("#qBody").val();
        question["Justification"] = $("#aJust").val();

        var questionBody = $("#qBody").val();
        var questionJustification = $("#aJust").val();
        var questionIsMultiple = false;
        var choicesCounter = 0;
        var answers = {};

        var allAnswers = [];

        $('#qAnswersTableBody > tr').each(function () {
            var answerBody = $(this).children().eq(0).html();
            var answerType = $(this).children().eq(1).html();
            answers[answerBody] = answerType;

            if (answerType == "true") {
                choicesCounter++;

                allAnswers.push({ "Text": answerBody, "IsCorect": true });
            } else {
                allAnswers.push({ "Text": answerBody, "IsCorect": false });
            }


        });

        if (choicesCounter > 1) {
            questionIsMultiple = true;
        }

        question["Answers"] = allAnswers;

        question["IsMultiple"] = questionIsMultiple;

        question["SubdomainId"] = parseInt(subdomainId);


        var id = $("#addQuestionModal").attr('data-id');
        if (id) {
            $.ajax({
                method: "POST",
                url: "http://localhost:7029/Question/Edit/" + id,
                data: JSON.stringify(question),
                contentType: 'application/json',
                success: function () {
                    $("#allQuestions a[data-id = '" + id + "']").text(questionBody);
                    $("#allQuestions a[data-id = '" + id + "']").append($('<i class="glyphicon glyphicon-remove pull-right"></i><i style="margin-right: 5px;" class="glyphicon glyphicon-edit pull-right"></i></a>'));

                    $("#addQuestionModal").modal("hide");
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
        } else {
            $.ajax({
                method: "POST",
                url: "http://localhost:7029/Question/Create/",
                data: JSON.stringify(question),
                contentType: 'application/json',
                success: function (data) {
                    addNewElement($("#allQuestions"), data.text, data.id, data.id);

                    $("#addQuestionModal").modal("hide");
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
        }


    });

    function clearModal() {
        $("#qBody").val('');
        $("#aJust").val('');

        $("#aBody").val('');
        $("#aType").attr('checked', false);

        $("#qAnswersTableBody").empty();

        $("#addQuestionModal").removeAttr('data-id');
    }

    function populateModal(questionData) {
        $("#qBody").val(questionData.Text);
        $("#aJust").val(questionData.Justification);

        var answersContainer = $("#qAnswersTableBody");

        for (var i in questionData.Answers) {
            var answerBody = questionData.Answers[i].Text;
            var answerType = questionData.Answers[i].IsCorect;

            answersContainer.append("<tr class='aRow'><td>" + answerBody + "</td><td>" + answerType + "</td><td><button class='btn deleteAnswer' type='button'>Remove</button></tr>");
            attachDeleteAnswerEvent();
        }

        $("#addQuestionModal").attr('data-id', questionData.Id);
    }
})();