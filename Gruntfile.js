'use strict';

module.exports = function(grunt) {
   
    require('load-grunt-tasks')(grunt);

    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        watch: {
            livereload: {
                options: {
                    livereload: '<%= connect.options.livereload %>'
                },
                files: [
                    'app/{,*/}*.html',
                    'app/styles/{,*/}*.css',
                    'app/scripts/{,*/}*.js'
                ]
            }
        },
        connect: {
            options: {
                port: 9000,
                hostname: 'localhost',
                livereload: 35729
            },
            livereload: {
                options: {
                    open: true,
                    base: 'app',
                }
            }
        },
        karma: {
            unit: {
                configFile: 'karma.conf.js'
            }
        }
    });

    grunt.registerTask('test', 'Start karma test runner', function() {
        grunt.task.run([
            'karma'
        ]);
    });

    grunt.registerTask('server', 'About project.', function() {
        grunt.log.write('Launching Cards 2.0.0 Web Application');

        grunt.task.run([
            'connect:livereload',
            'api',
            'watch'
        ]);
    });

    grunt.registerTask('api', 'Start mock API server.', function() {
        grunt.log.write('Launching Cards 2.0.0 Mock REST API');
        var server = require('./server/server.js');

        server.listen(9002);
    });

}
