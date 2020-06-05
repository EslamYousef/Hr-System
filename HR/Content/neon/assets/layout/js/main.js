/*global $*/
/*global console*/
$(function () {
    'use strict';
    var $body = $('body');
    $body.on('click', function (event) {
        var clickedOutside = $(event.target).closest('.main-menu').length == 0;
        var clickedOutside2 = $(event.target).closest('.custom-navbar').length == 0;
        if (clickedOutside && clickedOutside2) {
            $('.main-menu').addClass('small');
            $('.custom-navbar').hide();
            $('.main-menu span.li span').hide();
            $('.main-menu').animate({ width: '50px' });
            console.log("WD");
        } else {
            console.log("ZZZ");
        }
    })

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
        if ($('.item').length > 14 && $('.item').length < 30) {
            $('.custom-navbar').css('columnCount', '2');
            $('.custom-navbar').animate({ columnWidth: '260px' });
        }
        else if ($('.item').length > 29 && $('.item').length < 45) {
            $('.custom-navbar').css('columnCount', '3');
            $('.custom-navbar').animate({ columnWidth: '260px' });
        }
        else if ($('.item').length > 44 && $('.item').length < 60) {
            $('.custom-navbar').css('columnCount', '4');
            $('.custom-navbar').animate({ columnWidth: '260px' });
        }
        else if ($('.item').length > 59 && $('.item').length < 74) {
            $('.custom-navbar').css('columnCount', '5');
            $('.custom-navbar').animate({ columnWidth: '260px' });
        } else if ($('.item').length > 0 && $('.item').length < 15) {
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
        if ($('.item').length > 14 && $('.item').length < 30) {
            $('.custom-navbar').css('columnCount', '2');
            $('.custom-navbar').animate({ columnWidth: '260px' });
        }
        else if ($('.item').length > 29 && $('.item').length < 45) {
            $('.custom-navbar').css('columnCount', '3');
            $('.custom-navbar').animate({ columnWidth: '260px' });
        }
        else if ($('.item').length > 44 && $('.item').length < 60) {
            $('.custom-navbar').css('columnCount', '4');
            $('.custom-navbar').animate({ columnWidth: '260px' });
        }
        else if ($('.item').length > 59 && $('.item').length < 74) {
            $('.custom-navbar').css('columnCount', '5');
            $('.custom-navbar').animate({ columnWidth: '260px' });
        } else if ($('.item').length > 0 && $('.item').length < 15) {
            $('.custom-navbar').css('columnCount', '1');
            $('.custom-navbar').animate({ columnWidth: '260px' });
        }
    });

});