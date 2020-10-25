(function (angular) {
    "use strict";

    angular
        .module("todoApp")
        .directive("pagination", [pagination])
        .directive("todoPaginatedList", [todoPaginatedList]);

    /**
     * Directive definition function of 'pagination' directive.
     * 
     * TODO: make it a reusable component (i.e. usable by any list of objects not just the Models.Todo model)
     * TODO: correctly parametrize scope (inherited? isolated? which properties?)
     * TODO: create appropriate functions (link? controller?) and scope bindings
     * TODO: make appropriate general directive configuration (support transclusion? replace content? EAC?)
     * 
     * @returns {} directive definition object
     */
    function pagination() {
        var directive = {
            restrict: "E", // example setup as an element only
            templateUrl: "app/templates/pagination.html",
            scope: {}, // example empty isolate scope
            controller: ["$scope", controller],
            link: link,
            transclude: true
        };

        function controller($scope) {
            $scope.firstPage = function () {

            }

            $scope.previousPage = function () {

            }

            $scope.nextPage = function () {

            }

            $scope.lastPage = function () {

            }
        }

        function link(scope, element, attrs) { }



        return directive;
    }

    /**
     * Directive definition function of 'todoPaginatedList'.
     * 
     * TODO: correctly parametrize scope (inherited? isolated? which properties?)
     * TODO: create appropriate functions (link? controller?) and scope bindings
     * TODO: make appropriate general directive configuration (support transclusion? replace content? EAC?)
     * 
     * @returns {} directive definition object
     */
    function todoPaginatedList() {
        var directive = {
            //require: "^^pagination",
            restrict: "E", // example setup as an element only
            templateUrl: "app/templates/todo.list.paginated.html",
            scope: {}, // example empty isolate scope
            controller: ["$scope", "$http", controller],
            link: link
        };

        function controller($scope, $http) { // example controller creating the scope bindings
            $scope.loading = true;
            $scope.columnsToSort = [
                { columnName: "Id", sortDirection: "desc", selected: false },
                { columnName: "Task", sortDirection: "desc", selected: false },
                { columnName: "Created Date", sortDirection: "desc", selected: true }
            ];
            $scope.todos = [];

            // example of xhr call to the server's 'RESTful' api
            $http.get("api/Todo/Todos/",
                {
                    params: {
                        currentPage: 1,
                        pageSize: 20,
                        orderBy: "CreatedDate",
                        sortDirection: "desc"
                    }
                }).
                then(response => $scope.todos = response.data.collection).
                finally(function () {
                    $scope.loading = false;
                });

            $scope.orderBy = function (element) {
                $scope.columnsToSort.forEach(function (item) {
                    item.columnName = item.columnName.replace(" ↓", "").replace(" ↑", "");
                    item.selected = false;
                });

                var target = angular.element(element.currentTarget);

                var columnName = target.attr("column-name");

                var columnToSort = $scope.columnsToSort.find(x => x.columnName.includes(columnName));
                columnToSort.selected = true;

                if (columnToSort.sortDirection === "desc") {
                    columnToSort.sortDirection = "asc";

                    columnToSort.columnName = columnName + " ↑";
                }
                else {
                    columnToSort.sortDirection = "desc";

                    columnToSort.columnName = columnName + " ↓";
                }

                $scope.loading = true;
                $scope.todos = [];
                // example of xhr call to the server's 'RESTful' api
                $http.get("api/Todo/Todos/",
                    {
                        params: {
                            currentPage: 1,
                            pageSize: 20,
                            orderBy: columnName,
                            sortDirection: columnToSort.sortDirection
                        }
                    }).
                    then(response => $scope.todos = response.data.collection).
                    finally(function () {
                        $scope.loading = false;
                    });
            }
        }

        function link(scope, element, attrs) { }

        return directive;
    }

})(angular);