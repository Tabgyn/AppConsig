/* Sidebar Collapse */
$("#sidebar-collapse").on("click", function () {
    createCookie("menu-compact", $(".page-sidebar").hasClass("menu-compact"), 100);
});

function CheckMenuCompact() {
    if (readCookie("menu-compact") != null) {
        if (readCookie("menu-compact") === "true") {
            $(".page-sidebar").addClass("menu-compact");
            $(".sidebar-collapse").addClass("active");
        }
    }
}
/* End Sidebar Collapse */
