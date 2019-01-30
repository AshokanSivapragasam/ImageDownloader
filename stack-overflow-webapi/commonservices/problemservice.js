var mongoDbHelper = require('../commonservices/mongodbhelper.js');
var assert = require('assert');

var problemService = function() {
  var getAllProblems = function(callback) {
    var mongoDbCollection = mongoDbHelper.mongoDatabase.collection('problems');
    mongoDbCollection.find().toArray(function(error, allProblems) {
      assert.equal(error, null);
      callback(allProblems);
    });
  };

  var getProblemById = function(problemId, callback) {
    var mongoDbCollection = mongoDbHelper.mongoDatabase.collection('problems');
    mongoDbCollection.findOne({id: Number(problemId)}, function(error, problem){
      assert.equal(error, null);
      callback(problem);
    });
  };

  var postProblems = function(incomingProblems) {
    var mongoDbCollection = mongoDbHelper.mongoDatabase.collection('problems');
    // Insert some documents
    mongoDbCollection.insertMany(incomingProblems, function(error, result) {
      assert.equal(error, null);
      assert.equal(3, result.result.n);
      assert.equal(3, result.ops.length);
      console.log("Inserted " + incomingProblems.length + " documents into the collection");
    });
  };

  return {
    getAllProblems: getAllProblems,
    getProblemById: getProblemById,
    postProblems: postProblems
  };
}();

module.exports = problemService;
