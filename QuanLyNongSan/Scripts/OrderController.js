var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#btnContinue').off('click').on('click', function () {
            window.location.href = "/";
        });
        $('#btnPayment').off('click').on('click', function () {
            window.location.href = "/Order/Payment";
        });
        $('#btnUpdate').off('click').on('click', function () {
            var listProduct = $('.txtQuantity');
            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),
                    Product: {
                        ID: $(item).data('id')
                    }
                });
            });

            $.ajax({
                url: '/Order/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Order/Index";
                    }
                }
            })
        });
     

        $('#btnDeleteAll').off('click').on('click', function () {


            $.ajax({
                url: '/Order/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Order/Index";
                    }
                }
            })
        });

        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                data: { id: $(this).data('id') },
                url: '/Order/Delete',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Order/Index";
                    }
                }
            })
        });
    }
}
cart.init();

jQuery(document).ready(function ($) {
    $(".scroll").click(function (event) {
        event.preventDefault();
        $('html,body').animate({ scrollTop: $(this.hash).offset().top }, 1000);
    });
});

jQuery(document).ready(function ($) {
    $(".switcher-btn").click(function () {
        $(".switcher-wrapper").toggleClass("switcher-toggled")
    });
    $("#page").click(function () {
        $(".switcher-wrapper").removeClass("switcher-toggled")
    });
    $('option').mousedown(function (e) {
        e.preventDefault();
        $(this).toggleClass('selected');

        $(this).prop('selected', !$(this).prop('selected'));
        return false;
    });
$('.giohangprice').click(function () {
    var product_id = $(this).closest('.formsoluong').find('.idsp').val();
    var soluong = $(this).closest('.formsoluong').find('.soluongsp').val();

    $.ajax({
        url: '/Order/SuaSoLuong?ID=' + product_id + '&soluongmoi=' + soluong,
        data: { ID: product_id, soluongmoi: soluong },
        success: function (data) {
            $('body').load(location.href);
        },
        error: function (data) {
            alert('Sản phẫm lỗi');
        }
    });
});
});