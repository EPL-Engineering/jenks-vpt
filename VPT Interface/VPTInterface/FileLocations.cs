using System.IO;
using System;

namespace VPTInterface
{
    public static class FileLocations
    {
        public static readonly string RootFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Jenks", "VPT");
        public static readonly string SettingsFile = Path.Combine(RootFolder, "Settings.xml");
        public static readonly string ImageFolder = Path.Combine(RootFolder, "Images");
    }
}

