//if (!Modernizr.inputtypes.date) {
    $(function () {
        $(".datefield").datepicker($.datepicker.regional[Culture]); // Variable Culture définie dans _Layout.vbhtml
    });
//}