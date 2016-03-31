

//#region Namespaces
if (typeof(Netwise) === 'undefined')
{
    Netwise = {};
}

if (typeof(Netwise.Common) === 'undefined') {
    Netwise.Common = {};
}

if (typeof(Netwise.Common.Const) === 'undefined') {
    Netwise.Common.Const = {};
}

if (typeof(Netwise.Common.Form) === 'undefined') {
    Netwise.Common.Form = {};
}

if (typeof(Netwise.Common.Navigation) === 'undefined') {
    Netwise.Common.Navigation = {};
}

if (typeof(Netwise.Common.Conversion) === 'undefined') {
    Netwise.Common.Conversion = {};
}

if (typeof(Netwise.Common.Validation) === 'undefined') {
    Netwise.Common.Validation = {};
}

if (typeof(Netwise.Common.Utils) === 'undefined') {
    Netwise.Common.Utils = {};
}

if (typeof(Netwise.Common.WebApi) === 'undefined') {
    Netwise.Common.WebApi = {};
}
//#endregion

Netwise.Common.Const = {

    FormStates: {
        FORM_TYPE_CREATE: 1,
        FORM_TYPE_UPDATE: 2,
        FORM_TYPE_READ_ONLY: 3,
        FORM_TYPE_DISABLED: 4,
        FORM_TYPE_QUICK_CREATE: 5,
        FORM_TYPE_BULK_EDIT: 6
    },

    FieldRequirements: {
        NONE: "none",
        REQUIRED: "required",
        RECOMMENDED: "recommended"
    },

    NotificationTypes: {
        WARINING: "WARNING",       
        ERROR: "ERROR", 
        INFO: "INFO"
    }

};

Netwise.Common.Form = {

    setValue: function (attributeName, value) {
        "use strict";
        var attribute = Xrm.Page.data.entity.attributes.get(attributeName);
        if (attribute !== null) {
            attribute.setValue(value);
            attribute.setSubmitMode("always");
        }
    },

    setValueNotForced: function (attributeName, value) {
        "use strict";
        var attribute = Xrm.Page.data.entity.attributes.get(attributeName);
        if (attribute !== null) {
            attribute.setValue(value);
        }
    },

    setFocus: function (attributeName) {
        "use strict";
        var attribute = Xrm.Page.getControl(attributeName);
        if (attribute !== null) {
            attribute.setFocus(true);
        }
    },

    getValue: function (attributeName) {
        "use strict";
        var attribute = Xrm.Page.data.entity.attributes.get(attributeName);
        if (attribute !== null) {
            return attribute.getValue();
        }
        else {
            return null;
        }

    },

    disableControl: function (controlName) {
        "use strict";
        var control = Xrm.Page.ui.controls.get(controlName);
        if (control !== null) {
            control.setDisabled(true);
        }
    },

    enableControl: function (controlName) {
        "use strict";
        var control = Xrm.Page.ui.controls.get(controlName);
        if (control !== null) {
            control.setDisabled(false);
        }
    },

    forceSubmit: function (attributeName, force) {
        "use strict";
        var attribute = Xrm.Page.data.entity.attributes.get(attributeName);
        attribute.setSubmitMode(force);
    },

    showHideSection: function (tabName, sectionName, isVisible) {
        "use strict";
        var tab = Xrm.Page.ui.tabs.get(tabName);
        if (tab != null) {
            var section = tab.sections.get(sectionName);
            if (section !== null) {
                section.setVisible(isVisible);
            }
        }
    },

    getOption: function (options, number) {
        "use strict";
        var i;
        var o;
        for (i = 0; i < options.length; i++) {
            o = options[i];
            if (o.value === number) {
                return o;
            }
        }
    },

    setCurrentDate: function (attributeName) {
        "use strict";
        var date = new Date();
        Netwise.Common.Form.SetValueNotForced(attributeName, date);
    },

    setRequiredLevel: function (attributeName, reqlevel) {
        "use strict";
        var attribute = Xrm.Page.data.entity.attributes.get(attributeName);
        if (attribute != null) {
            attribute.setRequiredLevel(reqlevel);
        }
    },

    getSelectedOption: function (attributeName) {
        "use strict";
        return Xrm.Page.data.entity.attributes.get(attributeName).getSelectedOption();
    },

    hideControl: function (controlName) {
        "use strict";
        var control = Xrm.Page.ui.controls.get(controlName);
        if (control !== null) {
            control.setVisible(false);
        }
    },

    showControl: function (controlName) {
        "use strict";
        var control = Xrm.Page.ui.controls.get(controlName);
        if (control !== null) {
            control.setVisible(true);
        }
    },

    hideTab: function (tabName) {
        "use strict";
        var tab = Xrm.Page.ui.tabs.get(tabName);
        if (tab !== null) {
            tab.setVisible(false);
        }
    },

    showTab: function ShowTab(tabName) {
        "use strict";
        var tab = Xrm.Page.ui.tabs.get(tabName);
        if (tab !== null) {
            tab.setVisible(true);
        }
    },

    getIsDirty: function (attributeName) {
        "use strict";
        return Xrm.Page.data.entity.attributes.get(attributeName).getIsDirty();
    }
};

