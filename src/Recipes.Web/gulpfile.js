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

var locations = {
    packages: node_modules,
    styles: root + 'css/',
    lib: root + 'lib/',
    tests_lib: root + 'tests/lib/'
};

gulp.task('compile-less', function () {
    gulp.src(locations.styles + '**/*.less')
       .pipe(less())
       .pipe(gulp.dest(locations.styles));
});

gulp.task('copy-lib', function () {
    del([locations.lib, locations.tests_lib],
        function () {            
            gulp.src(locations.packages + 'lodash/index.js')
                .pipe(gulp.dest(locations.lib + 'lodash'));
        });
});

gulp.task('build', function () {
    sequence('copy-lib', 'compile-less');
});

gulp.task('test', function (done) {
    //TODO: no more karma, use gulp-mocha
});

gulp.task('watch-less', function () {
    watch(locations.styles + '*.less', function () {
        sequence('compile-less');
    });
})