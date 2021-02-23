let createError = require('http-errors');
let express = require('express');
let path = require('path');
let cookieParser = require('cookie-parser');
let cookieSession = require('cookie-session');
let logger = require('morgan');
let cors = require('cors');
let indexRouter = require('./routes/index');
let usersRouter = require('./routes/users');
let gameEngineRouteer = require('./routes/gameEngineApi');
let app = express();
app.use(cookieSession({
      name: 'session-id',
      keys: [
          "key1"
      ],
      secret:"cookiemonster",
      maxAge: 24 * 60 * 60 * 1000
    }
));
let c = cors({
  preflightContinue: true
});

// view engine setup
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'jade');
app.use(c);
app.use(logger('dev'));
app.use(express.json());
app.use(express.urlencoded({ extended: false }));

app.use(cookieParser());
app.use(express.static(path.join(__dirname, 'public')));
app.use(express.static(`${__dirname}/../build`));
app.use(function (req,res,next) {
  if(req.params.sessionid !== undefined){
      req.headers['sessionid'] = req.params.sessionid;
  }
  next();
});
app.use('/', indexRouter);
app.use('/session', usersRouter);
app.use('/gameEngine', gameEngineRouteer);
// catch 404 and forward to error handler
app.use(function(req, res, next) {
  next(createError(404));
});

// error handler
app.use(function(err, req, res, next) {
  // set locals, only providing error in development
  res.locals.message = err.message;
  res.locals.error = req.app.get('env') === 'development' ? err : {};

  // render the error page
  res.status(err.status || 500);
  res.render('error');
});

module.exports = app;
