﻿using System.Collections.Generic;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using MCLauncher.Model;
using MCLauncher.UI;
using MCLauncher.UI.Messages;
using NSubstitute;
using NUnit.Framework;

namespace Tests.UI
{
    [TestFixture]
    public class LauncherViewModelTests
    {
        [SetUp]
        public void SetUp()
        {
            _profileList = new List<string>()
            {
                "lastProfile",
                "profile1"
            };

            _launcherModel = Substitute.For<ILauncherModel>();
            _launcherModel.GetLastProfile().Returns("lastProfile");
            _launcherModel.GetProfiles().Returns(_profileList);
        }

        private List<string> _profileList;
        private ILauncherModel _launcherModel;

        [Test]
        public void EditProfile_OpenProfileEditingWindow()
        {
            var vm = new LauncherViewModel(_launcherModel);
            vm.EditProfile.Execute(null);

            _launcherModel.Received().OpenEditProfileWindow();
        }

        [Test]
        public void NewProfile_OpenProfileCreatingWindow()
        {
            var vm = new LauncherViewModel(_launcherModel);
            vm.NewProfile.Execute(null);

            _launcherModel.Received().OpenNewProfileWindow();
        }

        [Test]
        public void Profiles_Loaded_Init()
        {
            var vm = new LauncherViewModel(_launcherModel);
            Assert.AreEqual(vm.Profiles, _profileList);
        }

        [Test]
        public void Progress_EqualsMessageProgress_RecievedInstallProgressMessage()
        {
            var vm = new LauncherViewModel(_launcherModel);
            Messenger.Default.Send(new InstallProgressMessage(50));

            Assert.AreEqual(vm.Progress, 50);
        }

        [Test]
        public void ProgressBarVisibility_Collapsed_ProgressEquals100()
        {
            var vm = new LauncherViewModel(_launcherModel);
            Messenger.Default.Send(new InstallProgressMessage(100));

            Assert.AreEqual(vm.ProgresBarVisibility, Visibility.Collapsed);
        }

        [Test]
        public void ProgressBarVisibility_Visible_ProgressLess100()
        {
            var vm = new LauncherViewModel(_launcherModel);
            Messenger.Default.Send(new InstallProgressMessage(50));

            Assert.AreEqual(vm.ProgresBarVisibility, Visibility.Visible);
        }

        [Test]
        public void RefreshProfiles_UpdateProfiles_RecievedProfilesChangedMessage()
        {
            var vm = new LauncherViewModel(_launcherModel);
            vm.Profiles.Clear();
            Messenger.Default.Send(new ProfilesChangedMessage());

            Assert.IsNotEmpty(vm.Profiles);
        }

        [Test]
        public void Start_ExecuteStartGame()
        {
            var vm = new LauncherViewModel(_launcherModel);
            vm.Start.Execute(null);

            _launcherModel.Received().StartGame();
        }

        [Test]
        public void Status_UpdateStatus_RecievedStatusMessage()
        {
            var vm = new LauncherViewModel(_launcherModel);
            Messenger.Default.Send(new StatusMessage("testStatus"));

            Assert.AreEqual(vm.Status, "testStatus");
        }
    }
}