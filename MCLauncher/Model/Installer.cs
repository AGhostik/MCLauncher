﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using MCLauncher.Model.AssetsJson;
using MCLauncher.Model.MinecraftVersionJson;
using Newtonsoft.Json.Linq;

namespace MCLauncher.Model
{
    public class Installer
    {
        private readonly FileManager _fileManager;
        private readonly List<Tuple<Uri, string>> _downloadQueue;
        private readonly List<Tuple<string, string[]>> _extractQueue;

        public Installer(FileManager fileManager)
        {
            _fileManager = fileManager;
            _downloadQueue = new List<Tuple<Uri, string>>();
            _extractQueue = new List<Tuple<string, string[]>>();
        }

        public async void Install(Profile profile)
        {
            _checkDirectories(profile.GameDirectory, profile.CurrentVersion);

            //checking profile fields

            await _checkMinecraftVersion(profile.GameDirectory, profile.CurrentVersion);

            await _checkLibraries(profile.GameDirectory, profile.CurrentVersion);

            await _checkAssets(profile.GameDirectory, profile.CurrentVersion);

            MessageBox.Show("Install completed");
        }

        private async Task _checkAssets(string gameDirectory, string currentVersion)
        {
            var versionPath = $"{gameDirectory}versions\\{currentVersion}\\{currentVersion}";
            var minecraftVersion = _fileManager.ParseJson<MinecraftVersion>($"{versionPath}.json");
            var assetIndex = _fileManager.DownloadJson(minecraftVersion.AssetIndex.Url);
            
            var objects = assetIndex["objects"];
            var assets = objects.Values<JProperty>();
            foreach (var asset in assets)
            {
                var hash = asset.First["hash"].ToString();

                var subDirectory = $"{hash[0]}{hash[1]}";

                var directory = $"{gameDirectory}assets\\objects\\{subDirectory}\\";

                _checkDirectory(directory);

                _addToDownloadQueue($"{ModelResource.AssetsUrl}{subDirectory}/{hash}",$"{directory}{hash}");
            }

            await _downloadFromQueue();
        }

        private async Task _checkLibraries(string gameDirectory, string currentVersion)
        {
            var versionPath = $"{gameDirectory}versions\\{currentVersion}\\{currentVersion}";
            var minecraftVersion = _fileManager.ParseJson<MinecraftVersion>($"{versionPath}.json");
            foreach (var library in minecraftVersion.Libraries)
            {
                if (!_isLibraryAllow(library))
                {
                    continue;
                }

                //
                var libraryNameParts = library.Name.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                var assembly = libraryNameParts[0];
                var name = libraryNameParts[1];
                var version = libraryNameParts[2];

                string url;

                if (library.Url != null)
                {
                    url = $"{library.Url}{assembly.Replace('.', '/')}/{name}/{version}/{name}-{version}.jar";
                }
                else if (library.Downloads?.Classifiers?.NativesWindows != null)
                {
                    url = library.Downloads.Classifiers.NativesWindows.Url;
                }
                else if (library.Downloads?.Artifact != null)
                {
                    url = library.Downloads.Artifact.Url;
                }
                else
                {
                    url = $"{ModelResource.LibrariesUrl}{assembly.Replace('.', '/')}/{name}/{version}/{name}-{version}.jar";
                }

                var os = string.Empty;
                if (library.Natives != null)
                {
                    if (library.Natives.Windows == "natives-windows-${arch}")
                    {
                        os = Environment.Is64BitOperatingSystem ? "-natives-windows-64" : "-natives-windows-32";
                    }
                    else
                    {
                        os = $"-{library.Natives.Windows}";
                    }
                }

                var savingDirectory = $"{gameDirectory}libraries\\{assembly.Replace('.', '\\')}\\{name}\\{version}\\";
                var savingFile = $"{savingDirectory}{name}-{version}{os}.jar";

                _checkDirectory(savingDirectory);
                _addToDownloadQueue(url, savingFile);

                _checkLibraryExtract(library, savingFile);
                //
            }

            await _downloadFromQueue();
            _extractFromQueue(gameDirectory, currentVersion);
        }
        
        private void _checkLibraryExtract(Libraries library, string savingFile)
        {
            if (library.Extract == null || !library.Extract.Exclude.Any()) return;

            var extractItem = new Tuple<string, string[]>(savingFile, library.Extract.Exclude);
            _extractQueue.Add(extractItem);
        }

        private static bool _isLibraryAllow(Libraries library)
        {
            if (library.Rules == null) return true;

            foreach (var rule in library.Rules)
            {
                if (rule.Action != null && rule.Action == "disallow" && rule.Os?.Name != null && rule.Os.Name == "windows") 
                    return false;
            }

            return true;
        }

        private void _extractFromQueue(string gameDirectory, string currentVersion)
        {
            var natives = $"{gameDirectory}versions\\{currentVersion}\\natives";
            foreach (var extracTuple in _extractQueue)
            {
                _fileManager.ExtractToDirectory(extracTuple.Item1, natives);
                foreach (var fileOrDirectory in extracTuple.Item2)
                {
                    _fileManager.Delete($"{natives}\\{fileOrDirectory}");
                }
            }
        }

        private async Task _checkMinecraftVersion(string gameDirectory, string currentVersion)
        {
            var versionFilePath = $"{gameDirectory}versions\\{currentVersion}\\{currentVersion}";
            if (!_fileManager.FileExist($"{versionFilePath}.jar"))
            {
                _addToDownloadQueue($"{ModelResource.VersionsDirectoryUrl}{currentVersion}/{currentVersion}.jar",
                    $"{versionFilePath}.jar");
            }
            if (!_fileManager.FileExist($"{versionFilePath}.json"))
            {
                _addToDownloadQueue($"{ModelResource.VersionsDirectoryUrl}{currentVersion}/{currentVersion}.json",
                    $"{versionFilePath}.json");
            }
            await _downloadFromQueue();
        }

        private void _checkDirectories(string gameDirectory, string currentVersion)
        {
            _checkDirectory(gameDirectory);
            _checkDirectory($"{gameDirectory}versions\\{currentVersion}\\natives");
            _checkDirectory($"{gameDirectory}assets\\objects");
            _checkDirectory($"{gameDirectory}assets\\virtual\\legacy");
            _checkDirectory($"{gameDirectory}libraries");
        }

        private void _checkDirectory(string directory)
        {
            if (!_fileManager.DirectoryExist(directory))
            {
                _fileManager.CreateDirectory(directory);
            }
        }

        private async Task _downloadFromQueue()
        {
            using (var client = new WebClient())
            {
                //client.DownloadFileCompleted += _fileDownloaded;

                foreach (var downloadTuple in _downloadQueue)
                {
                    try
                    {
                        await client.DownloadFileTaskAsync(downloadTuple.Item1, downloadTuple.Item2);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(downloadTuple.Item1);
                        Console.WriteLine(downloadTuple.Item2);
                        throw;
                    }
                    
                }
            }
            _downloadQueue.Clear();
        }

        private void _fileDownloaded(object sender, AsyncCompletedEventArgs asyncCompletedEventArgs)
        {
            //
            // percent = downloadedCount / _downloadQueue.Count
            // Invoke(percent);
            //
        }

        private void _addToDownloadQueue(string url, string path)
        {
            if (_fileManager.FileExist(path))
                return;

            _downloadQueue.Add(new Tuple<Uri, string>(new Uri(url), path));
        }
    }
}