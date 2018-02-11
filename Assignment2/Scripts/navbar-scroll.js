$(window).scroll(function () {
    if ($(this).scrollTop() > 80) {
        $('.navbar').addClass('navbar-scrolled');
    }
    else {
        $('.navbar').removeClass('navbar-scrolled');
    }
})