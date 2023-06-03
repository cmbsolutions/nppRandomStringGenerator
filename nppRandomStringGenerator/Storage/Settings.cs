using nppRandomStringGenerator.Properties;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using nppRandomStringGenerator.Storage.Models;
using System.Linq;

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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private SettingsModel DeserializeIni(string ini)
        {
            SettingsModel tmp = new SettingsModel();
            tmp.ConfigItems = new ConfigItem[18];

            using (FileStream stream = new FileStream(ini, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    String line = reader.ReadLine();
                    String[] parts = line.Split('=');
                    tmp.Appname = parts[0];

                    line = reader.ReadLine();
                    parts = line.Split('=');
                    tmp.Appversion = parts[1];

                    int i = 0;
                    while (!reader.EndOfStream || i >= 18)
                    {
                        line = reader.ReadLine();
                        if (line == "" || line == null) break;
                        parts = line.Split(new char[] { '=' }, 2);

                        tmp.ConfigItems[i] = new ConfigItem { Name = parts[0], Value = parts[1] };
                        
                        i++;
                    }
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
