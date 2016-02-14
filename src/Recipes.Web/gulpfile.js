/// <binding AfterBuild='build' ProjectOpened='watch-less' />
var gulp = require('gulp'),
    del = require('del'),
    fs = require('fs'),
    sequence = require('run-sequence'),
    watch = require('gulp-watch'),
    less = require('gulp-less');

eval('var project = ' + fs.readFileSync('./project.json'));

var root = __dirname + '/' + project.webroot + '/';
var node_modules = __dirname + '/node_modules/';

var sources = {
    styles: root + 'css/',
    lodash: node_modules + 'lodash/index.js',
    react: node_modules + 'react/dist/react.js'
};

var destinations = {
    styles: root + 'css/',
    lib: root + 'lib/',
    tests_lib: root + 'tests/lib/'
}

gulp.task('compile-less', function () {
    return gulp.src(sources.styles + '**/*.less')
       .pipe(less())
       .pipe(gulp.dest(destinations.styles));
});

gulp.task('copy-lib', function () {
    return gulp.src([sources.lodash, sources.react])
        .pipe(gulp.dest(destinations.lib));
});

gulp.task('build', function () {
    return sequence('copy-lib', 'compile-less');
});

gulp.task('test', function (done) {
    //TODO: no more karma, use gulp-mocha
});

gulp.task('watch-less', function () {
    return watch(sources.styles + '*.less', function () {
        sequence('compile-less');
    });
})