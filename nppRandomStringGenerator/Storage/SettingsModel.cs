namespace nppRandomStringGenerator.Storage.Models
{
    public class SettingsModel
    {
        public string Appname { get; set; }
        public string Appversion { get; set; }
        public Config Configs { get; set; }
    }

    public class Config
    {
        public ConfigItem[] Default { get; set; }
        public ConfigItem[] Custom { get; set; }
    }

    public class ConfigItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}