using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Shared.Plugins;
using SwiftlyS2.Shared;
using Microsoft.Extensions.Configuration;

namespace SoundEventBlocker;

[PluginMetadata(Id = "SoundEventBlocker", Version = "1.0.0", Name = "SoundEventBlocker", Author = "samyyc", Description = "Block configured sound events.")]
public partial class SoundEventBlocker : BasePlugin {
  public SoundEventBlocker(ISwiftlyCore core) : base(core)
  {
  }

  public override void ConfigureSharedServices(IServiceCollection sharedServices) {
  }

  public override void UseSharedServices(IServiceProvider sharedProvider) {
  }

  public override void Load(bool hotReload) {
    Core.Configuration
      .InitializeJsonWithModel<ConfigModel>("config.jsonc", "Main")
      .Configure(builder => {
        builder.AddJsonFile("config.jsonc", false, true);
      });
    
    ServiceCollection collection = new();
    collection
      .AddSwiftly(Core)
      .AddSingleton<SoundEventListener>();

    collection
      .AddOptionsWithValidateOnStart<ConfigModel>()
      .BindConfiguration("Main");

    ServiceProvider provider = collection.BuildServiceProvider();
    
    provider.GetRequiredService<SoundEventListener>();
    
  }

  public override void Unload() {
  }
} 