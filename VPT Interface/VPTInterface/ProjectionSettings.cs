using System.IO;
using Serilog.Parsing;

namespace VPTInterface
{
    public class ProjectionSettings
    {
        public string imageName = "test_pattern.png";
        public double[] backgroundColor = new double[3] {0, 0, 0 };
        public double distance = 50;
        public double width = 50;
        public int monitor = 2;

        public ProjectionSettings() { }

        public static ProjectionSettings Restore()
        {
            ProjectionSettings settings = new ProjectionSettings();

            var settingsPath = FileLocations.SettingsFile;
            if (File.Exists(settingsPath))
            {
                settings = KLib.FileIO.XmlDeserialize<ProjectionSettings>(settingsPath);
            }
            return settings;
        }

        public void Save()
        {
            var settingsPath = FileLocations.SettingsFile;
            var folder = Path.GetDirectoryName(settingsPath);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            KLib.FileIO.XmlSerialize(this, settingsPath);
        }
    }
}