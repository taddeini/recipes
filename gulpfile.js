'use strict';

var $ = require('gulp-load-plugins')();
var browserify = require('browserify');
var del = require('del');
var gulp = require('gulp');
var less = require('gulp-less');
var source = require('vinyl-source-stream');
var sourceFile = './app/scripts/app.js';
var destFolder = './dist/scripts';
var destFileName = 'app.js';

// Styles
gulp.task('styles', function () {
  return gulp.src(['app/styles/**/*.less', 'app/styles/**/*.css'])
    .pipe(less())
    .pipe($.autoprefixer('last 1 version'))
    .pipe(gulp.dest('dist/styles'));
});

// Scripts
gulp.task('scripts', function () {
  return browserify({entries: [sourceFile], insertGlobals: true, fullPaths: true})
    .bundle()
    .on('error', $.util.log.bind($.util, 'Browserify Error'))
    .pipe(source(destFileName))
    .pipe(gulp.dest(destFolder));
});

// HTML
gulp.task('html', function () {
  return gulp.src('app/*.html')
    .pipe($.useref())
    .pipe(gulp.dest('dist'));
});

// Images
gulp.task('images', function () {
  return gulp.src('app/images/**/*')
    .pipe($.cache($.imagemin({
      optimizationLevel: 3,
      progressive: true,
      interlaced: true
    })))
    .pipe(gulp.dest('dist/images'));
});

// Clean
gulp.task('clean', function (cb) {
  cb(del.sync(['dist/styles', 'dist/scripts', 'dist/images']));
});

// Bundle
gulp.task('bundle', ['styles', 'scripts'], function () {
  return gulp.src('./app/*.html')
    .pipe($.useref.assets())
    .pipe($.useref.restore())
    .pipe($.useref())
    .pipe(gulp.dest('dist'));
});

// Build
gulp.task('build', ['html', 'images', 'scripts', 'styles'], function () {
  gulp.src('dist/scripts/app.js')
    .pipe($.uglify())
    .pipe($.stripDebug())
    .pipe(gulp.dest('dist/scripts'));
});


// stuff I don't care about yet, but might care about in the future
//var watchify = require('watchify');
//
//Watch
//gulp.task('watch', ['html', 'bundle'], function () {
//
//  browserSync({
//    notify: false,
//    logPrefix: 'BS',
//    // Run as an https by uncommenting 'https: true'
//    // Note: this uses an unsigned certificate which on first access
//    //       will present a certificate warning in the browser.
//    // https: true,
//    server: ['dist', 'app']
//  });
//
//  gulp.watch('app/scripts/**/*.js', ['scripts', reload]);
//
//  // Watch .html files
//  gulp.watch('app/*.html', ['html']);
//
//  gulp.watch(['app/styles/**/*.scss', 'app/styles/**/*.css'], ['styles', reload]);
//
//  // Watch image files
//  gulp.watch('app/images/**/*', reload);
//});
