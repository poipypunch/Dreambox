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
app.directive('tree', function () {
    return {
        restrict: 'E', // tells Angular to apply this to only html tag that is <tree>
        replace: true, // tells Angular to replace <tree> by the whole template
        scope: {
            t: '=src' // create an isolated scope variable 't' and pass 'src' to it.  
        },
        template: '<ul><branch ng-repeat="c in t.categories" src="c"></branch></ul>'
    };
})

app.directive('branch', function ($compile) {
    return {
        restrict: 'E', // tells Angular to apply this to only html tag that is <branch>
        replace: true, // tells Angular to replace <branch> by the whole template
        scope: {
            b: '=src' // create an isolated scope variable 'b' and pass 'src' to it.  
        },
        template: '<li><a>{{ b.CategoryDesc }}</a></li>',
        link: function (scope, element, attrs) {
            //// Check if there are any children, otherwise we'll have infinite execution

            var has_children = angular.isArray(scope.b.categories);

            //// Manipulate HTML in DOM
            if (has_children) {
                element.append('<tree src="b"></tree>');

                // recompile Angular because of manual appending
                $compile(element.contents())(scope);
            }

            //// Bind events
            element.on('click', function (event) {
                event.stopPropagation();

                if (has_children) {
                    element.toggleClass('collapsed');
                }
            });
        }
    };
})

