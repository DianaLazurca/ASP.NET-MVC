$("#helper").css("display", "block");

$.ajax({

    method: "GET",
    url: "http://localhost:7029/Domain/GetAllDomains",
    success: function (data) {

        $.each(data, function () {
            addNewDomain(this.Name, this.Id);
        });
    },
    error: function () {
        console.log("Domains retrieval request failed");
    }

});

$('#allDomains').on('click', 'a', function () {

    var currentSelectedDomain = $('#allDomains a[class ~= "active"]');
    $(currentSelectedDomain).removeClass('active');
    $(this).addClass('active');
    var domainId = $(this).attr('data-id');

    // get all subdomains for the selected somain
    $.ajax({
        method: "GET",
        url: "http://localhost:7029/Domain/GetSubdomains/" + parseInt(domainId),
        success: function (data) {

            var startTestToolsContainer = $("#startTestTools");
            var helper = $("#helper");
            var subdomainsContainer = $('#allSubdomains');

            $('#allSubdomains').empty();

            if (data.length) {
                helper.css("display", "none");
                startTestToolsContainer.css("display", "block");

                $.each(data, function () {
                    addNewSubdomain(this.Name, this.Id);
                });

            } else {
                helper.html("This domain doesn't have any subdomains added yet. Select another domain.");
                helper.css("display", "block");
                startTestToolsContainer.css("display", "none");
            }

        },
        error: function () {
            console.log("Subdomains retrieval request failed");
        }

    });

});

function addNewDomain(newDomainName, id) {

    var allDomains = $('#allDomains');
    var ahref = $('<a href="#" class="list-group-item" data-id= "' + id + '"> ' + newDomainName + '</a>');
    allDomains.append(ahref);

};

function addNewSubdomain(subdomainName, id) {

    var allSubdomains = $('#allSubdomains');
    var ahref = $('<a href="#" class="list-group-item" data-id= "' + id + '"> ' + subdomainName + '</a>');
    allSubdomains.append(ahref);

};

$('#allSubdomains').on('click', 'a', function () {

    //if ($(this).hasClass('active')) {
    //    $(this).removeClass('active');
    //} else {
    //    $(this).addClass('active');
    //}
    $(this).toggleClass('active');

});

$('#startTestBtn').on('click', function () {

    var duration = $('#startTestTime').val();
    var chosenSubdomains = {};
    chosenSubdomains["selectedSubdomains"] = [];

    if (duration == "") {
        alert("You must specify a time interval !");
        return false;
    } else {

        $.each($('#allSubdomains a[class ~= "active"]'), function () {

            chosenSubdomains["selectedSubdomains"].push(parseInt($(this).attr('data-id')));

        });
        var currentSelectedDomain = $('#allDomains a[class ~= "active"]');
        var domainId = $(currentSelectedDomain).attr('data-id');

        chosenSubdomains["domainId"] = parseInt(domainId);
        chosenSubdomains["duration"] = parseInt(duration);

        var jsonObj = JSON.stringify(chosenSubdomains);

        $.ajax({

            method: "POST",
            url: "http://localhost:7029/Test/GenerateTest/",
            contentType: 'application/json',
            data: jsonObj,
            success: function (data) {
                console.log("Success");
            },
            error: function () {
                console.log("Test generation request failed.");
            }

        });

    }

});