Array.prototype.firstOrDefault = function (method) {
    var returnItem = null;
    $.each(this, function (index, arrayItem) {
        if (method(arrayItem)) {
            returnItem = arrayItem;
            return false;
        }
    });
    return returnItem;
}

Array.prototype.any = function (method) {
    var indx = -1;
    $.each(this, function (index, arrayItem) {
        if (method(arrayItem)) {
            indx = index;
            return false;
        }
    });
    return indx != -1;
}

Array.prototype.where = function (method) {
    var returnArray = [];
    $.each(this, function (index, arrayItem) {
        if (method(arrayItem)) {
            returnArray.push(arrayItem);
        }
    });
    return returnArray;
}

Array.prototype.removeWhere = function (method) {
    var indexArray = [];
    $.each(this, function (index, arrayItem) {
        if (method(arrayItem)) {
            indexArray.push(index);
        }
    });
    var arry = this;
    indexArray.sort(inversSort);
    $.each(indexArray, function (index, indexItem) {
        arry.splice(indexItem, 1);
    });
}

function inversSort(a, b) {
    return a === b ? 0 : a < b ? 1 : -1;
}