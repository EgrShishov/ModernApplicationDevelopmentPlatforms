$(document).ready(function () {
    $(document).on('click', '.page-link', function (e) {
        e.preventDefault();

        var url = $(this).data('ajax-url');
        console.log(url);
        $('#catalog').load(url);
    })
})