Netwise.Common.Navigation = {

    hideNavigation: function (name) {
        var nav = Xrm.Page.ui.navigation.items.get(name);
        if (nav != null) {
            nav.setVisible(false);
        }
    },

    showNavigation: function (name) {
        var nav = Xrm.Page.ui.navigation.items.get(name);
        if (nav != null) {
            nav.setVisible(true);
        }
    },
};

Netwise.Common.Conversion = {

    createLookup: function (id, logicalName, display) {
        return [{ id: id, entityType: logicalName, name: display }];
    },

    createLookupFromEntityReference: function (attribute) {
        if (attribute == null || attribute.Id == null) return null;
        return this.CreateLookup(attribute.Id, attribute.LogicalName, attribute.Name);
    },

    createMoney: function (value) {
        return { Value: value };
    },

    createOptionSet: function (value) {
        return { Value: value };
    },

    createDateTimeFromJSONAttribute: function (attribute) {
        if (attribute == null) return null;
        return new Date(parseInt(attribute.replace("/Date(", "").replace(")/", ""), 10));
    },

    createEntityReference: function (id, logicalName, name) {
        if (id == null) return null;
        return { Id: id, LogicalName: logicalName, Name: name };
    },

    createEntityReferenceFromLookup: function (value) {
        if (value == null || value[0] == null) return null;
        return this.CreateJSONEntityReference(value[0].id, value[0].entityType, value[0].name);
    },
};

Netwise.Common.Validation = {

    isNipValid: function (inputVal) {
        if (inputVal == null) return true;
        if (!inputVal.match(/^[0-9]{10}$/))
            return false;
        var my_nums = inputVal.replace(/-/g, '');
        var valid_nums = "657234567";
        var sum = 0;
        for (var temp = 8; temp >= 0; temp--)
            sum += (parseInt(valid_nums.charAt(temp)) * parseInt(my_nums.charAt(temp)));
        if ((sum % 11) == 10 ? false : ((sum % 11) == parseInt(my_nums.charAt(9))))
            return true;
        else
            return false;
    },

    isRegonValid: function (inputVal) {
        if (inputVal == null) return true;
        if (!inputVal.match(/^[0-9]{7}$/) && !inputVal.match(/^[0-9]{9}$/) && !inputVal.match(/^[0-9]{14}$/))
            return false;

        return true;
    },

    isPeselValid: function (value) {
        var pesel = value.replace(/[\ \-]/gi, '');
        if (pesel.length != 11) { return false; } else {
            var steps = new Array(1, 3, 7, 9, 1, 3, 7, 9, 1, 3);
            var sum_nb = 0;
            for (var x = 0; x < 10; x++) { sum_nb += steps[x] * pesel.charAt(x); }
            sum_m = 10 - sum_nb % 10;
            if (sum_m == 10) { sum_c = 0; } else { sum_c = sum_m; }
            if (sum_c != pesel.charAt(10)) { return false; }
        }
        return true;
    },

    isEmailValid: function (inputval) {

        var _r = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

        if (inputval == null || inputval == "") return true;

        if (!inputval.match(_r))
            return false;
        return true;
    },

    isPhoneNumberValid: function (inputVal) {
        if (!inputVal.match(/^\+[0-9]{11}$/)) {
            return false;
        }
        return true;
    }
};

