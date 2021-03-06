﻿using MCLauncher.UI;

namespace MCLauncher.Model
{
    public interface ISettingsModel
    {
        void EditProfile(string oldProfileName, Profile newProfile);
        AllVersions DownloadAllVersions();
        Profile LoadLastProfile();
        void OpenGameDirectory(string directory);
        string FindJava();
        void SaveProfile(Profile profile);
    }
}