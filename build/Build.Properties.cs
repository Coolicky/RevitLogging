using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;

partial class Build
{
    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    const string ArtifactsFolder = "output";
    readonly AbsolutePath ArtifactsDirectory = RootDirectory / ArtifactsFolder;
    const string Author = "Coolicky";
    const string Description = "Serilog Extension for Logging events & Errors for Revit Plugins";
    readonly string[] Tags = { "revit", "serilog", "logging" };
    const string RepoType = "git";
    const string PackageIcon = ".nuget/PackageIcon.png";
    const string LicenceFile = "License.md";
}