Netwise.Common.Utils = {

    toUpper: function (attributeName) {
        var value = Netwise.Common.Form.GetValue(attributeName);
        if (value !== null && value !== "") {
            Netwise.Common.Form.SetValueNotForced(attributeName, value.toString().toUpperCase());
        }
    },

    guidsAreEqual: function (guid1, guid2) {
        var isEqual = false;
        if (guid1 === null || guid2 == null) {
            isEqual = false;
        }
        else {
            isEqual = guid1.replace(/[{}]/g, "").toLowerCase() === guid2.replace(/[{}]/g, "").toLowerCase();
        }
        return isEqual;
    },

    getLookupText: function (attributename) {
        var attribute = Xrm.Page.data.entity.attributes.get(attributename);
        if (attribute.getValue()[0] !== null)
            return attribute.getValue()[0].name;
        return null;
    },

    isNullOrEmpty: function (text) {
        return text === null || text === ""; 
    }

};

Netwise.Common.WebApi = {

    getAllRecords: function (entityName, onSuccess, onError) {

        var local = Netwise.Common.WebApi.Local;
        var requestedUri = local.getODataPath() + entityName + 's';
        var request = local.prepareGetRequest(requestedUri);

        local.sendGetRequest(request, onSuccess, onError);
    },

    getRecordById: function (entityName, id, onSuccess, onError) {

        var local = Netwise.Common.WebApi.Local;
        var requestedUri = local.getODataPath() + entityName + 's(' + id + ')';
        var request = local.prepareGetRequest(requestedUri);

        local.sendGetRequest(request, onSuccess, onError);
    },

    getRecordByFilter: function (entityName, filter, onSuccess, onError) {

        var local = Netwise.Common.WebApi.Local;
        var requestedUri = local.getODataPath() + entityName + 's?$filter=' + filter;
        var request = local.prepareGetRequest(requestedUri);

        local.sendGetRequest(request, onSuccess, onError);
    },

    createRecord: function (entityName, properties, onSuccess, onError) {

        var local = Netwise.Common.WebApi.Local;
        var requestedUri = local.getODataPath() + entityName + 's';
        var request = local.preparePostRequest(requestedUri);

        local.sendPostRequest(reqest, properties, 204, onSuccess, onError); 

    },

    updateRecord: function (entityName, id, properties, onSuccess, onError) {

        var local = Netwise.Common.WebApi.Local;
        var requestedUri = local.getODataPath() + entityName + 's(' + id + ')';
        var request = local.preparePostRequest(requestedUri);

        local.sendPostRequest(reqest, properties, 204, onSuccess, onError);
    },

    upsertRecord: function (entityName, id) {

        var local = Netwise.Common.WebApi.Local;
        var requestedUri = local.getODataPath() + entityName + 's(' + id + ')';
        var request = new XMLHttpRequest();

        request.open("PATCH", encodeURI(requestedUri), true);

        request.setRequestHeader("Accept", "application/json");
        request.setRequestHeader("Content-Type", "application/json; charset=utf-8");
        request.setRequestHeader("OData-MaxVersion", "4.0");
        request.setRequestHeader("OData-Version", "4.0");

        local.sendPatchRequest(reqest, properties, onSuccess, onError);
  
    },

    deleteRecord: function (entityName, id, onSuccess, onError) {

        var local = Netwise.Common.WebApi.Local;
        var requestedUri = local.getODataPath() + entityName + 's';

        var request = new XMLHttpRequest();
        request.open("DELETE", encodeURI(requestedUri), true);
        request.setRequestHeader("Accept", "application/json");
        request.setRequestHeader("Content-Type", "application/json; charset=utf-8");
        request.setRequestHeader("OData-MaxVersion", "4.0");
        request.setRequestHeader("OData-Version", "4.0");

        request.onreadystatechange = function () {

            if (this.readyState == 4 /* complete */) {

                request.onreadystatechange = null;

                if (this.status == 204) {
                    onSuccess(JSON.parse(this.response));;

                }
                else {
                    onError(JSON.parse(this.response));
                }
            }
        };

        request.send(JSON.stringify(properties));
    },

    executeAction: function (actionName, actionBody, onSuccess, onError) {

        var local = Netwise.Common.WebApi.Local;
        var requestedUri = local.getODataPath() + actionName;
        var request = local.preparePostRequest(requestedUri);

        local.sendPostRequest(reqest, actionBody, onSuccess, onError);
    },

    executeBoundedAction: function (actionName, actionBody, onSuccess, onError) {

        var local = Netwise.Common.WebApi.Local;
        var requestedUri = local.getODataPath() + entityName + 's(' + id + ')/' + actionName;
        var request = local.prepareGetRequest(requestedUri);

        local.sendPostRequest(reqest, actionBody, 200, onSuccess, onError);
    },

    executeFunction: function (functionWithAttributes, onSuccess, onError) {

        var local = Netwise.Common.WebApi.Local;
        var requestedUri = local.getODataPath() + functionWithAttributes;
        var request = local.prepareGetRequest(requestedUri);

        local.sendGetRequest(request, onSuccess, onError);
    },

    executeBoundedFunction: function (entityName, id, functionWithAttributes, onSuccess, onError) {

        var local = Netwise.Common.WebApi.Local;
        var requestedUri = local.getODataPath() + entityName + 's(' + id + ')/' + functionWithAttributes;
        var request = local.prepareGetRequest(requestedUri);

        local.sendGetRequest(request, onSuccess, onError);
    },

    Local: {

        getContext: function () {

            if (typeof GetGlobalContext !== "undefined")
            {
                return GetGlobalContext();
            }
            else {
                if (typeof Xrm != "undefined") {
                    return Xrm.Page.context;
                }
                else { throw new Error("Context is not available."); }
            }
        },

        getODataPath: function () {

            var WEB_API_POSTFIX = '/api/data/v8.0/';
            var clientUrl = Netwise.Common.WebApi.Local.getContext().getClientUrl();

            return clientUrl + WEB_API_POSTFIX;
        },

        prepareGetRequest: function (requestedUri) {

            var request = new XMLHttpRequest();
            request.open('GET', encodeURI(requestedUri), true);

            request.setRequestHeader('Content-Type', 'application/json');
            request.setRequestHeader('OData-MaxVersion', '4.0');
            request.setRequestHeader('Accept', 'application/json');
            request.setRequestHeader('Prefer', 'odata.include-annotations="*"');

            return request;
        },

        preparePostRequest: function (requestedUri) {

            var request = new XMLHttpRequest();
            request.open("POST", encodeURI(requestedUri), true);

            request.setRequestHeader("Accept", "application/json");
            request.setRequestHeader("Content-Type", "application/json; charset=utf-8");
            request.setRequestHeader("OData-MaxVersion", "4.0");
            request.setRequestHeader("OData-Version", "4.0");

            return request;
        },

        sendGetRequest: function (request, onSuccess, onError) {

            request.onreadystatechange = function () {

                if (this.readyState == 4 /* complete */) {

                    request.onreadystatechange = null;

                    var response = this.response;

                    if (this.status == 200) {
                        onSuccess(JSON.parse(this.response));
                    }
                    else {
                        onError(JSON.parse(this.response));
                    }
                }
            };
            request.send();
        }, 

        sendPostRequest: function(request, properties, successCode, onSuccess, onError){

            request.onreadystatechange = function () {

                if (this.readyState == 4 /* complete */) {

                    request.onreadystatechange = null;

                    if (this.status == successCode) {
                        onSuccess(JSON.parse(this.response));;
      
                    }
                    else {
                        onError(JSON.parse(this.response));
                    }
                }
            };

            request.send(JSON.stringify(properties));
        }, 

        sendPatchRequest: function (request, properties, onSuccess, onError) {

            request.onreadystatechange = function () {

                if (this.readyState == 4 /* complete */) {

                    request.onreadystatechange = null;

                    if (this.status == 204) {
                        onSuccess(JSON.parse(this.response));;

                    }
                    else {
                        onError(JSON.parse(this.response));
                    }
                }
            };

            request.send(JSON.stringify(properties));
        }

    }
};