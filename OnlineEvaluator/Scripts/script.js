
$(document).ready(function () {
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
                // console.log(data
                $('#allSubdomains').empty();
                $.each(data, function (index) {
                    console.log(this);
                    addNewSubdomain(this.Name, this.Id, index);                    
                });
            },
            error: function () {
                console.log("something baad happened ");
            }

        });

    });
    $.ajax({
        method: "GET",
        url: "http://localhost:7029/Domain/GetAllDomains",
        success: function (data) {
            $.each(data, function (index) {
                addNewDomain(this.Name, this.Id, index);
                
            });
        },
        error: function () {
            console.log("Something baad happened");
        }

    });
});


// Create a new domain
$('#addNewDomainButton').click(function () {
    var newDomainName = $('#addNewDomainInput').val();
    $.ajax({
        method: "POST",
        url: "http://localhost:7029/Domain/Create/",
        data: { domainName: newDomainName },
        success: function(data) {
            console.log(data);
            addNewDomain(newDomainName, data.id, data.id);
            $('#addNewDomainInput').val('');
            $("#allDomains a:last-child").addClass("active");
            $("#allDomains a:last-child").trigger('click');
        },
        error : function() {
            console.log("Something baad happened");
        },
        statusCode: {
            401: function () {
                console.log("Error at inserting a new domain");
            }
        }

    });
});

// click pe  a => class active, toate subdomeniile + toate intrebarile din subdomeniul selectat 
// some back propagation? 
$('#allDomains').on('click', 'a', function () {
    
    //console.log(this);
});

$('#allDomains').on('click', 'i[class ~= "glyphicon-remove"]', function () {

    var parent = this.parentElement;
    var domainId = this.parentElement.attributes[2].value;

    $.ajax({
        method: "POST",
        url: "http://localhost:7029/Domain/Delete/" + parseInt(domainId),
        statusCode: {
            200: function () {
                $(parent).next().addClass('active');
                $(parent).next().trigger('click');
                console.log("un domeniu s-a sters 200 code");
                parent.remove();
                
            },
            404: function () {
                console.log("Something went wrong");
            }
        }

    });
});

$('#allDomains').on('click', 'i[class ~= "glyphicon-edit"]', function () {

    var parent = this.parentElement;
    var domainId = this.parentElement.attributes[2].value;
    console.log("Edit element " + domainId);

   //<a href="#" class="list-group-item">
   //                         Domain 3<i class="glyphicon glyphicon-remove pull-right"></i><i style="margin-right: 5px;" class="glyphicon glyphicon-edit pull-right"></i>
    //                     </a> 
    console.log($('#allDomains a[data-id = '+domainId+']').text());
    var NewElem = $('<a href="#" class="list-group-item" data-id= "' + id + '"> ' + newDomainName + '<i class="glyphicon glyphicon-remove pull-right"></i><i style="margin-right: 5px;" class="glyphicon glyphicon-edit pull-right"></i></a>');
    
});

//$('#addNewSubdomainButton').click(function () {
//    var newSubdomainName = $('#addNewSubdomainInput').val();
//    console.log("Create a new subdomain called" + newSubdomainName);

//});





function addNewDomain(newDomainName, id, index) {

    var allDomains = $('#allDomains');
    // console.log("Create a new domain called" + newDomainName);
    var ahref = $('<a href="#" class="list-group-item" data-id= "' + id + '"> ' + newDomainName + '<i class="glyphicon glyphicon-remove pull-right"></i><i style="margin-right: 5px;" class="glyphicon glyphicon-edit pull-right"></i></a>');
    allDomains.append(ahref);    

    if (index == 0) {
        ahref.addClass("active");

        ahref.trigger('click');
    }
    
}


$('#allSubdomains').on('click', 'a', function () {
    var currentSelectedSubdomain = $('#allSubdomains a[class ~= "active"]');
    $('#allSubdomains a[class ~= "active"]').removeClass('active');
    $(this).addClass('active');
    var subdomainId = $(this).attr('data-id');
    // get all quesrions for the selected somain
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
            console.log(data);
            addNewSubdomain(newSubdomainName, data.id, data.id);
        },
        error: function () {
            console.log("something baad happened");
        },
        statuscode: {
            401: function () {
                console.log("error at inserting a new subdomain");
            }
        }

    });
});

function addNewSubdomain(subdomainName, id, index) {
    var allSubdomains = $('#allSubdomains');
    // console.log("Create a new domain called" + newDomainName);
    var ahref = $('<a href="#" class="list-group-item" data-id= "' + id + '"> ' + subdomainName + '<i class="glyphicon glyphicon-remove pull-right"></i><i style="margin-right: 5px;" class="glyphicon glyphicon-edit pull-right"></i></a>');
    if (index == 0) {
        ahref.addClass("active");
    }
    allSubdomains.append(ahref);
}


