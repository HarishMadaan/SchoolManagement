mod.controller("schoolController", function ($scope, $timeout, toaster, schoolMasterFactory) {
    $('#loader').hide();
    $scope.selectedView = "Payment Info";

    $scope.PaymentVoucherModel = {};
    $scope.ParticularPaymentObject = {};

    $scope.ShowPaymentVoucherListingGrid = true;
    $scope.spnPaggingGrid = true;
    $scope.dvcheckBox = true;
    $scope.subLedgerValue = {};
    $scope.IsEditMode = false;
    $scope.AddSubLedger = true;

});