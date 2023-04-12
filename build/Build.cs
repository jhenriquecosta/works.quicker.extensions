using System.Linq;

using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Git;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Tooling;


using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using System.IO;
using Octokit;
using System.Threading.Tasks;
using Nuke.Common.Tools.GitHub;
using Nuke.Common.ChangeLog;
using System;
using Octokit.Internal;
using ParameterAttribute = Nuke.Common.ParameterAttribute;

[GitHubActions("continuous",
GitHubActionsImage.UbuntuLatest,
AutoGenerate = false,
FetchDepth = 0,
    OnPushBranches = new[] 
    {
        "main", 
        "developer",
        "releases/**"
    },
    OnPullRequestBranches = new[] 
    {
        "releases/**" 
    },
    InvokedTargets = new[]
    {
        nameof(Pack),
    },
    EnableGitHubToken = true,
    ImportSecrets = new[] 
    { 
        nameof(MyGetApiKey), 
        nameof(NuGetApiKey) 
    }
)]

class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Pack);
    
    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Parameter("MyGet Feed Url for Public Access of Pre Releases")]
    readonly string MyGetNugetFeed;
    [Parameter("MyGet Api Key"), Secret]
    readonly string MyGetApiKey;

    [Parameter("Nuget Feed Url for Public Access of Pre Releases")]
    readonly string NugetFeed;
    [Parameter("Nuget Api Key"), Secret]
    readonly string NuGetApiKey;

    [Parameter("Copyright Details")]
    readonly string Copyright;

    [Parameter("Artifacts Type")]
    readonly string ArtifactsType =  "*.nupkg";

    [Parameter("Excluded Artifacts Type")]
    readonly string ExcludedArtifactsType =  "*.symbols.nupkg";

    [GitVersion]
    readonly GitVersion GitVersion;

    [GitRepository]
    readonly GitRepository GitRepository;

    [Solution(GenerateProjects = true)]
    readonly Solution Solution;

    static GitHubActions GitHubActions => GitHubActions.Instance;
    static AbsolutePath ArtifactsDirectory => RootDirectory / ".artifacts";

    static readonly string PackageContentType = "application/octet-stream";
    static string ChangeLogFile => RootDirectory / "CHANGELOG.md";

    string GithubNugetFeed => GitHubActions != null
         ? $"https://nuget.pkg.github.com/{GitHubActions.RepositoryOwner}/index.json"
         : null;

     Target Info => _ => _
    .Description("Configures the build")
    .Executes(async ()=>
    {
          
         await Console.Out.WriteLineAsync($"Is Release           : {Configuration} ");
         await Console.Out.WriteLineAsync($"Is PullRequest       : {GitHubActions?.IsPullRequest} ");
         await Console.Out.WriteLineAsync($"Is Development       : {GitRepository.IsOnDevelopBranch()}");
         await Console.Out.WriteLineAsync($"Is Master            : {GitRepository.IsOnMasterBranch()}");
         await Console.Out.WriteLineAsync($"GitHub Source Pkg    : {GithubNugetFeed} ");
         await Console.Out.WriteLineAsync($"GitHub Token         : {GitHubActions?.Token} ");     
         await Console.Out.WriteLineAsync($"Root      Dir        : {RootDirectory.ToString()} ");
         await Console.Out.WriteLineAsync($"Artefacts Dir        : {ArtifactsDirectory?.ToString()} ");
         await Console.Out.WriteLineAsync($"ArtifactsType        : {ArtifactsType?.ToString()} ");
         await Console.Out.WriteLineAsync($"ExcludedArtifactsType: {ExcludedArtifactsType?.ToString()} ");        
         await Console.Out.WriteLineAsync($"GitHub               : {GitHubActions?.Token}");
         await Console.Out.WriteLineAsync($"Branch               : {GitHubActions?.Ref}");
         await Console.Out.WriteLineAsync($"Commit               : {GitHubActions?.Sha}");
        
    });
    Target Clean => _ => _
      .Description($"Cleaning Project.")
      .DependsOn(Info) 
      .Before(Restore)
      .Executes(() =>
      {
          //DotNetClean(c => c.se(Solution.src.));
          //EnsureCleanDirectory(ArtifactsDirectory);
           RootDirectory
                .GlobDirectories(
                    "src/**/obj",
                    "src/**/bin",
                    "tests/**/obj",
                    "tests/**/bin"
                ).ForEach(DeleteDirectory);
      });
    Target Restore => _ => _
        .Description($"Restoring Project Dependencies.")
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(
                r => r.SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .Description($"Building Project with the version.")
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(b => b
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetVersion(GitVersion.SemVer)
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetInformationalVersion(GitVersion.InformationalVersion)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .EnableNoRestore());
        });

     Target BuildPackages => _ => _
    .Description($"Packing Project with the version.")
    .Requires(() => Configuration.Equals(Configuration.Release))
    .Produces(ArtifactsDirectory / ArtifactsType)
    .DependsOn(Compile)
    .Executes(() =>
    {
         Serilog.Log.Information("PackagesDirectory {0}",ArtifactsDirectory);
         ArtifactsDirectory.GlobFiles("*.nupkg", "*symbols.nupkg").ForEach(DeleteFile);
                 DotNetPack(_ => _
                .SetConfiguration(Configuration)
                .SetOutputDirectory(ArtifactsDirectory)
                .SetIncludeSymbols(true)
                .SetNoBuild(InvokedTargets.Contains(Compile))
                .SetProperty("SourceLinkCreate", true)
                .CombineWith(
                    Solution.AllProjects.Where(p=>p.Path.ToString().Contains("src")), (_, v) => _
                        .SetProject(v)));
           
    });
    
    Target Pack => _ => _
    .Description($"Packing Project with the version.")
    .Requires(() => Configuration.Equals(Configuration.Release))
    .Produces(ArtifactsDirectory / ArtifactsType)
    .DependsOn(Compile)
    .Triggers(PublishToGithub)
    .Executes(() =>
        {
            Serilog.Log.Information("PackagesDirectory {0}",ArtifactsDirectory);
            ArtifactsDirectory.GlobFiles("*.nupkg", "*symbols.nupkg").ForEach(DeleteFile);
            
                 DotNetPack(_ => _
                .SetConfiguration(Configuration)
                .SetOutputDirectory(ArtifactsDirectory)
                .SetIncludeSymbols(true)
                .SetNoBuild(InvokedTargets.Contains(Compile))
                .SetProperty("SourceLinkCreate", true)
                .SetCopyright(Copyright)
                .SetVersion(GitVersion.NuGetVersionV2)
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetInformationalVersion(GitVersion.InformationalVersion)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .CombineWith(
                    Solution.AllProjects.Where(p=>p.Path.ToString().Contains("src")), (_, v) => _
                        .SetProject(v)));
           
        });
    
     Target PublishToGithub => _ => _
       .Description($"Publishing to Github for Development only.")
       .Triggers(CreateRelease)
       .Requires(() => Configuration.Equals(Configuration.Release))
       .OnlyWhenStatic(() => GitRepository.IsOnMainOrMasterBranch() || GitHubActions.IsPullRequest)
       .Executes(() =>
       {
           Console.Out.WriteLineAsync($"Publishing from {ArtifactsDirectory} types {ArtifactsType} excluding {ExcludedArtifactsType} to GITHUB...");
           GlobFiles(ArtifactsDirectory, ArtifactsType)
               .Where(x => !x.EndsWith(ExcludedArtifactsType))
               .ForEach(x =>
               {
                   var GithubSourceUrlPackage = string.IsNullOrEmpty(GithubNugetFeed) ? "https://nuget.pkg.github.com/jhenriquecosta/index.json" : GithubNugetFeed;
                   Console.Out.WriteLineAsync($"Publishing {x} to {GithubSourceUrlPackage} GITHUB Package...");
                   DotNetNuGetPush(s => s
                       .SetTargetPath(x)
                       .SetSource(GithubSourceUrlPackage)
                       .SetApiKey(GitHubActions?.Token)
                       .EnableSkipDuplicate()
                   );
               });
       });

    Target PublishToMyGet => _ => _
       .Description($"Publishing to MyGet for PreRelese only.")
       .Requires(() => Configuration.Equals(Configuration.Release))
       .Triggers(CreateRelease)
       .OnlyWhenStatic(() => GitRepository.IsOnReleaseBranch())
       .Executes(() =>
       {
           GlobFiles(ArtifactsDirectory, ArtifactsType)
               .Where(x => !x.EndsWith(ExcludedArtifactsType))
               .ForEach(x =>
               {
                   DotNetNuGetPush(s => s
                       .SetTargetPath(x)
                       .SetSource(MyGetNugetFeed)
                       .SetApiKey(MyGetApiKey)
                       .EnableSkipDuplicate()
                   );
               });
       });
    Target PublishToNuGet => _ => _
       .Description($"Publishing to NuGet with the version.")
       .Triggers(CreateRelease)
       .Requires(() => Configuration.Equals(Configuration.Release))
       .OnlyWhenStatic(() => GitRepository.IsOnMainOrMasterBranch())
       .Executes(() =>
       {
           GlobFiles(ArtifactsDirectory, ArtifactsType)
               .Where(x => !x.EndsWith(ExcludedArtifactsType))
               .ForEach(x =>
               {
                   DotNetNuGetPush(s => s
                       .SetTargetPath(x)
                       .SetSource(NugetFeed)
                       .SetApiKey(NuGetApiKey)
                       .EnableSkipDuplicate()
                   );
               });
       });

    Target CreateRelease => _ => _
       .Description($"Creating release for the publishable version.")
       .Requires(() => Configuration.Equals(Configuration.Release))
       .OnlyWhenStatic(() => GitRepository.IsOnMainOrMasterBranch() || GitRepository.IsOnReleaseBranch())
       .Executes(async () =>
       {
           var credentials = new Credentials(GitHubActions.Token);
           GitHubTasks.GitHubClient = new GitHubClient(new ProductHeaderValue(nameof(NukeBuild)),
               new InMemoryCredentialStore(credentials));

           var (owner, name) = (GitRepository.GetGitHubOwner(), GitRepository.GetGitHubName());

           var releaseTag = GitVersion.NuGetVersionV2;
           var changeLogSectionEntries = ChangelogTasks.ExtractChangelogSectionNotes(ChangeLogFile);
           var latestChangeLog = changeLogSectionEntries
               .Aggregate((c, n) => c + Environment.NewLine + n);

           var newRelease = new NewRelease(releaseTag)
           {
               TargetCommitish = GitVersion.Sha,
               Draft = true,
               Name = $"v{releaseTag}",
               Prerelease = !string.IsNullOrEmpty(GitVersion.PreReleaseTag),
               Body = latestChangeLog
           };

           var createdRelease = await GitHubTasks
                                       .GitHubClient
                                       .Repository
                                       .Release.Create(owner, name, newRelease);

           GlobFiles(ArtifactsDirectory, ArtifactsType)
              .Where(x => !x.EndsWith(ExcludedArtifactsType))
              .ForEach(async x => await UploadReleaseAssetToGithub(createdRelease, x));

           await GitHubTasks
                      .GitHubClient
                      .Repository
                      .Release
              .Edit(owner, name, createdRelease.Id, new ReleaseUpdate { Draft = false });
       });


    private static async Task UploadReleaseAssetToGithub(Release release, string asset)
    {
        await using var artifactStream = File.OpenRead(asset);
        var fileName = Path.GetFileName(asset);
        var assetUpload = new ReleaseAssetUpload
        {
            FileName = fileName,
            ContentType = PackageContentType,
            RawData = artifactStream,
        };
        await GitHubTasks.GitHubClient.Repository.Release.UploadAsset(release, assetUpload);
    }
}