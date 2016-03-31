//#region Namespaces
if (typeof (Netwise) === 'undefined') {
    Netwise = {};
}

if (typeof (Netwise.Validation) === 'undefined') {
    Netwise.Validation = {};
}

Netwise.Validation = {

    IsNipValid: function(nip){

        //Check length
        if (nip == null)
            return false;

        //nip = nip.replace(/\-/g, '');

        if (nip.length != 10)
            return false;

        //Check digits
        for (i = 0; i < 10; i++) {

            var num = nip.charAt(i);
            if (isNaN(num)) {
                return false;
            }
        }

        //Check checkdigit
        sum = 6 * nip.charAt(0) +
        5 * nip.charAt(1) +
        7 * nip.charAt(2) +
        2 * nip.charAt(3) +
        3 * nip.charAt(4) +
        4 * nip.charAt(5) +
        5 * nip.charAt(6) +
        6 * nip.charAt(7) +
        7 * nip.charAt(8);

        sum %= 11;

        if (nip.charAt(9) != sum)
            return false;

        return true;
    }
}