$("#createButton").click(function () {
    $.ajax({
        url: "/Auctions/Create",
        method: "get"
    }).done(function (response) {
        $("#actionArea").html(response);
    })
});

$("#saveButton").click(function () {
    $.ajax({
        url: "/Auctions/Create/",
        method: "post",
        data: $("#createForm").serialize()
    }).done(function (response) {
        $("#listingArea").html(response);
    })
});


$(".editButton").click(function () {
    $.ajax({
        url: "/Auctions/Edit",
        method: "get",
        data: { id : $(this).attr('data-id') }
    }).done(function (response) {
        $("#actionArea").html(response);
    })
});


$("#updateButton").click(function () {
    $.ajax({
        url: "/Auctions/Edit/",
        method: "post",
        data: $("#editForm").serialize()
    }).done(function (response) {
        $("#listingArea").html(response);
    })
});

$(".deleteButton").click(function () {
    if (confirm("Are you sure you want delete?")) {
        $.ajax({
            url: "/Auctions/Delete",
            method: "post",
            data: { id: $(this).attr('data-id') }
        }).done(function (response) {
            $("#listingArea").html(response);
        })
    }
    else {

    }
    
});