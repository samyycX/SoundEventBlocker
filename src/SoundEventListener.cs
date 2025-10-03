using System.Collections.Immutable;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SoundEventBlocker;

public class SoundEventListener {

  private ImmutableHashSet<uint> _blockedEvents = [];
  private ILogger<SoundEventListener> _logger;

  private void ParseConfig(ConfigModel cfg)
  {
    _blockedEvents = cfg.BlockedEvents.Select(name => MurmurHash2.HashStringLowercase(name, 0x53524332)).ToImmutableHashSet();
    _logger.LogInformation("Loaded {Count} blocked events.", _blockedEvents.Count);
  }

  public SoundEventListener(ISwiftlyCore core, IOptionsMonitor<ConfigModel> config, ILogger<SoundEventListener> logger) {
    _logger = logger;
    ParseConfig(config.CurrentValue);
    config.OnChange(ParseConfig);

    core.NetMessage.HookServerMessage<CMsgSosStartSoundEvent>(msg => {
      var currentConfig = config.CurrentValue;

      if (!currentConfig.Enabled) return HookResult.Continue;

      if (_blockedEvents.Contains(msg.SoundeventHash)) {
        if (currentConfig.Debug)
        {
          logger.LogInformation("Blocked event hash: {Hash})", msg.SoundeventHash);
        }
        return HookResult.Stop;
      }
      return HookResult.Continue;
    });
  }
}