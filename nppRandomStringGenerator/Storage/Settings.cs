using nppRandomStringGenerator.Properties;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using nppRandomStringGenerator.Storage.Models;
using System.Runtime.Serialization.Json;

namespace nppRandomStringGenerator.Storage
{
    public class Settings
    {
        public SettingsModel settings { get; set; }

        private string FilePath { get; set; }

        public void Load()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string savePath = Path.Combine(appDataPath, "CMBSolutions", "nppRandomStringGenerator");
            FilePath = Path.Combine(savePath, "nppRandomStringGenerator.json");

            if (!File.Exists(FilePath))
            {
                try
                {
                    if (!Directory.Exists(savePath))
                    {
                        Directory.CreateDirectory(savePath);
                    }
                    using (StreamWriter writer = new StreamWriter(FilePath))
                    {
                        writer.WriteLine(Encoding.UTF8.GetString(Resources.nppRandomStringGeneratorSettings));
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
                settings = DeserializeJson<SettingsModel>(FilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private SettingsModel DeserializeJson<SettingsModel>(string json)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(json));
                stream.Write(data, 0, data.Length);
                stream.Position = 0;

                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SettingsModel));
                return (SettingsModel)serializer.ReadObject(stream);
            }
        }

        private string SerializeToJson<SettingsModel>(SettingsModel obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SettingsModel));
                serializer.WriteObject(stream, obj);
                stream.Position = 0;

                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        // Save JSON string to a file
        public void Save()
        {
            string json = SerializeToJson(settings);
            File.WriteAllText(FilePath, json);                       
        }
    }
}
