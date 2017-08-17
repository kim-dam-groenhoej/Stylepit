"use strict";

$(document).ready(function () {
    $("#prodCreate").bootstrapValidator();
    $("#checkout").bootstrapValidator();
});
// AJAX POST TIL MANDAG
$("#signup-newsletter").on("submit", function (event) {
    var $this = $(this);
    var values = $this.serialize();
    var formInputs = $('#signup-newsletter input[type=text]');

    if (formInputs.val() == "") {
        $("#info-box").addClass("alert-danger");
        $("#info-box").removeClass("alert-success");
        $("#info-box").text("Der gik noget galt");
        $("#info-box").css("display", "block");
    } else {
        $.ajax({
            type: $this.attr('method'),
            url: $this.attr('action'),
            data: values
        }).done(function () {
            $("#info-box").addClass("alert-success");
            $("#info-box").removeClass("alert-danger");
            $("#info-box").text("Tak for din tilmelding");
            $("#info-box").css("display", "block");

            $this.trigger("reset");
        }).fail(function () {
            $("#info-box").addClass("alert-danger");
            $("#info-box").removeClass("alert-success");
            $("#info-box").text("Der gik noget galt");
            $("#info-box").css("display", "block");

            console.log("ajax fail");
        });
    }

    event.preventDefault();
});

$("#addToCart").on("submit", function (event) {
    var $this = $(this);
    var values = $this.serialize();

    $.ajax({
        type: $this.attr('method'),
        url: $this.attr('action'),
        data: values
    }).done(function () {
        $("#cart-container").load("/Shop/GetCart");
    }), event.preventDefault();
});

$("#deleteFromCart").on("submit", function (event) {
    var $this = $(this);
    var values = $this.serialize();

    $.ajax({
        type: $this.attr('method'),
        url: $this.attr('action'),
        data: values
    }).done(function () {
        $("#testloading").load("/Shop/ShowCart");
    }), event.preventDefault();
});

$(document).ready(function () {
    $(".owl-carousel").owlCarousel({
        items: 4,
        autoplay: true
    });
});
$(".breadcrumb li").each(function () {
    $(this).addClass("breadcrumb-item");
});

