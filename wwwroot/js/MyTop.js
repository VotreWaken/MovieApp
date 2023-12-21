
document.addEventListener('DOMContentLoaded', function () {
    function burgerMenu() {
        let burger = document.querySelector('.burger-menu');
        let menu = document.querySelector('.leftBarItems');
        burger.addEventListener('click', () => {
            if (!menu.classList.contains('active')) {
                menu.classList.add('active');
                burger.src = '/img/LeftBar/burger-close.svg';
                burger.classList.add('active-burger');
            } else {
                menu.classList.remove('active');
                burger.src = '/img/LeftBar/burger-menu.svg';
                burger.classList.remove('active-burger');
            }
        });

        window.addEventListener('resize', () => {
            if (window.innerWidth > 768) {
                menu.classList.remove('active');
                burger.src = '/img/LeftBar/burger-menu.svg';
                burger.classList.remove('active-burger');
            }
        });
    }
    burgerMenu();

    function fixedNav() {
        const nav = document.querySelector('.leftBar');
        const breakpoint = 500;
        if (window.scrollY >= breakpoint) {
            nav.classList.add('fixed__nav');
        } else {
            nav.classList.remove('fixed__nav');
        }
    }

    window.addEventListener('scroll', fixedNav);
});