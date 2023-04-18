$(function() {

    $('#side-menu').metisMenu();

});

//Loads the correct sidebar on window load,
//collapses the sidebar on window resize.
// Sets the min-height of #page-wrapper to window size
$(function () {
    $(window).bind("load resize", function () {
        topOffset = 50;
        width = (this.window.innerWidth > 0) ? this.window.innerWidth : this.screen.width;
        if (width < 768) {
            $('div.navbar-collapse').addClass('collapse');
            if ($("#page-wrapper-dt").length) {
                //if ($("#side-menu-2-collapse").is(":hidden")) {
                $("#side-menu-2-collapse").css("height", "auto");
                //}
            } else {
                topOffset = 100; // 2-row-menu
            }
        } else {
            $('div.navbar-collapse').removeClass('collapse');
//            if ($("#page-wrapper-dt").length) {
//                if ($("#side-menu-2-collapse").is(":visible")) {
//                    $("#side-menu-2-collapse").css("overflow", "hidden");
//                }
//            }
        }

                height = ((this.window.innerHeight > 0) ? this.window.innerHeight : this.screen.height) - 1;
                height = height - topOffset;
                if (height < 1) height = 1;
                if (height > topOffset) {
                    $("#page-wrapper").css("min-height", (height) + "px");
                    // $("#page-wrapper-dt").css("min-height", (height) + "px");
                }

    });
    
    var url = window.location;
    var uhref = url.href.substring(url.href.lastIndexOf('/'));
    var element = $('ul.nav a').filter(function () {
        return this.href == url || (uhref.indexOf(this.href) == 0 && this.href != '/') || this.href == "/";
    }).addClass('in').parent();

    //.addClass('active').parent().parent().addClass('in').parent();

    if (element.is('li')) {
        element.addClass('active');
        element.siblings().find('a').css("outline", "none");
    }

});
