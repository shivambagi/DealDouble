////$("#createBtn").click(function () {
////    $.ajax({
////        url: '/Auctions/Create',
////        method: 'Get',
////    })
////        .done(function (response) {
////            $("#formModal").modal("toggle");
////            $("#actionArea").html(response);
////        })
////});

////$("#saveBtn").click(function () {
////    $.ajax({
////        url: '/Auctions/Create',
////        type: 'Post',
////        data: $("#createForm").serialize(),
////    }).done(function (response) {
////        $("#formModal").modal("toggle"); //to dismiss modal after finish
////        $("#listingArea").html(response);
////    }).fail(function () {
////        alert("Fail");
////    });
////});


////$(".editBtn").click(function () {
////    var id = $(this).attr("data-id");
////    $.ajax({
////        url: '/Auctions/Edit',
////        method: 'Get',
////        data: {
////            id: id,
////        }
////    })
////        .done(function (response) {
////            $("#formModal").modal("toggle");
////            $("#actionArea").html(response);
////        })
////});


////$("#updateBtn").click(function () {
////    $.ajax({
////        url: '/Auctions/Edit',
////        method: 'Post',
////        data: $("#editForm").serialize(),
////    }).done(function (response) {
////        $("#formModal").modal("toggle"); //to dismiss modal after finish
////        $("#listingArea").html(response);
////    }).fail(function () {
////        alert("Fail");
////    });
////});

////$(".deleteBtn").click(function () {
////    $("#deleteModal").modal("toggle");
////    $("#modalDeleteBtn").attr("data-id", $(this).attr("data-id"));
////});



////$("#modalDeleteBtn").click(function () {
////    debugger;
////    var id = $(this).attr("data-id");
////    $.ajax({
////        url: '/Auctions/Delete',
////        method: 'Post',
////        data: {
////            id: id,
////        }
////    })
////        .done(function (response) {
            
////            $("#listingArea").html(response);
////            $("#deleteModal").modal('toggle');
////        })
////});