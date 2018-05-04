// Get the modal
var modal3 = document.getElementById('id03');

// When the user clicks anywhere outside of the modal, close it
window.onclick = function(event) {
    if (event.target == modal3) {
        modal3.style.display = "none";
    }
}
/*
$("#addNewBook").click(function() {
    $.get("Views/Home/SpecialOrd", function(data, status) {
        var markup = "<li>" + data.title + "</li>";
        $("#bookList").append(markup);
    })
});
*/