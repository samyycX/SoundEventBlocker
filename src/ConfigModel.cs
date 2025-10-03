namespace SoundEventBlocker;

public class ConfigModel {
  public bool Enabled { get; set; } = true;

  public bool Debug { get; set; } = false;
  
  public List<string> BlockedEvents { get; set; } = new List<string>();
}