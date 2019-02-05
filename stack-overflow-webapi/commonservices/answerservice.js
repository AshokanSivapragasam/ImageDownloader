var mongoDbHelper = require('../commonservices/mongodbhelper.js');
var assert = require('assert');

var answerService = function() {
  var getAnswers = function(problemRefId, callback) {
    var mongoDbCollection = mongoDbHelper.mongoDatabase.collection('answers');
    mongoDbCollection.find({problemRefId: Number(problemRefId)}).toArray(function(error, answers) {
      assert.equal(error, null);
      callback(answers);
    });
  };

  var getAnswerById = function(answerId, callback) {
    var mongoDbCollection = mongoDbHelper.mongoDatabase.collection('answers');
    mongoDbCollection.findOne({id: Number(answerId)}, function(error, answer){
      assert.equal(error, null);
      callback(answer);
    });
  };

  var postAnswers = function(incomingAnswers) {
    var mongoDbCollection = mongoDbHelper.mongoDatabase.collection('answers');
    // Insert some documents
    mongoDbCollection.insertMany(incomingAnswers, function(error, result) {
      assert.equal(error, null);
      assert.equal(3, result.result.n);
      assert.equal(3, result.ops.length);
      console.log("Inserted " + incomingAnswers.length + " documents into the collection");
    });
  };

  return {
    getAnswers: getAnswers,
    getAnswerById: getAnswerById,
    postAnswers: postAnswers
  };
}();

module.exports = answerService;
