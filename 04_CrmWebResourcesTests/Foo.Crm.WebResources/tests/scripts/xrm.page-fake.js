var Xrm = {
    Page: {
        ui: {
            currentNotification: null, 
            clearFormNotification: function () { this.currentNotification = null },
            setFormNotification: function (text) { this.currentNotification = text }
        }
    }
};
