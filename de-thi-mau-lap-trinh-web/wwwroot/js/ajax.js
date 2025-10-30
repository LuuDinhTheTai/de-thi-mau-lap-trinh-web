$(function () {
    // Lấy URL được inject từ layout hoặc dùng fallback
    var filterUrl = window.appUrls && window.appUrls.filterProducts ? window.appUrls.filterProducts : '/Home/FilterProducts';

    // Event delegation (an toàn nếu link được render sau)
    $(document).on('click', '.category-link', function (e) {
        e.preventDefault();
        var maLoai = $(this).data('maloai');

        $.ajax({
            url: filterUrl,
            type: 'GET',
            data: { maLoai: maLoai },
            success: function (result) {
                $('#product-list-container').html(result);
            },
            error: function (xhr, status, err) {
                console.error('AJAX error:', status, err, xhr.responseText);
                alert('Không thể tải dữ liệu sản phẩm.');
            }
        });
    });
});