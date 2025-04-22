(function ($) {
  "use strict";
  /*============= preloader js css =============*/
  var cites = [];
  cites[0] = "شیرجه رفتن به داستان، یک کلیک دور!";
  cites[1] = "میزبان رویدادهای چهره به چهره در شهرهای مختلف است";
  cites[2] = "خواندن عالی بعدی شما فقط با یک ضربه دور است!";
  cites[3] = "باز کردن قفل دنیاها، یک کتاب در یک زمان.";
  var cite = cites[Math.floor(Math.random() * cites.length)];
  $("#preloader p").text(cite);
  $("#preloader").addClass("loading");

  $(window).on("load", function () {
    setTimeout(function () {
      $("#preloader").fadeOut(500, function () {
        $("#preloader").removeClass("loading");
      });
    }, 500);
  });
})(jQuery);
