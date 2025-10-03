<div align="center">
  <img src="https://pan.samyyc.dev/s/VYmMXE" />
  <h2><strong>SoundEventBlocker</strong></h2>
  <h3>No description.</h3>
</div>

<p align="center">
  <img src="https://img.shields.io/badge/build-passing-brightgreen" alt="Build Status">
  <img src="https://img.shields.io/github/downloads/samyycX/SoundEventBlocker/total" alt="Downloads">
  <img src="https://img.shields.io/github/stars/samyycX/SoundEventBlocker?style=flat&logo=github" alt="Stars">
  <img src="https://img.shields.io/github/license/samyycX/SoundEventBlocker" alt="License">
</p>

# SoundEventBlocker

A SwiftlyS2 plugin that helps you block specific sound events.

## Configuration

After you start the plugin once, the configuration file will be generated inside `addons/swiftlys2/configs/plugins/SoundEventBlocker/config.jsonc`.

The structure looks like this
```
{
  "Main": {
    "Enabled": true,
    "Debug": true,
    "BlockedEvents": [
      "HEGrenade.Bounce"
    ]
  }
}
```

Fill `BlockedEvents` with the sound event name you need to block.