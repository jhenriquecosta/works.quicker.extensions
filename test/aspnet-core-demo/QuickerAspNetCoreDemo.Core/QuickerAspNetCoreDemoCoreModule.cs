using Quicker.AutoMapper;
using Quicker.Localization;
using Quicker.Localization.Dictionaries;
using Quicker.Localization.Dictionaries.Json;
using Quicker.Modules;
using Quicker.Reflection.Extensions;

namespace QuickerAspNetCoreDemo.Core
{
    [DependsOn(typeof(QuickerAutoMapperModule))]
    public class QuickerAspNetCoreDemoCoreModule : QuickerModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            Configuration.Localization.Languages.Add(new LanguageInfo("en", "English", isDefault: true));
            Configuration.Localization.Languages.Add(new LanguageInfo("tr", "Türkçe"));

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource("QuickerAspNetCoreDemoModule",
                    new JsonEmbeddedFileLocalizationDictionaryProvider(
                        typeof(QuickerAspNetCoreDemoCoreModule).GetAssembly(),
                        "QuickerAspNetCoreDemo.Core.Localization.SourceFiles"
                    )
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(QuickerAspNetCoreDemoCoreModule).GetAssembly());
        }
    }
}