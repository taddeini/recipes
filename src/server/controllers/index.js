module.exports = function (app) {
  app.get('*', function (req, res) {
    console.log(req.path);
    res.render('index');
  });
};  
