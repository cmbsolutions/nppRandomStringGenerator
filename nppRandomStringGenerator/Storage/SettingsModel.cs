using System;

namespace nppRandomStringGenerator.Storage.Models
{
    public class SettingsModel
    {
        public string Appname { get; set; }
        public string Appversion { get; set; }
        public ConfigItem[] ConfigItems { get; set; }
    }

    public class ConfigItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}