///<reference path="angular.js" />

var bankDetailApp = angular.module('bankDetailApp', ['LocalStorageModule', 'AuthApp']);

bankDetailApp.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

bankDetailApp.controller('bankDetailController', ['$scope', 'bankDetailService', function ($scope, bankDetailService) {


    Clear();
    function Clear() {

        $scope.BankDetail = {};
        $scope.btnSave = 'Add Bank Detail';
        $scope.BankDetail.BankId = 0;
        $scope.BankDetail.CustomerId = 0;
        $scope.BankDetail.CompanyId = 0;
        $scope.BankDetailList = [];
        $scope.BankDetailTempList = [];
        GetAllBankDetails();
        LoadBankDetail();
        GetAllCustomers();
    }

    function LoadBankDetail() {
        var bankDetail = sessionStorage.getItem("BankDetail");
        if (bankDetail != null) {
            $scope.btnSave = 'Update Bank Detail';
            $scope.BankDetail = JSON.parse(sessionStorage.BankDetail);
        }
        sessionStorage.removeItem("BankDetail");
    }

    function GetAllBankDetails() {
        bankDetailService.GetAllBankDetails().then(function (response) {
            $scope.BankDetailList = response.data;
        }, function () {
            alert("Failed to get Bank Detail!");
        })
    }
    function GetAllCustomers() {
        bankDetailService.GetAllCustomers().then(function (response) {
            $scope.CustomerList = response.data;
        }, function () {
            alert("Failed to get customer!");
        })
    }
    function ResetObject() {
        $scope.BankDetail = {};
        $scope.BankDetail.BankId = 0;
    }
    $scope.SetCustomerAccount = function () {
        $scope.BankDetail.AccountFor = "Customer";
    }
    $scope.SetCompanyAccount = function () {
        $scope.BankDetail.AccountFor = "Company";
    }
    $scope.AddBankDetail = function (bankDetail) {
        if ($scope.BankDetail.BankId == 0) {
            $scope.BankDetail.Status = true;
            $scope.BankDetailTempList.push(bankDetail)
            ResetObject();
        }
        else {
            $scope.BankDetailTempList.push(bankDetail);
            $scope.btnSave = 'Add Bank Detail';
            ResetObject();
        }
    }
    $scope.AddNew = function () {
        window.location.href = "/BankDetails/Create";
    }
    $scope.SaveBankDetail = function () {
        var bankDetailCount = $scope.BankDetailTempList.length;
        angular.forEach($scope.BankDetailTempList, function (data) {
            if (data.BankId == 0) {
                bankDetailService.SaveBankDetail(data).then(function (response) {
                    bankDetailCount--;
                    if (bankDetailCount == 0) {
                        alert("Bank Detail Saved Successfully");
                        window.location.href = "/BankDetails/Index";
                    }

                }, function () {
                    alert("Error occured. Please try again.");

                });
            }
            else {
                bankDetailService.UpdateBankDetail(data).then(function (response) {
                    bankDetailCount--;
                    if (bankDetailCount == 0) {
                        alert("Bank Detail Update Successfully");
                        window.location.href = "/BankDetails/Index";
                    }

                }, function () {
                    alert("Error occured. Please try again.");

                });
            }

        });


    }
    $scope.Delete = function (bankDetail) {
        var r = confirm("Are you sure you want to delete?");
        if (r == true) {
            bankDetailService.DeleteBankDetail(bankDetail).then(function (response) {
                alert("Bank Detail Deleted Successfully");
                window.location.reload();
            }, function () {
                alert("Error occured. Please try again.");

            });
        } else {
            return;
        }
    }
    $scope.Edit = function (bankDetail) {
        sessionStorage.setItem("BankDetail", JSON.stringify(bankDetail));
        window.location.href = "/BankDetails/Create"
    }
    $scope.ResetObject = function () {
        ResetObject();
    }
    $scope.RedirectToList = function () {
        window.location.href = "/BankDetails/Index";
    }
    $scope.Remove = function (bankDetail) {
        var index = $scope.BankDetailTempList.indexOf(bankDetail);
        $scope.BankDetailTempList.splice(index, 1);
    }


}])


bankDetailApp.factory('bankDetailService', ['$http', function ($http) {

    var bankDetailAppFactory = {};

    bankDetailAppFactory.GetAllBankDetails = function () {
        return $http.get('/api/BankDetails/AllBankDetails')
    }
    bankDetailAppFactory.SaveBankDetail = function (BankDetail) {
        return $http.post('/api/BankDetails/CreateBankDetail', BankDetail)
    };
    bankDetailAppFactory.UpdateBankDetail = function (BankDetail) {
        return $http.put('/api/BankDetails/UpdateBankDetail/' + BankDetail.BankId, BankDetail)
    };

    bankDetailAppFactory.DeleteBankDetail = function (BankDetail) {
        return $http.delete('/api/BankDetails/DeleteBankDetail/' + BankDetail.BankId)
    };
    bankDetailAppFactory.GetAllCustomers = function () {
        return $http.get('/api/Customers/AllCustomers')
    }
    return bankDetailAppFactory;

}])

