/// <reference path="jquery-3.2.1.js" />

$(document).ready(function () {

    var height1 = $('#content').height()
    var height2 = $('body').height()

    if (height1 > height2) {
        $('#sidebar').height(height1)
    } else {
        $('#sidebar').height(height2)
    }

    $('#sidebarCollapse').on('click', function () {
        // open or close navbar
        $('#sidebar').toggleClass('active');
        // close dropdowns
        $(this).toggleClass('active');
        $('.collapse.in').toggleClass('in');
        // and also adjust aria-expanded attributes we use for the open/closed arrows
        // in CSS
        $('a[aria-expanded=true]').attr('aria-expanded', 'false');
    });

});