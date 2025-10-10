using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Shared.Plugins;
using SwiftlyS2.Shared;
using Microsoft.Extensions.Configuration;

namespace SoundEventBlocker;

[PluginMetadata(Id = "SoundEventBlocker", Version = "1.0.1", Name = "SoundEventBlocker", Author = "samyyc", Description = "Block configured sound events.")]
public class SoundEventBlocker(ISwiftlyCore core) : BasePlugin(core) {
    private IServiceProvider _provider;

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

    _provider = collection.BuildServiceProvider();
    
    _provider.GetRequiredService<SoundEventListener>();
    
  }

  public override void Unload() {
  }
} 