desc 'default'
task default: [:rebuild, :test]

desc 'build'
task :build do
  sh 'msbuild'
end

desc 'rebuild'
task :rebuild do
  sh 'msbuild /t:clean;rebuild'
end

desc 'run'
task :run do
  sh 'start iisexpress /site:Cards.Web'
end

desc 'test'
task :test do
  sh "./packages/xunit.runners.1.9.1/tools/xunit.console.clr4.exe ./src/Cards.Tests/bin/Debug/Cards.Tests.dll"
end
