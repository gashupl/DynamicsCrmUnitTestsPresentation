/// <reference path="../../foo_/jscripts/entities/foo_account.js" />
/// <reference path="sinon-1.17.3.js" />
/// <reference path="sinon-ie-1.17.3.js" />
/// <reference path="sinon-qunit-1.0.0.js" />
/// <reference path="xrm.page-fake.js" />

QUnit.test("foo_account.js : Netwise.Crm.Account.Form.Main.onSave : Valid NIP", function (assert) {

    var execObj = {
        getEventArgs: function () {
            return {
                saveBlocked: false, 
                preventDefault: function () {
                    this.saveBlocked = true; 
                }
            }
        }
    };

    var sandbox = sinon.sandbox.create();

    sandbox.stub(Netwise.Common.Form, "getValue").returns("1132545872");
    Netwise.Crm.Account.Form.Main.onSave(execObj);
    assert.ok(execObj.getEventArgs().saveBlocked  == false, "Passed!");

    sandbox.restore();
});

QUnit.test("foo_account.js : Netwise.Crm.Account.Form.Main.onSave : Invalid NIP", function (assert) {

    var execObj = {
        getEventArgs: function () {
            return {
                saveBlocked: false,
                preventDefault: function () {
                    this.saveBlocked = true;
                }
            }
        }
    };

    var sandbox = sinon.sandbox.create();

    sandbox.stub(Netwise.Common.Form, "getValue").returns("1132545873");
    Netwise.Crm.Account.Form.Main.onSave(execObj);
    assert.ok(execObj.getEventArgs().saveBlocked == false, "Passed!");

    sandbox.restore();
});

QUnit.test("foo_account.js : Netwise.Crm.Account.Form.Main.onFooCategoryChanged : Account found", function (assert) {

    var accountId = "99CB6F89-85E4-4206-817B-FED35FE8EB23"; 

    var sandbox = sinon.sandbox.create();

    sandbox.stub(Netwise.Common.Form, "getValue").returns(accountId);
    sandbox.stub(Netwise.Common.Form, "setValue").returns("");
    sandbox.stub(Netwise.Common.WebApi, "getRecordById");

    Netwise.Crm.Account.Form.Main.onFooCategoryChanged();
    Netwise.Crm.Account.Form.Common.onGetAccountSuccess({ foo_code: '1234' });
    assert.ok(Xrm.Page.ui.currentNotification === null, "Passed!");

    sandbox.restore();
});

QUnit.test("foo_account.js : Netwise.Crm.Account.Form.Main.onFooCategoryChanged : Account not found", function (assert) {

    var sandbox = sinon.sandbox.create();

    sandbox.stub(Netwise.Common.Form, "getValue").returns("99CB6F89-85E4-4206-817B-FED35FE8EB23");
    sandbox.stub(Netwise.Common.Form, "setValue").returns("");
    sandbox.stub(Netwise.Common.WebApi, "getRecordById");

    Netwise.Crm.Account.Form.Main.onFooCategoryChanged();
    Netwise.Crm.Account.Form.Common.onGetAccountError({ Error: 'Error' });

    assert.ok(Xrm.Page.ui.currentNotification !== null, "Passed!");

    sandbox.restore();
});