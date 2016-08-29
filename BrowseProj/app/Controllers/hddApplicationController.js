app.controller('hddApplicationController', function ($scope) {

        
    getLocalDisks();
    
    
    function browse()
    {
        getFolders();
        getFiles();
        flag = true;
        getFileSizes();
    }

    
    function getFolders()
    {
        $.ajax({
            type: "Post",
            url: "api/Folders/",
            data: JSON.stringify($scope.currentpath),
            contentType: "application/json",
            success: onFoldersSuccess
        });
    }

    function getFiles()
    {
        $.ajax({
            type: "Post",
            url: "api/Files",
            data: JSON.stringify($scope.currentpath),
            contentType: "application/json",
            success: onFilesSuccess
        });
    }

    function getFileSizes()
    {
        $.ajax({
            type: "Post",
            url: "api/Sizes",
            data: JSON.stringify($scope.currentpath),
            contentType: "application/json",
            success: onSizesSuccess
        });
    }
    
    function onFoldersSuccess(data) {
        $scope.folders = createUserBrowseViewData(data);
        apply();
    }

    function onFilesSuccess(data) {
        $scope.files = createUserBrowseViewData(data);
        apply();
    }

    function onSizesSuccess(data) {
        
        if (flag == true)
        {
            $scope.less10 = data[0];
            $scope.from10to50 = data[1];
            $scope.morethan100 = data[2];
            apply();
            flag = false;
        }
        
    }

    $scope.onFolderClick = function (folder)
    {
        $scope.currentpath += folder + "\\";
        browse();
    }

    $scope.onUpClick = function ()
    {
        
        $scope.patharr = $scope.currentpath.split('\\');
        if ($scope.patharr[$scope.patharr.length - 1] == "")
        {
            $scope.patharr.splice(-1, 1);
        }
        
        if ($scope.patharr.length > 1)
        {

            $scope.patharr.splice($scope.patharr.length - 1, 1);
            if (($scope.patharr.length == 1) && ($scope.patharr[0].indexOf("\\") == -1))
            {
                $scope.patharr[0] += "\\";
            }
            $scope.currentpath = $scope.patharr.join('\\');
            browse();
          
        }
        else
        {
            if ($scope.patharr.length <= 1)
            {
                getLocalDisks();
            }
         
        }
        
    }

    
    function getLocalDisks()
    {
        $scope.files = "";
        $scope.currentpath = "";
        $.ajax({
            type: "Get",
            url: "api/Folders/",
            success: onFoldersSuccess
        });
        deleteSizes();
        flag = false;
    }

    function apply()
    {
        if (!$scope.$$phase)
        {
            $scope.$apply();
        }
    }

    function createUserBrowseViewData(fullpath)
    {
        var userBrowserViewData = [];
        fullpath.forEach(function (item, i, arr) {
            
            var viewitem = item.replace($scope.currentpath, "");
            
            userBrowserViewData.push(viewitem);
        });
        
        return userBrowserViewData;
    }

    function deleteSizes()
    {
        $scope.less10 = 0;
        $scope.from10to50 = 0;
        $scope.morethan100 = 0;
    }

});


