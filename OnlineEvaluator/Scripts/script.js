
$(document).ready(function () {
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
            addNewDomain(newDomainName, data.id);
            $('#addNewDomainInput').val('');
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
        url: "http://localhost/booking/Hotel/Delete/",
        data: { id: parseInt(hotelId) },

        success: function () {
            $('#hotelsContainer table tr[id=' + hotelId + ']').remove();
        }

    });

   // console.log(parent);
});

$('#addNewSubdomainButton').click(function () {
    var newSubdomainName = $('#addNewSubdomainInput').val();
    console.log("Create a new subdomain called" + newSubdomainName);
});

function addNewDomain(newDomainName, id, index) {

    var allDomains = $('#allDomains');
   // console.log("Create a new domain called" + newDomainName);
    var ahref = $('<a href="#" class="list-group-item" data-id= "'+id+'"> '+ newDomainName +'<i class="glyphicon glyphicon-remove pull-right"></i><i style="margin-right: 5px;" class="glyphicon glyphicon-edit pull-right"></i></a>');
    if (index == 0) {
        ahref.addClass("active");
    }
    allDomains.append(ahref);
}



