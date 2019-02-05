import { FileMock, FileError } from "@ionic-native-mocks/file";

FileMock.prototype.listDir = function (path, dirName) {
    var response = [];
    response.push({
    name: '1.jpg',
    isFile: true,
    isDirectory: false,
    fullPath: '<full_path>',
    nativeURL: '<native_url>'
    }, {
    name: '2.jpg',
    isFile: true,
    isDirectory: false,
    fullPath: '<full_path>',
    nativeURL: '<native_url>'
    }, {
    name: '3.jpg',
    isFile: true,
    isDirectory: false,
    fullPath: '<full_path>',
    nativeURL: '<native_url>'
    });

    return new Promise(function (resolve, reject) {
        resolve(response);
    });
};