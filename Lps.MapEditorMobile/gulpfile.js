var gulp = require('gulp');
var bower = require('gulp-bower');
var concat = require('gulp-concat');
var jshint = require('gulp-jshint');

gulp.task('bower', function () {
    return bower('./bower_components');
});

gulp.task('fonts', function () {
    gulp.src('bower_components/font-awesome/fonts/*')
        .pipe(gulp.dest('content/fonts'));
    gulp.src('bower_components/material-design-iconic-font/dist/fonts/*')
        .pipe(gulp.dest('content/fonts'));
    gulp.src('bower_components/glyphicons-halflings/fonts/*')
       .pipe(gulp.dest('content/fonts'));
});

gulp.task('css', function () {
    gulp.src('bower_components/font-awesome/css/*')
        .pipe(gulp.dest('content/css'));
    gulp.src('bower_components/material-design-iconic-font/dist/css/*')
        .pipe(gulp.dest('content/css'));
    gulp.src('bower_components/glyphicons-halflings/css/*')
       .pipe(gulp.dest('content/css'));
    gulp.src('bower_components/angular-bootstrap/*.css')
        .pipe(gulp.dest('content/css'));
});


gulp.task('scripts', function () {
    gulp.src('bower_components/angular/angular*js')
        .pipe(gulp.dest('scripts'));
    gulp.src('bower_components/angular-bootstrap/*.js')
        .pipe(gulp.dest('scripts'));
   
});

gulp.task('services', function () {
    gulp.src('app/scripts/services/**/*.js')
        .pipe(concat('services.js'))
        .pipe(gulp.dest('app/scripts'));
});

gulp.task('templates', function () {
    gulp.src('bower_components/covis-angular-devkit/covis/**/*')
        .pipe(gulp.dest('covis'));
});

gulp.task('dev', [
    
    
]);

gulp.task('default', [
        'dev'
]);
