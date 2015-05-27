$(document).ready(function () {
    $('#allDomains, #allSubdomains').on('click', 'a', function (event) {
        event.preventDefault();

        var parentContainer = $(this).parent();
        var parentContainerId = $(parentContainer).attr('id');

        var currentSelectedContainer = $('#' + parentContainerId + ' a[class ~= "active"]');
        $(currentSelectedContainer).removeClass('active');
        if ($(this).attr('data-id') !== $(currentSelectedContainer).attr('data-id')) {
            makeNonEditable(currentSelectedContainer);

            var id = $(this).attr('data-id');

            if (parentContainerId === "allDomains") {
                $('#allSubdomains').empty();
                $.ajax({
                    method: "GET",
                    url: "http://localhost:7029/Domain/GetSubdomains/" + parseInt(id),
                    success: function (data) {

                        $.each(data, function (index) {
                            addNewElement($("#allSubdomains"), this.Name, this.Id, index);
                        });
                    },
                    error: function () {
                        console.log("something baad happened ");
                    }

                });
            } else {
                //TO DO: load questions for subdomain
            }
        }

        $(this).addClass('active');
        
    });
    
    $.ajax({
        method: "GET",
        url: "http://localhost:7029/Domain/GetAllDomains",
        success: function (data) {
            $.each(data, function (index) {
                addNewElement($("#allDomains"), this.Name, this.Id, index);

            });
            
        },
        error: function () {
            console.log("Something baad happened");
        }

    });
});


// Create a new domain
$('#addNewDomainButton').click(function () {
    var newDomainName = $('#addNewDomainInput').val().trim();
    if (!newDomainName) {
        return;
    }
    $.ajax({
        method: "POST",
        url: "http://localhost:7029/Domain/Create/",
        data: { domainName: newDomainName },
        success: function (data) {
            console.log(data);
            addNewElement($("#allDomains"), newDomainName, data.id, data.id);
            $('#addNewDomainInput').val('');
            $("#allDomains a").last().trigger('click');
        },
        error: function () {
            console.log("Something baad happened");
        },
        statusCode: {
            409: function () {
                console.log("Error at inserting a new domain");
            }
        }

    });
});

$('#addNewSubdomainButton').click(function () {

    var newSubdomainName = $('#addNewSubdomainInput').val();
    // id-ul domeniului selectat
    var id = $('#allDomains a[class ~= "active"]').attr('data-id');
    console.log(id + newSubdomainName);
    $.ajax({
        method: "POST",
        url: "http://localhost:7029/Subdomain/Create/",
        data: { domainId: parseInt(id), subdomainName: newSubdomainName },
        success: function (data) {
            addNewElement($("#allSubdomains"), newSubdomainName, data.id, data.id);
            $('#addNewSubdomainInput').val('');
            $("#allSubdomains a").last().trigger('click');
        },
        error: function () {
            console.log("something baad happened");
        },
        statuscode: {
            409: function () {
                console.log("error at inserting a new subdomain");
            }
        }

    });
});


$('#allDomains').on('click', 'i[class ~= "glyphicon-remove"]', function (event) {
    event.preventDefault();
    event.stopPropagation();

    var parentContainer = $(this).parent();
    var domainId = parseInt($(parentContainer).attr("data-id"));

    $.ajax({
        method: "POST",
        url: "http://localhost:7029/Domain/Delete/" + parseInt(domainId),
        statusCode: {
            200: function () {
                if ($(parentContainer).hasClass('active')) {
                    $("#allDomains a").first().trigger('click');
                }
                    
                $("#allDomains a[data-id = '" + domainId + "']").remove();
            },
            404: function () {
                console.log("Something went wrong");
            }
        }

    });
});

$('#allDomains, #allSubdomains').on('click', 'i[class ~= "glyphicon-edit"]', function () {
    var parentContainer = $(this).parent();

    var id = parseInt($(parentContainer).attr("data-id"));
    var originalName = $(parentContainer).attr("data-name");

    $(parentContainer).empty();

    makeEditable(parentContainer, originalName);
});

$('#allDomains, #allSubdomains').on('click', 'i[class ~= "glyphicon-remove-circle"]', function (event) {
    event.preventDefault();
    event.stopPropagation();

    makeNonEditable($(this).parents("a[class ~= 'list-group-item']").get(0)).addClass('active');
});

function makeNonEditable(container) {
    var newContainer = $('<a href="#" class="list-group-item" data-id= "' + $(container).attr('data-id') + '" data-name="' + $(container).attr('data-name') + '"></a>');
    $(container).replaceWith(newContainer);

    var name = $(newContainer).attr('data-name');
    $(newContainer).text(name);
    $(newContainer).append($('<i class="glyphicon glyphicon-remove pull-right"></i><i style="margin-right: 5px;" class="glyphicon glyphicon-edit pull-right"></i></a>'));

    return newContainer;
}

function makeEditable(container, originalName) {
    var formGroup = $("<div class='form-group'></div>");

    var inputGroup = $("<div class='input-group'></div>");

    var input = $('<input style="max-width:1000px;" type="text" class="form-control input-sm" />');
    $(input).val(originalName);

    var okButton = $('<span class="input-group-addon"></span>');
    $(okButton).append($('<i class="glyphicon glyphicon-ok pull-right"></i>'));

    var cancelButton = $('<span class="input-group-addon"></span>');
    $(cancelButton).append($('<i class="glyphicon glyphicon-remove-circle pull-right"></i>'));

    $(inputGroup).append(input);
    $(inputGroup).append(okButton);
    $(inputGroup).append(cancelButton);
    $(formGroup).append(inputGroup);

    $(container).append(formGroup);
}

function addNewElement(container, elementName, id, index) {

    var ahref = $('<a href="#" class="list-group-item" data-id= "' + id + '" data-name="' + elementName + '">' + elementName + '<i class="glyphicon glyphicon-remove pull-right"></i><i style="margin-right: 5px;" class="glyphicon glyphicon-edit pull-right"></i></a>');
    container.append(ahref);

    if (index == 0) {
        ahref.trigger('click');
    }
}

//function addNewDomain(newDomainName, id, index) {
//    var allDomains = $('#allDomains');

//    var ahref = $('<a href="#" class="list-group-item" data-id= "' + id + '" data-name="' + newDomainName + '">' + newDomainName + '<i class="glyphicon glyphicon-remove pull-right"></i><i style="margin-right: 5px;" class="glyphicon glyphicon-edit pull-right"></i></a>');
//    allDomains.append(ahref);

//    if (index == 0) {
//        ahref.addClass("active");
//        ahref.trigger('click');
//    }
//}

//function addNewSubdomain(subdomainName, id, index) {
//    var allSubdomains = $('#allSubdomains');

//    var ahref = $('<a href="#" class="list-group-item" data-id="' + id + '" data-name="' + subdomainName + '">' + subdomainName + '<i class="glyphicon glyphicon-remove pull-right"></i><i style="margin-right: 5px;" class="glyphicon glyphicon-edit pull-right"></i></a>');

//    allSubdomains.append(ahref);

//    if (index == 0) {
//        ahref.addClass("active");
//        ahref.trigger('click');
//    }
//}


