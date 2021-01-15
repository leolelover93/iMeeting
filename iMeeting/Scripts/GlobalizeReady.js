//if (!Modernizr.inputtypes.date) {
    $(function () {
        $.validator.methods.number = function (value, element) {
            return this.optional(element) || !isNaN(Globalize.parseFloat(value));
        };

        $.validator.methods.date = function (value, element) {
            return this.optional(element) || Globalize.parseDate(value);
        };

        if (Culture == 'fr')
            Globalize.culture('fr-FR');
    });
//}