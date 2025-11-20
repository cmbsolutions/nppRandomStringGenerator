using nppRandomStringGenerator.Properties;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using nppRandomStringGenerator.Storage.Models;
using System.Collections.Generic;

namespace nppRandomStringGenerator.Storage
{
    public class Settings
    {
        public SettingsModel settings { get; set; }

        private string FilePath { get; set; }

        public void Load(bool reset = false)
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string savePath = Path.Combine(appDataPath, "CMBSolutions", "nppRandomStringGenerator");
            FilePath = Path.Combine(savePath, "nppRandomStringGenerator.ini");

            if (!File.Exists(FilePath) || reset)
            {
                try
                {
                    if (!Directory.Exists(savePath))
                    {
                        Directory.CreateDirectory(savePath);
                    }
                    using (StreamWriter writer = new StreamWriter(FilePath))
                    {
                        writer.WriteLine(Resources.nppRandomStringGeneratorSettings);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error creating settingsfile");
                    return;
                }
            }

            try
            {
                settings = DeserializeIni(FilePath);


                if (settings.Appversion != "1.9.8")
                {
                    SettingsModel defaults = DeserializeIniFromString(Resources.nppRandomStringGeneratorSettings);

                    foreach (ConfigItem configitem in defaults.ConfigItems)
                    {
                        if (!settings.ConfigItems.Exists(c => c.Name == configitem.Name))
                        {
                            settings.ConfigItems.Add(configitem);
                        }
                    }
                    settings.Appname = "nppRandomStringGenerator";
                    settings.Appversion = "1.9.8";
                }
                //else
                //{
                //    string check = "";
                //    foreach (ConfigItem configitem in settings.ConfigItems)
                //    {
                //        check += configitem.Name + "|";
                //    }
                //    check = check.TrimEnd('|');
                    
                //    string checkHash;
                //    using (MD5 md5 = MD5.Create())
                //    {
                //        byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(check));
                //        checkHash = BitConverter.ToString(hashBytes).Replace("-", "").ToUpperInvariant();
                //    }
                //    if ( check != "392FD30BC7A07D4CBD4F176F9C57A835")
                //    {
                //        MessageBox.Show("Unknown settings found. Resetting to defaults.", "nppRandomStringGenerator", MessageBoxButtons.OK);

                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private SettingsModel DeserializeIni(string ini)
        {
            SettingsModel tmp = new SettingsModel
            {
                ConfigItems = new List<ConfigItem>()
            };

            using (FileStream stream = new FileStream(ini, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    String line = reader.ReadLine();
                    String[] parts = line.Split('=');
                    tmp.Appname = parts[1];

                    line = reader.ReadLine();
                    parts = line.Split('=');
                    tmp.Appversion = parts[1];

                    while (!reader.EndOfStream)
                    {
                        line = reader.ReadLine();
                        if (line == "" || line == null) break;
                        parts = line.Split(new char[] { '=' }, 2);

                        tmp.ConfigItems.Add(new ConfigItem { Name = parts[0], Value = parts[1] });
                    }
                }
            }

            return tmp;
        }

        private SettingsModel DeserializeIniFromString(string ini)
        {
            SettingsModel tmp = new SettingsModel
            {
                ConfigItems = new List<ConfigItem>()
            };

            using (StringReader reader = new StringReader(ini))
            {
                String line = reader.ReadLine();
                String[] parts = line.Split('=');
                tmp.Appname = parts[1];

                line = reader.ReadLine();
                parts = line.Split('=');
                tmp.Appversion = parts[1];

                while (line != "")
                {
                    line = reader.ReadLine();
                    if (line == "" || line == null) break;
                    parts = line.Split(new char[] { '=' }, 2);

                    tmp.ConfigItems.Add(new ConfigItem { Name = parts[0], Value = parts[1] });
                }
            }

            return tmp;
        }

        private string SerializeToIni(SettingsModel obj)
        {

            StringBuilder sb = new StringBuilder(); 

            sb.AppendLine($"appname={obj.Appname}");
            sb.AppendLine($"appversion={obj.Appversion}");
                    
            foreach ( ConfigItem configitem in obj.ConfigItems )
            {
                sb.AppendLine($"{configitem.Name}={configitem.Value}");
            }
            return sb.ToString();
        }

        // Save JSON string to a file
        public void Save()
        {
            string ini = SerializeToIni(settings);
            File.WriteAllText(FilePath, ini);                       
        }
    }
}
