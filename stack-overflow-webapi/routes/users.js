var express = require('express');
var userService = require('../commonservices/userservice.js');
var router = express.Router();

/* GET users listing. */
router.get('/', function(req, res, next) {
  userService.getAllUsers(function(allUsers){
    res.send(allUsers);
  });
});

router.get('/:userId', function(req, res, next) {
  userService.getUserById(req.params.userId, function(user){
    res.send(user);
  });
});

router.post('/', function(req, res, next) {
  var user = userService.postUsers(req.body);
  res.send("OK");
});

module.exports = router;
