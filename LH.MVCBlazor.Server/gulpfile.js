// gulpfile.js

const gulp = require('gulp');
const sass = require('gulp-sass')(require('sass'));

// Compile Sass to CSS
gulp.task('sass', function () {
    
    return gulp.src('./node_modules/nhsuk-frontend/packages/**/*.scss')
    .pipe(sass().on('error', sass.logError))
    .pipe(gulp.dest('./wwwroot/css/nhsuk/'));
});
gulp.task('copy-nhsuk-js', function () {
    return gulp.src('./node_modules/nhsuk-frontend/dist/nhsuk-9.0.1.min.js')
        .pipe(gulp.dest('./wwwroot/js/'))
        .on('end', () => console.log('NHS UK JS copy complete.'));  // Adjust destination as needed
});


// Watch for changes
gulp.task('watch', function () {
    gulp.watch('./node_modules/nhsuk-frontend/packages/**/*.scss', gulp.series('sass')); // Monitor SCSS files in the npm package
    gulp.watch('./node_modules/nhsuk-frontend/dist/nhsuk-9.0.1.min.js', gulp.series('copy-nhsuk-js')); // Monitor the JS file
});

// Default task
gulp.task('default', gulp.series('sass', 'copy-nhsuk-js', 'watch')); // Run sass, copy js, and watch tasks
