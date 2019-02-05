var mongoDbHelper = require('../commonservices/mongodbhelper.js');
var assert = require('assert');

var userService = function() {
  var getAllUsers = function(callback) {
    var mongoDbCollection = mongoDbHelper.mongoDatabase.collection('users');
    mongoDbCollection.find({}).toArray(function(error, allUsers) {
      assert.equal(error, null);
      callback(allUsers);
    });
  };

  var getUserById = function(userId, callback) {
    var mongoDbCollection = mongoDbHelper.mongoDatabase.collection('users');
    mongoDbCollection.findOne({id: Number(userId)}, function(error, user){
      assert.equal(error, null);
      callback(user);
    });
  };

  var postUsers = function(incomingUsers) {
    var mongoDbCollection = mongoDbHelper.mongoDatabase.collection('users');
    // Insert some documents
    mongoDbCollection.insertMany(incomingUsers, function(error, result) {
      assert.equal(error, null);
      assert.equal(3, result.result.n);
      assert.equal(3, result.ops.length);
      console.log("Inserted " + incomingUsers.length + " documents into the collection");
    });
  };

  return {
    getAllUsers: getAllUsers,
    getUserById: getUserById,
    postUsers: postUsers
  };
}();

module.exports = userService;
