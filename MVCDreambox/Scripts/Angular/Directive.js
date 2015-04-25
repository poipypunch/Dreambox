app.directive('showErrors', function ($timeout, showErrorsConfig) {
    var getShowSuccess, linkFn;
    getShowSuccess = function (options) {
        var showSuccess;
        showSuccess = showErrorsConfig.showSuccess;
        if (options && options.showSuccess != null) {
            showSuccess = options.showSuccess;
        }
        return showSuccess;
    };
    linkFn = function (scope, el, attrs, formCtrl) {
        var blurred, inputEl, inputName, inputNgEl, options, showSuccess, toggleClasses;
        blurred = false;
        options = scope.$eval(attrs.showErrors);
        showSuccess = getShowSuccess(options);
        inputEl = el[0].querySelector('[name]');
        inputNgEl = angular.element(inputEl);
        inputName = inputNgEl.attr('name');
        if (!inputName) {
            throw 'show-errors element has no child input elements with a \'name\' attribute';
        }
        inputNgEl.bind('blur', function () {
            blurred = true;
            return toggleClasses(formCtrl[inputName].$invalid);
        });
        scope.$watch(function () {
            return formCtrl[inputName] && formCtrl[inputName].$invalid;
        }, function (invalid) {
            if (!blurred) {
                return;
            }
            return toggleClasses(invalid);
        });
        scope.$on('show-errors-check-validity', function () {
            return toggleClasses(formCtrl[inputName].$invalid);
        });
        scope.$on('show-errors-reset', function () {
            return $timeout(function () {
                el.removeClass('has-error');
                el.removeClass('has-success');
                return blurred = false;
            }, 0, false);
        });
        return toggleClasses = function (invalid) {
            el.toggleClass('has-error', invalid);
            if (showSuccess) {
                return el.toggleClass('has-success', !invalid);
            }
        };
    };
    return {
        restrict: 'A',
        require: '^form',
        compile: function (elem, attrs) {
            if (!elem.hasClass('form-group')) {
                throw 'show-errors element does not have the \'form-group\' class';
            }
            return linkFn;
        }
    };
}
  );

app.provider('showErrorsConfig', function () {
    var _showSuccess;
    _showSuccess = false;
    this.showSuccess = function (showSuccess) {
        return _showSuccess = showSuccess;
    };
    this.$get = function () {
        return { showSuccess: _showSuccess };
    };
});

