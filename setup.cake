#tool "nuget:?package=NUnit.Runners&version=2.6.4"
///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
   // Executed BEFORE the first task.
   Information("Running tasks...");
});

Teardown(ctx =>
{
   // Executed AFTER the last task.
   Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Default")
.IsDependentOn("Test")
.Does(() => {
});

Task("Clean")
.Does(() => {
   var settings = new DeleteDirectorySettings {
      Recursive = true,
      Force = true
   };
   DeleteDirectories(GetDirectories("./src/**/bin/"), settings);
   DeleteDirectories(GetDirectories("./src/**/obj/"), settings);
   DeleteDirectories(GetDirectories("./test/**/bin/"), settings);
   DeleteDirectories(GetDirectories("./test/**/obj/"), settings);
});

Task("Build")
.IsDependentOn("Clean")
.Does(() => {
   MSBuild("./dk.gov.oiosi.library.sln", configurator =>
    configurator.SetConfiguration(configuration)
        .SetVerbosity(Verbosity.Minimal)
        .WithRestore());
});

Task("Test")
.IsDependentOn("Build")
.Does(() => {
   var assemblies = GetFiles($"./test/**/bin/{configuration}/net461/dk.gov.oiosi.test.unit.dll");
   NUnit(assemblies, new NUnitSettings {
      Framework = "net-4.5"
   });
});

RunTarget(target);
