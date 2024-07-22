/// <reference path="../lib/jquery/jquery.min.js" />

function Containes(v, array) {
    for (var i = 0; i < array.length; i++) {
        if (array[i] == v)
            return true;
    }
    return false;
}
Array.prototype.Contains = function (v) {
    return Containes(v, this)
}

function Distinct(array1, array2) {
    var distictArrayItems = [];
    for (var i = 0; i < array1.length; i++) {
        if (!array2.Contains(array1[i])) {
            distictArrayItems.push(array1[i])
        }
    }
    return distictArrayItems;
}

Array.prototype.DistinctFrom = function (array2) {
    return Distinct(this, array2);
}

$(function () {

    function AddTop_Munu_items_Border() {
        $('.nav-menu').children('li').each(function (index, element) {
            $(this).children('a').addClass('menu-item-border-bottom')
        });
    } 

    AddTop_Munu_items_Border();

    $('li').each(function (i, e) {
        $(this).attr('id', 'menu_' + i);
    });

    $('.left-aside-nav-menus a').click(function (e) {
        var activeIds = Array.prototype.slice.call($('.open-menu').map(function (i, e) { return $(e).attr('id'); })).sort();
        var parentIds = Array.prototype.slice.call($(this).parent('li').parents('.menu-item').map(function (i, e) { return $(e).attr('id'); })).sort();
        var currentActiveId = $(this).parent('.menu-item').attr('id');

        var distictIds = activeIds.DistinctFrom(parentIds);

        if ($(this).next('ul').length > 0) {
            $('ul').removeClass('open-menu')
            $(this).parent('.menu-item').toggleClass('open-menu');
            e.preventDefault();
        }


        var topMostParent = $('#' + parentIds[0]).children('a');
        if (topMostParent.length <= 0) {
            topMostParent = $(this);
        }
        

        if ($(this).parents('.menu-item').hasClass('open-menu')) {
            topMostParent.addClass('active-menu-item');
        }
        else {
            topMostParent.removeClass('active-menu-item');
        }

        $(this).toggleClass('text-white')

        $(distictIds).each(function (index, element) {
            $('#' + element).removeClass('open-menu').children('a').removeClass('text-white');
            
        });
    });


    $('#btnNavigationMenuClose').click(function (e) {
        CollpaseNavigationMenus();
        e.preventDefault();
        return false;
    }).mouseover(function () {
        $(this).removeClass('slide-left-animation');
    }).mouseleave(function () {
        $(this).addClass('slide-left-animation');
    });

    $('#btnNavigationMenuOpen').click(function (e) {
        unCollapseNavigationMenus()

        e.preventDefault();
    }).mouseover(function () {
        $(this).removeClass('slide-right-animation');
    }).mouseleave(function () {
        $(this).addClass('slide-right-animation');
    });

    function unCollapseNavigationMenus() {
        $('#btnNavigationMenuClose').removeClass('d-none');
        $('#btnNavigationMenuOpen').addClass('d-none');
        $('aside').width(400);
        $('#main_ocd_icon').removeClass('d-none');
        $(".highest-visiblity").removeClass('d-none');
        $(".lowest-visiblity").addClass('d-none');
        $('.search-panel').addClass('d-none')
    }

    function CollpaseNavigationMenus() {
        $('aside').width(70);
        $('#main_ocd_icon').addClass('d-none');
        $('#btnNavigationMenuOpen').removeClass('d-none');
        $('#btnNavigationMenuClose').addClass('d-none');

        $(".highest-visiblity").addClass('d-none');
        $(".lowest-visiblity").removeClass('d-none');
    }

    $('#btnSearch_Panel').click(function (e) {
        $('.search-panel').toggleClass('d-none')
        e.preventDefault();
    });

    $('.search-panel').addClass('d-none');


    $('#bodyRenderSection,#bodyRenderSection a').click(function () {
        CollpaseNavigationMenus();
    })
});