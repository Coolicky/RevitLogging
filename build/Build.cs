using Nuke.Common;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

partial class Build : NukeBuild
{
    public static int Main () => Execute<Build>(x => x.Compile);

    Target Clean => _ => _
        .Executes(() =>
        {
            EnsureCleanDirectory(ArtifactsDirectory);
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore();
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(r => r
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .SetInformationalVersion(GitVersion.InformationalVersion)
                .SetConfiguration(Configuration)
                .SetVerbosity(DotNetVerbosity.Minimal));
        });

    Target Pack => _ => _
        .DependsOn(Compile)
        .Requires(() => GitVersion)
        .Executes(() =>
        {
            DotNetPack(settings => settings
                .SetConfiguration(Configuration)
                .SetVersion(GitVersion.NuGetVersion)
                .SetPackageIconUrl(PackageIcon)
                .SetPackageLicenseUrl(LicenceFile)
                .SetAuthors(Author)
                .SetDescription(Description)
                .SetPackageTags(Tags)
                .SetRepositoryType(RepoType)
                .SetRepositoryUrl(GitRepository.HttpsUrl)
                //TODO: Set properly when microsoft fixes the issue
                // .SetOutputDirectory(ArtifactsDirectory)
                .SetVerbosity(DotNetVerbosity.Minimal));
        });

}
