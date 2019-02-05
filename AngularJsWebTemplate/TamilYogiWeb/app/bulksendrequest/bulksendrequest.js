'use strict';

angular.module('myApp.bulksendrequest', ['ngRoute'])

  .config(['$routeProvider', function ($routeProvider) {
  			$routeProvider.when('/bulksendrequest', {
  				templateUrl : 'bulksendrequest/bulksendrequest.html',
  				controller : 'BulksendRequestCtrl'
  			});
  		}
  	])

  .config(function ($mdThemingProvider) {
      // Configure a dark theme with primary foreground yellow
      $mdThemingProvider.theme('docs-dark', 'default')
      .primaryPalette('yellow')
      .dark();
    })

  .controller('BulksendRequestCtrl', ['$scope', '$http', 'fileUploadService', 'commonService', function ($scope, $http, fileUploadService, commonService) {
  			$scope.bulkSendModel = {
  				BatchId : 'b480ee7d-2444-4f37-ba6d-db279b87b60a',
  				BulksendId : 'b480ee7d-2444-4f37-ba6d-db279b87b60a',
  				TenantAccountId : '10290011',
  				EmailContentId : '317215',
  				BulksendApproach : true,
  				BulksendInputDataFile : '',
  				EmailClassification : false,
  				DataImportType : false,
  				IsEmailSendInvoke : true,
  				WantToScheduleEmailSendTime : true,
  				EmailSendScheduleDatetime : '2015-01-01',
  				DataExtensionTemplateName : 'TriggeredSendDataExtension'
  			};

  			$scope.sendBulkSendRequest = function () {
  				fileUploadService.uploadFileToUrl($scope.bulksenddatafile, commonService.webServiceBaseUrl + '/resources/filemanager/PostFileManager');
  				fileUploadService.saveModelToDatabase($scope.bulkSendModel, commonService.webServiceBaseUrl + '/resources/eirequests/AddBulksendRequest');
  			};
  		}
  	])

  .factory('fileUploadService', ['$q', '$http', function ($q, $http) {
  			var getModelAsFormData = function (data) {
  				var dataAsFormData = new FormData();
  				angular.forEach(data, function (key, value) {
  					dataAsFormData.append(value, key);
  					//alert(value + ' | ' + key);
  				});

  				return dataAsFormData;
  			};

  			var saveModelToDatabase = function (data, url) {
  				var deferred = $q.defer();
  				$http({
  					url : url,
  					method : 'POST',
  					headers : {
  						'Content-Type' : undefined
  					},
  					data : getModelAsFormData(data),
  					transformRequest : angular.identity,
  				}).success(function (result) {
  					alert(result);
  					deferred.resolve(result);
  				}).error(function (result, status) {
  					alert(status);
  					deferred.reject(status);
  				});
  				return deferred.promise;
  			};

  			var uploadFileToUrl = function (file, uploadUrl) {
  				var fd = new FormData();
  				fd.append('file', file);
  				$http.post(uploadUrl, fd, {
  					transformRequest : angular.identity,
  					headers : {
  						'Content-Type' : undefined
  					}
  				})
  				.success(function (result) {
  					alert(result);
  				})
  				.error(function (result, status) {
  					alert(status);
  				});
  			};

  			var createBulksendRequest = function (dataModel, createBulksendResourceUrl) {
  				$http({
  					url : commonService.webServiceBaseUrl + '/resources/eirequests/AddBulksendRequest',
  					method : 'POST',
  					data : JSON.stringify($scope.bulkSendModel),
  					contentType : 'application/json',
                    Accept : 'application/json'
  				}).success(function (data) {
  					alert(data.EmailInterchangeId);
  				}).error(function (data) {
  					alert("failed");
  				});
  			};

  			return {
  				saveModelToDatabase : saveModelToDatabase,
  				uploadFileToUrl : uploadFileToUrl
  			};
  		}
  	])

  .directive('customUploadFile', customUploadFile)

  .directive('fileNameModel', ['$parse', function ($parse) {
  			return {
  				restrict : 'A',
  				link : function (scope, element, attributes) {
  					var model = $parse(attributes.fileNameModel);
  					var modelSetter = model.assign;

  					element.bind('change', function () {
  						scope.$apply(function () {
  							modelSetter(scope, element[0].files[0]);
  						});
  					});
  				}
  			};
  		}
  	]);

  function customUploadFile() {
  	var directive = {
  		restrict : 'E',
  		templateUrl : 'bulksendrequest/customuploadfiletemplate.html',
  		link : customUploadFileLink
  	};
  	return directive;
  }

  function customUploadFileLink(scope, element, attrs) {
  	var hiddenFileInputSelector = $(element[0].querySelector('#hiddenFileInputSelector'));
  	var customFileInputSelector = $(element[0].querySelector('#customFileInputSelector'));

  	if (hiddenFileInputSelector.length && customFileInputSelector.length) {
  		customFileInputSelector.click(function (e) {
  			hiddenFileInputSelector.click();
  		});
  	}

  	hiddenFileInputSelector.on('change', function (elem) {
  		var files = elem.target.files;
  		if (files[0]) {
  			scope.bulkSendModel.BulksendInputDataFile = files[0].name;
  		} else {
  			scope.bulkSendModel.BulksendInputDataFile = null;
  		}
  		scope.$apply();
  	});
  }
