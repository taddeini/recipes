// var gulp = require('gulp'),
//   del = require('del'),
//   fs = require('fs'),
//   concat = require('gulp-concat'),
//   sequence = require('run-sequence'),
//   uglify = require('gulp-uglify'),
//   minify = require('gulp-cssnano'),
//   source = require('vinyl-source-stream'),
//   buffer = require('vinyl-buffer'),
//   reactify = require('reactify'),
//   browserify = require('browserify'),
//   less = require('gulp-less');

// eval('var project = ' + fs.readFileSync('./project.json'));
// var dist_root = './' + project.webroot + '/';

// gulp.task('clean', function() {
//   return del.sync(dist_root + '/**');
// });

// gulp.task('fonts', function() {
//   return gulp.src('./Fonts/**/*.*')
//     .pipe(gulp.dest(dist_root + '/fonts'));
// });

// gulp.task('styles', function() {
//   return gulp.src(
//     [
//       'node_modules/font-awesome/css/font-awesome.css',
//       'node_modules/skeleton-css/css/normalize.css',
//       'node_modules/skeleton-css/css/skeleton.css',
//       './Styles/main.less'
//     ])
//     .pipe(less())
//     .pipe(concat('app.css'))
//     //.pipe(minify())
//     .pipe(gulp.dest(dist_root + '/css'));
// });

// gulp.task('pre-build', function() {
//   return sequence('clean', 'fonts', 'styles');
// });

// gulp.task('build', ['pre-build'], function() {
//   return browserify('./Scripts/App.jsx')
//     .transform(reactify)
//     .bundle()
//     .pipe(source('app.js'))
//     .pipe(buffer())
//     //.pipe(uglify())
//     .pipe(gulp.dest(dist_root));
// });