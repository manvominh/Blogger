function MakeAddCommentButtonVisible() {
    var edValue = document.getElementById("commentText");
    var s = edValue.value;
    if (s.length === 0) {
        document.getElementById("btnAddComment").setAttribute("disabled", "true");
    } else {
        document.getElementById("btnAddComment").removeAttribute("disabled");
    }
}
function DisableAddCommentButton() {
    document.getElementById("btnAddComment").setAttribute("disabled", "true");
}