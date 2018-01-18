mod.factory("schoolMasterFactory", function ($http) {

    var PaymentVoucherApiUrl = ApiDomain + "/api/SchoolMasterApi";

    var api = {
        //GetPaymentVoucherDetailList: function (model) {
        //    return $http.get(ReceiptVoucherApiUrl + "/GetPaymentVoucherDetail" + model);
        //},
        //GetPaymentVoucherDetail: function (id) {
        //    return $http.get(PaymentVoucherApiUrl + "/" + id);
        //},
        //SavePaymentVoucherDetail: function (model) {
        //    return $http.post(PaymentVoucherApiUrl, model)
        //},
        //DeletePaymentVoucherDetail: function (PaymentId) {
        //    return $http.delete(PaymentVoucherApiUrl + "/" + PaymentId)
        //},
        //SearchPaymentVoucherDetail: function (model) {
        //    return $http.post(PaymentVoucherApiUrl + "/SearchPaymentVoucherDetail", model)
        //},
        //FindSubLedgerAmountNaration: function (PaymentId) {
        //    return $http.post(PaymentVoucherApiUrl + "/FindSubLedgerAmountNaration/" + PaymentId)
        //},
        //GetMaxPaymentVoucherNumber: function () {
        //    return $http.get(PaymentVoucherApiUrl + "/GetMaxPaymentVoucherNumber")
        //},
        //GetPaymentVoucherPrint: function (PaymentId) {
        //    return $http.get(PaymentVoucherApiUrl + "/GetPaymentVoucherPrint/" + PaymentId)
        //},

    };

    return api;

});