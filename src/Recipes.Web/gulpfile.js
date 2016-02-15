/// <binding AfterBuild='build' ProjectOpened='watch-less' />
var gulp = require('gulp'),
    del = require('del'),
    fs = require('fs'),
    sequence = require('run-sequence'),
    watch = require('gulp-watch'),
    useref = require('gulp-useref'),
    less = require('gulp-less');

eval('var project = ' + fs.readFileSync('./project.json'));

var src_root = __dirname + '/' + project.webroot + '/';
var dist_root = __dirname + '/' + project.webroot + '/dist';

var source = {
    styles: src_root + 'css/',
};

var dist = {
    styles: dist_root + 'css/',
    scripts: dist_root + 'scripts/'
}

gulp.task('less', function () {
    return gulp.src(source.styles + '**/*.less')
       .pipe(less())
       .pipe(gulp.dest(source.styles));
});

gulp.task('build', ['less'], function () {

    //TODO: copy over fonts and add min/uglify
    return gulp.src(src_root + 'index.htm')
        .pipe(useref())
        .pipe(gulp.dest(dist_root));
});

gulp.task('watch-less', function () {
    return watch(source.styles + '*.less', function () {
        sequence('less');
    });
})