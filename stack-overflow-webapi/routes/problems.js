var express = require('express');
var problemService = require('../commonservices/problemservice.js');
var answerService = require('../commonservices/answerservice.js');
var userService = require('../commonservices/userservice.js');
var router = express.Router();

router.get('/', function(req, res, next) {
  problemService.getAllProblems(function(allProblems){
    userService.getAllUsers(function(allUsers){
      for(var problemIndex = 0; problemIndex < allProblems.length; problemIndex += 1) {
        allUsers.forEach(function(user, userIndex){
          if(allProblems[problemIndex].postedBy == user.id){
            allProblems[problemIndex].postedBy = user;
          }
        });
      }
      res.send(allProblems);
    });
  });
});

router.get('/:problemId', function(req, res, next) {
  problemService.getProblemById(req.params.problemId, function(problem){
    userService.getUserById(problem.postedBy, function(user){
      problem.postedBy = user;
      answerService.getAnswers(req.params.problemId, function(answers){
        userService.getAllUsers(function(allUsers){
          for(var answerIndex = 0; answerIndex < answers.length; answerIndex += 1) {
            allUsers.forEach(function(user, userIndex){
              if(answers[answerIndex].postedBy == user.id){
                answers[answerIndex].postedBy = user;
              }
            });
          }
          problem.answers = answers;
          res.send(problem);
        });
      });
    });
  });
});

router.post('/', function(req, res, next) {
  var problem = problemService.postProblems(req.body);
  res.send("OK");
});

module.exports = router;
