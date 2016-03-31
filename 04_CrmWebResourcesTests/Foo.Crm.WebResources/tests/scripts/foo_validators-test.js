/// <reference path="../../foo_/jscripts/common/foo_validators.js" />

QUnit.test("foo_validators / Netwise.Validation.NipIsValid / Valid NIP ", function (assert) {
    var validNip = "1132545872"; 
    var isValid = Netwise.Validation.IsNipValid(validNip);
    assert.ok(isValid === true, "Passed!");
});

QUnit.test("foo_validators / Netwise.Validation.NipIsValid / Invalid NIP ", function (assert) {
    var invalidNip = "1132545870"
    var isValid = Netwise.Validation.IsNipValid(invalidNip);
    assert.ok(isValid === false, "Passed!");
});