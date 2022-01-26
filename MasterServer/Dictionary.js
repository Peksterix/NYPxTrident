"use strict";
/*
Copyright (c) 2015 Fabio Landoni (http://fabiolandoni.ch/)


Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
Object.defineProperty(exports, "__esModule", { value: true });
exports.Dictionary = void 0;
;
var Dictionary = /** @class */ (function () {
    function Dictionary() {
        this._keys = [];
        this._values = [];
        this.undefinedKeyErrorMessage = "Key is either undefined, null or an empty string.";
    }
    Dictionary.prototype.isEitherUndefinedNullOrStringEmpty = function (object) {
        return (typeof object) === "undefined" || object === null || object.toString() === "";
    };
    Dictionary.prototype.checkKeyAndPerformAction = function (action, key, value) {
        if (this.isEitherUndefinedNullOrStringEmpty(key)) {
            throw new Error(this.undefinedKeyErrorMessage);
        }
        return action(key, value);
    };
    Dictionary.prototype.add = function (key, value) {
        var _this = this;
        var addAction = function (key, value) {
            if (_this.containsKey(key)) {
                throw new Error("An element with the same key already exists in the dictionary.");
            }
            _this._keys.push(key);
            _this._values.push(value);
        };
        this.checkKeyAndPerformAction(addAction, key, value);
    };
    Dictionary.prototype.remove = function (key) {
        var _this = this;
        var removeAction = function (key) {
            if (!_this.containsKey(key)) {
                return false;
            }
            var index = _this._keys.indexOf(key);
            _this._keys.splice(index, 1);
            _this._values.splice(index, 1);
            return true;
        };
        return (this.checkKeyAndPerformAction(removeAction, key));
    };
    Dictionary.prototype.getValue = function (key) {
        var _this = this;
        var getValueAction = function (key) {
            if (!_this.containsKey(key)) {
                return null;
            }
            var index = _this._keys.indexOf(key);
            return _this._values[index];
        };
        return this.checkKeyAndPerformAction(getValueAction, key);
    };
    Dictionary.prototype.containsKey = function (key) {
        var _this = this;
        var containsKeyAction = function (key) {
            if (_this._keys.indexOf(key) === -1) {
                return false;
            }
            return true;
        };
        return this.checkKeyAndPerformAction(containsKeyAction, key);
    };
    Dictionary.prototype.changeValueForKey = function (key, newValue) {
        var _this = this;
        var changeValueForKeyAction = function (key, newValue) {
            if (!_this.containsKey(key)) {
                throw new Error("In the dictionary there is no element with the given key.");
            }
            var index = _this._keys.indexOf(key);
            _this._values[index] = newValue;
        };
        this.checkKeyAndPerformAction(changeValueForKeyAction, key, newValue);
    };
    Dictionary.prototype.keys = function () {
        return this._keys;
    };
    Dictionary.prototype.values = function () {
        return this._values;
    };
    Dictionary.prototype.count = function () {
        return this._values.length;
    };
    return Dictionary;
}());
exports.Dictionary = Dictionary;