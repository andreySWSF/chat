"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var SkypeWindowGuard = /** @class */ (function () {
    function SkypeWindowGuard() {
    }
    SkypeWindowGuard.prototype.canActivate = function (route, state) {
        return confirm('are you sure?');
    };
    return SkypeWindowGuard;
}());
exports.SkypeWindowGuard = SkypeWindowGuard;
//# sourceMappingURL=skype-window.guard.js.map