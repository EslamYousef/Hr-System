/*global $*/
/*global console*/
$(function () {
    'use strict';


    $('span.shrink').click(function () {
        $('.main-menu span.li').removeClass('active');
        if ($('.main-menu').hasClass('small')) {
            $('.main-menu span.li span').delay(300).show(100);
            $('.main-menu').animate({ width: '200px' });
            if ($('.item').length > 0) {
                $('.custom-navbar').show();
            }
            $('.main-menu').removeClass('small');
        } else {
            $('.main-menu').addClass('small');
            $('.custom-navbar').hide();
            $('.main-menu span.li span').hide();
            $('.main-menu').animate({ width: '50px' });
        }
    });

    $('.main-menu span.li').click(function () {
        $(this).siblings('.li').removeClass('active');
        $(this).addClass('active');
        if ($('#' + $(this).data('value')).css('display') === 'none') {
            $('#' + $(this).data('value')).siblings('ul').hide();
            $('#' + $(this).data('value')).siblings('ul').find('ul').hide();
            $('#' + $(this).data('value')).siblings('ul').find('li').removeClass('item');
            $('#' + $(this).data('value')).show();
            $('#' + $(this).data('value')).children('li').addClass('item');
            $('#' + $(this).data('value')).siblings('ul').find('i').removeClass('up');
            $('#' + $(this).data('value')).siblings('ul').find('i').addClass('down');
            $('.main-menu span.li span').delay(300).show(100);
            $('.main-menu').animate({width: '200px'});
            $('.main-menu').removeClass('small');
        } else {
            $('#' + $(this).data('value')).find('ul').hide();
            $('#' + $(this).data('value')).hide();
            //            $('#' + $(this).data('value')).children('li').removeClass('item');
            $('#' + $(this).data('value')).find('li').removeClass('item');
        }
        //New Menu
        if ($('.item').length > 26 && $('.item').length < 33) {
            $('.custom-navbar').css('columnCount', '2');
            $('.custom-navbar').animate({ columnWidth: '260px' });
        } else if ($('.item').length > 50 && $('.item').length < 49) {
            $('.custom-navbar').css('columnCount', '3');
            $('.custom-navbar').animate({ columnWidth: '260px' });
        } else if ($('.item').length > 50 && $('.item').length < 65) {
            $('.custom-navbar').css('columnCount', '3');
            $('.custom-navbar').animate({ columnWidth: '260px' });
        } else if ($('.item').length > 75 && $('.item').length < 81) {
            $('.custom-navbar').css('columnCount', '4');
            $('.custom-navbar').animate({ columnWidth: '260px' });
        } else if ($('.item').length >= 0 && $('.item').length < 17) {
            $('.custom-navbar').css('columnCount', '1');
            $('.custom-navbar').animate({ columnWidth: '260px' });
        }
        if ($('.item').length > 0) {
            $('.custom-navbar').show();
        } else {
            $('.custom-navbar').hide();
        }
    });

    $('ul li.children span').click(function () {
        $('.main-menu span').removeClass('active');
        $('.custom-navbar').find('span').removeClass('active');
        $(this).addClass('active');
        if ($(this).siblings('ul').css('display') === 'none') {
            $(this).children('i').removeClass('down');
            $(this).children('i').addClass('up');
            $(this).next().show();
            $(this).next('ul').children('li').addClass('item');
        }
        else
        {
            $(this).next().hide();
            $(this).siblings('ul').children('li.children').find('ul').hide();
            $(this).children('i').removeClass('up');
            $(this).children('i').addClass('down');
            $(this).siblings().find('i').removeClass('up');
            $(this).siblings().find('i').addClass('down');
            $(this).children('i').addClass('down');
            $(this).siblings('ul').children('li.children').find('ul').find('li').removeClass('item');
            $(this).next('ul').children('li').removeClass('item');
        }

        //New Menu
        if ($('.item').length > 26 && $('.item').length < 33) {
            $('.custom-navbar').css('columnCount', '2');
            $('.custom-navbar').animate({ columnWidth: '260px' });
        }
        else if ($('.item').length > 50 && $('.item').length < 49) {
            $('.custom-navbar').css('columnCount', '3');
            $('.custom-navbar').animate({ columnWidth: '260px' });
        }
        else if ($('.item').length > 50 && $('.item').length < 65) {
            $('.custom-navbar').css('columnCount', '3');
            $('.custom-navbar').animate({ columnWidth: '260px' });
        }
        else if ($('.item').length > 75 && $('.item').length < 81) {
            $('.custom-navbar').css('columnCount', '4');
            $('.custom-navbar').animate({ columnWidth: '260px' });
        } else if ($('.item').length > 0 && $('.item').length < 17) {
            $('.custom-navbar').css('columnCount', '1');
            $('.custom-navbar').animate({ columnWidth: '260px' });
        }
    });

});