var swiper = new Swiper('.book-image-slider', {
    slidesPerView: 1,
    spaceBetween: 10,
    loop: true,
    watchOverflow : true,
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev'
    }
});