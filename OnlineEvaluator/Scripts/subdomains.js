// la document ready ia subdomeniile pentru primul domeniu si apoi pentru primul subdomeniu

//$('#addNewSubdomainButton').click(function () {

//    var newSubdomainName = $('#addNewSubdomainInput').val();
//    // id-ul domeniului selectat
//    var id = $('#allDomains a[class ~= "active"]').attr('data-id');
//    console.log(id + newSubdomainName);
//    $.ajax({
//        method: "POST",
//        url: "http://localhost:7029/Subdomain/Create/",
//        data: { domainId: parseInt(id) , subdomainName : newSubdomainName},
//        success: function (data) {
//            console.log(data);
//            addNewSubdomain(newSubdomainName, data.id, data.id);
//        },
//        error: function () {
//            console.log("something baad happened");
//        },
//        statuscode: {
//            401: function () {
//                console.log("error at inserting a new subdomain");
//            }
//        }

//    });
//});

//function addNewSubdomain(subdomainName, id, index) {
//    var allSubdomains = $('#allSubdomains');
//    // console.log("Create a new domain called" + newDomainName);
//    var ahref = $('<a href="#" class="list-group-item" data-id= "' + id + '"> ' + subdomainName + '<i class="glyphicon glyphicon-remove pull-right"></i><i style="margin-right: 5px;" class="glyphicon glyphicon-edit pull-right"></i></a>');
//    if (index == 0) {
//        ahref.addClass("active");
//    }
//    allSubdomains.append(ahref);
//}

//$('#allDomains').on('click', 'a', function () {
//    var currentSelectedDomain = $('#allDomains a[class ~= "active"]');
//    $(currentSelectedDomain).removeClass('active');
//    $(this).addClass('active');
//    var domainId = $(this).attr('data-id');
//    // get all subdomains for the selected somain

//    $.ajax({
//        method: "GET",
//        url: "http://localhost:7029/Domain/GetSubdomains/" + parseInt(domainId),
//        success: function (data) {
//            // console.log(data
//            $('#allSubdomains').empty();
//            $.each(data, function (index) {
//                console.log(this);                
//                addNewSubdomain(this.Name, this.Id, index);
//            });
//        },
//        error: function () {
//            console.log("something baad happened ");
//        }

//    });

//});


//$('#allSubdomains').on('click', 'a', function () {
//    var currentSelectedSubdomain = $('#allSubdomains a[class ~= "active"]');
//    $('#allSubdomains a[class ~= "active"]').removeClass('active');
//    $(this).addClass('active');
//    var subdomainId = $(this).attr('data-id');
//    // get all quesrions for the selected somain
//});

$('#allQuestions').on('click', 'a:first-child', function(){
    // console.log(this);

    $.ajax({
        method: "POST",
        url: "http://localhost:7029/Test/GenerateTest/",
        contentType: 'application/json',
        data: JSON.stringify({
            'selectedSubdomains': [
               1,
               2,
               3,
               4
            ]
        }),
    success : function() {
         console.log("ceva");
        
    },
    error : function() {
        console.log("SOmething wrong happend with post to Generate Test")
    }

    });
});