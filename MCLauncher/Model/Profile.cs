﻿using System;
using GalaSoft.MvvmLight;

namespace MCLauncher.Model
{
    [Serializable]
    public class Profile : ViewModelBase
    {
        private string _gameDirectory;
        private string _javaFile;
        private string _jvmArgs;
        private LauncherVisibility _launcherVisibility;
        private string _minecraftVersion;
        private string _name;
        private string _nickname;
        private bool _showAlpha;
        private bool _showBeta;
        private bool _showCustom;
        private bool _showRelease;
        private bool _showSnapshot;

        public Profile()
        {
            Name = string.Empty;
            Nickname = string.Empty;
            JavaFile = string.Empty;
            GameDirectory = string.Empty;
            JvmArgs = string.Empty;
            LauncherVisibility = LauncherVisibility.KeepOpen;
            MinecraftVersion = string.Empty;
            ShowCustom = false;
            ShowRelease = false;
            ShowSnapshot = false;
            ShowBeta = false;
            ShowAlpha = false;
        }

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public string Nickname
        {
            get => _nickname;
            set => Set(ref _nickname, value);
        }

        public string JavaFile
        {
            get => _javaFile;
            set => Set(ref _javaFile, value);
        }

        public string GameDirectory
        {
            get => _gameDirectory;
            set => Set(ref _gameDirectory, value);
        }

        public string JvmArgs
        {
            get => _jvmArgs;
            set => Set(ref _jvmArgs, value);
        }

        public string MinecraftVersion
        {
            get => _minecraftVersion;
            set => Set(ref _minecraftVersion, value);
        }

        public LauncherVisibility LauncherVisibility
        {
            get => _launcherVisibility;
            set => Set(ref _launcherVisibility, value);
        }

        public bool ShowCustom
        {
            get => _showCustom;
            set => Set(ref _showCustom, value);
        }

        public bool ShowRelease
        {
            get => _showRelease;
            set => Set(ref _showRelease, value);
        }

        public bool ShowSnapshot
        {
            get => _showSnapshot;
            set => Set(ref _showSnapshot, value);
        }

        public bool ShowBeta
        {
            get => _showBeta;
            set => Set(ref _showBeta, value);
        }

        public bool ShowAlpha
        {
            get => _showAlpha;
            set => Set(ref _showAlpha, value);
        }
    }
}