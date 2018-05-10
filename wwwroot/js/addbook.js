$(function() {
    $("#addBook-btn").click(function() {
        var _title = $('input[name="title"]').val();
        var _author = $('input[name="author"]').val();
        var _publishDate = $('input[name="publishDate"]').val();
        var _amount = $('input[name="amount"]').val();

        var _tr = '<tr><td>' + _title + '</td> <td>' + _author + '</td> <td>' + _publishDate + '</td><td>' + _amount + '</td></tr>';

        $('tbody').append(_tr);

        $('input').val("");
    });
});