app.directive('csSelect', function () {
    return {
        require: '^stTable',
        template: '<input type="checkbox"/>',
        scope: {
            row: '=csSelect'
        },
        link: function (scope, element, attr, ctrl) {

            element.bind('change', function (evt) {
                scope.$apply(function () {
                    ctrl.select(scope.row, 'multiple');
                });
            });

            scope.$watch('row.isSelected', function (newValue, oldValue) {
                if (newValue === true) {
                    element.parent().addClass('st-selected');
                } else {
                    element.parent().removeClass('st-selected');
                }
            });
        }
    };
});
app.directive('checkList', function () {
    return {
        scope: {
            list: '=checkList',
            value: '@'
        },
        link: function (scope, elem, attrs) {
            var handler = function (setup) {
                var checked = elem.prop('checked');
                var index = scope.list.indexOf(scope.value);

                if (checked && index == -1) {
                    if (setup) elem.prop('checked', false);
                    else scope.list.push(scope.value);
                } else if (!checked && index != -1) {
                    if (setup) elem.prop('checked', true);
                    else scope.list.splice(index, 1);
                }
            };

            var setupHandler = handler.bind(null, true);
            var changeHandler = handler.bind(null, false);

            elem.on('change', function () {
                scope.$apply(changeHandler);
            });
            scope.$watch('list', setupHandler, true);
        }
    };
});
app.directive('passwordMatch', [function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, elem, attrs, controller) {
            var checker = function () {
                var e1 = scope.$eval(attrs.ngModel);
                var e2 = scope.$eval(attrs.passwordMatch);
                return (!e1 && !e2) || e1 == e2;
            };

            scope.$watch(checker, function (n) {
                controller.$setValidity('passwordMatch', n);
            });
        }
    };
}]);
app.directive('numbersOnly', function () {
    return {
        restrict: 'A',
        require: '?ngModel',
        link: function (scope, element, attrs, ngModel) {
            if (!ngModel) return;
            ngModel.$parsers.push(function (val) {
                var parsed = val.replace(/[^0-9]+/g, '');
                if (val !== parsed) {
                    ngModel.$setViewValue(parsed);
                    ngModel.$render();
                }
                return parsed;
            });
        }
    };
});
app.directive('uiTree', function () {
    return {
        template: '<ul class="uiTree"><ui-tree-node ng-repeat="node in tree"></ui-tree-node></ul>',
        replace: true,
        transclude: true,
        restrict: 'E',
        scope: {
            tree: '=ngModel',
            attrNodeId: "@",
            loadFn: '=',
            expandTo: '=',
            selectedId: '='
        },
        controller: function ($scope, $element, $attrs) {
            $scope.loadFnName = $attrs.loadFn;
            // this seems like an egregious hack, but it is necessary for recursively-generated
            // trees to have access to the loader function
            if ($scope.$parent.loadFn)
                $scope.loadFn = $scope.$parent.loadFn;

            // TODO expandTo shouldn't be two-way, currently we're copying it
            if ($scope.expandTo && $scope.expandTo.length) {
                $scope.expansionNodes = angular.copy($scope.expandTo);
                var arrExpandTo = $scope.expansionNodes.split(",");
                $scope.nextExpandTo = arrExpandTo.shift();
                $scope.expansionNodes = arrExpandTo.join(",");
            }
        }
    };
})
.directive('uiTreeNode', ['$compile', '$timeout', function ($compile, $timeout) {
    return {
        restrict: 'E',
        replace: true,
        template: '<li>' +
          '<div class="node" data-node-id="{{ nodeId() }}">' +
            '<a class="glyphicon glyphicon-chevron-right" ng-click="toggleNode(nodeId())""></a>' +
            '<a ng-hide="selectedId" ng-href="#/assets/{{ nodeId() }}">{{ node.name }}</a>' +
            '<span ng-show="selectedId" ng-class="css()" ng-click="setSelected(node)">' +
                '{{ node.name }}</span>' +
          '</div>' +
        '</li>',
        link: function (scope, elm, attrs) {
            scope.nodeId = function (node) {
                var localNode = node || scope.node;
                return localNode[scope.attrNodeId];
            };
            scope.toggleNode = function (nodeId) {
                var isVisible = elm.children(".uiTree:visible").length > 0;
                var childrenTree = elm.children(".uiTree");
                if (isVisible) {
                    scope.$emit('nodeCollapsed', nodeId);
                } else if (nodeId) {
                    scope.$emit('nodeExpanded', nodeId);
                }
                if (!isVisible && scope.loadFn && childrenTree.length === 0) {
                    // load the children asynchronously
                    var callback = function (arrChildren) {
                        scope.node.children = arrChildren;
                        scope.appendChildren();
                        elm.find("a.glyphicon i").show();
                        elm.find("a.glyphicon img").remove();
                        scope.toggleNode(); // show it
                    };
                    var promiseOrNodes = scope.loadFn(nodeId, callback);
                    if (promiseOrNodes && promiseOrNodes.then) {
                        promiseOrNodes.then(callback);
                    } else {
                        $timeout(function () {
                            callback(promiseOrNodes);
                        }, 0);
                    }
                    elm.find("a.glyphicon i").hide();
                    var imgUrl = "http://www.efsa.europa.eu/efsa_rep/repository/images/ajax-loader.gif";
                    elm.find("a.glyphicon").append('<img src="' + imgUrl + '" width="18" height="18">');
                } else {
                    childrenTree.toggle(!isVisible);
                    elm.find("a.glyphicon i").toggleClass("glyphicon-chevron-right");
                    elm.find("a.glyphicon i").toggleClass("glyphicon-chevron-down");
                }
            };

            scope.appendChildren = function () {
                // Add children by $compiling and doing a new ui-tree directive
                // We need the load-fn attribute in there if it has been provided
                var childrenHtml = '<ui-tree ng-model="node.children" attr-node-id="' +
                    scope.attrNodeId + '"';
                if (scope.loadFn) {
                    childrenHtml += ' load-fn="' + scope.loadFnName + '"';
                }
                // pass along all the variables
                if (scope.expansionNodes) {
                    childrenHtml += ' expand-to="expansionNodes"';
                }
                if (scope.selectedId) {
                    childrenHtml += ' selected-id="selectedId"';
                }
                childrenHtml += ' style="display: none"></ui-tree>';
                return elm.append($compile(childrenHtml)(scope));
            };

            scope.css = function () {
                return {
                    nodeLabel: true,
                    selected: scope.selectedId && scope.nodeId() === scope.selectedId
                };
            };
            // emit an event up the scope.  Then, from the scope above this tree, a "selectNode"
            // event is expected to be broadcasted downwards to each node in the tree.
            // TODO this needs to be re-thought such that the controller doesn't need to manually
            // broadcast "selectNode" from outside of the directive scope.
            scope.setSelected = function (node) {
                scope.$emit("nodeSelected", node);
            };
            scope.$on("selectNode", function (event, node) {
                scope.selectedId = scope.nodeId(node);
            });

            if (scope.node.hasChildren) {
                elm.find("a.glyphicon").append('<i class="glyphicon-chevron-right"></i>');
            }

            if (scope.nextExpandTo && scope.nodeId() == parseInt(scope.nextExpandTo, 10)) {
                scope.toggleNode(scope.nodeId());
            }
        }
    };
}]);
