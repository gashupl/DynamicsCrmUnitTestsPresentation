
/// <reference path="../common/ntw_common2016.js" />
/// <reference path="../common/foo_validators.js" />

if (typeof (Netwise) == "undefined") {
    Netwise = {};
}

if (typeof (Netwise.Crm) == "undefined") {
    Netwise.Crm = {};
}

if (typeof (Netwise.Crm.Account) == "undefined") {
    Netwise.Crm.Account = {};
};

if (typeof (Netwise.Crm.Account.Form) == "undefined") {
    Netwise.Crm.Account.Form = {};
};

if (typeof (Netwise.Crm.Account.Ribbon) == "undefined") {
    Netwise.Crm.Account.Ribbon = {};
};

Netwise.Crm.Account = {

    EntityLogicalName: "Account",

    Fields: {
        AccountId: "AccountId", 
        foo_nip: "foo_nip",
        foo_categorycode: "foo_categorycode"
    },
}

Netwise.Crm.Account.Form = {

    Messages: {
        InvalidNip: "Niepoprawny numer NIP", 
        GetCategoryCodeError: "Nie można pobrać kodu dla wybranej kategorii"
    },

    NotificationCodes: {
        IncorrectNip: "IncorrectNipNotification", 
        GetCategoryCodeError: "getAccountErrorNotification"
    }, 

    Main: {

        onSave: function (execObj) {

            var nip = Netwise.Common.Form.getValue(Netwise.Crm.Account.Fields.foo_nip);

            if (Netwise.Validation.IsNipValid(nip) === false) {
                Xrm.Page.ui.setFormNotification(Netwise.Crm.Account.Form.Messages.InvalidNip, Netwise.Common.Const.NotificationTypes.ERROR, Netwise.Crm.Account.Form.NotificationCodes.incorrectNip);
                execObj.getEventArgs().preventDefault(); 

            }
            else {
                Xrm.Page.ui.clearFormNotification(Netwise.Crm.Account.Form.NotificationCodes.IncorrectNip);
            }
        },

        onFooCategoryChanged: function () {

            var accountId = Netwise.Common.Form.getValue(Netwise.Crm.Account.Fields.AccountId);

            Netwise.Common.WebApi.getRecordById
                (Netwise.Crm.Account.EntityLogicalName, accountId, Netwise.Crm.Account.Form.Common.onGetAccountSuccess, Netwise.Crm.Account.Form.Common.onGetAccountError);
        
        },
    },

    Common: {

        onGetAccountSuccess: function (data) {
            Xrm.Page.ui.clearFormNotification(Netwise.Crm.Account.Form.NotificationCodes.GetCategoryCodeError);
            Netwise.Common.Form.setValue(Netwise.Crm.Account.foo_categorycode, data.foo_code); 
        }, 

        onGetAccountError: function (err) {
            if (err.Error != '') {
                Xrm.Page.ui.setFormNotification(err.Error, Netwise.Common.Const.NotificationTypes.WARINING, Netwise.Crm.Account.Form.NotificationCodes.GetCategoryCodeError);
            }
        }
    }
}

Netwise.Crm.Account.Ribbon = {

}