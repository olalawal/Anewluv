(function ($) {
    jQuery.validator.addMethod("notequalto", function (value, element, params) {
        if (this.optional(element)) return true;
        
        var props = params.split(',');

        for (var i in props) {
            if ($('#' + props[i]).val() == value) return false;
        }

        return true;
    });

    jQuery.validator.unobtrusive.adapters.addSingleVal("notequalto", "otherproperties");

    jQuery.validator.addMethod("requiredif", function (value, element, params) {
        if ($(element).val() != '') return true
        var $other = $('#' + params.other);
        //var otherVal =  $other.val();
        return params.comp == 'isequalto' ? (otherVal != params.value) : (otherVal == params.value);
    });

    jQuery.validator.unobtrusive.adapters.add("requiredif", ["other", "comp", "value"], function (options) {
        options.rules['requiredif'] = {
            other: options.params.other,
            comp: options.params.comp,
            value: options.params.value
        };
        options.messages['requiredif'] = options.message;

//        var $other = $('#' + options.params.other);
//        $other.bind("focusout, keypress, click", function () {
//            $('#PromotionalCode').valid();
//        });
    });

    jQuery.validator.addMethod("requiredor", function (value, element, params) {
        if ($(element).val() == '') {
            var $other = $('#' + params);
            return $other.val() != '';
        }
        return true;
    });

    jQuery.validator.unobtrusive.adapters.addSingleVal("requiredor", "otherproperty");
} (jQuery));