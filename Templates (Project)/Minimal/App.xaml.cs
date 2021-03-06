﻿using System;
using Windows.UI.Xaml;
using System.Threading.Tasks;
using Sample.Services.SettingsServices;
using Windows.ApplicationModel.Activation;
using Template10.Mvvm;
using Template10.Common;

namespace Sample
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    sealed partial class App : Template10.Common.BootStrapper
    {
        public App()
        {
            InitializeComponent();
            // SplashFactory = (e) => new Views.Splash(e);

            #region App settings

            var _settings = SettingsService.Instance;
            RequestedTheme = _settings.AppTheme;
            CacheMaxDuration = _settings.CacheMaxDuration;
            ShowShellBackButton = _settings.UseShellBackButton;

            #endregion
        }

        public override Task OnPrelaunchAsync(IActivatedEventArgs args, out bool continueStartup)
        {
            // TODO (non-ui)
            continueStartup = false;
            return Task.CompletedTask;
        }

        public override void OnResuming(object s, object e, AppExecutionState previousExecutionState)
        {
            if (previousExecutionState == AppExecutionState.Prelaunch)
            {
                // TODO (complete ui)
            }
        }

        // runs only when not restored from state
        public override Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            NavigationService.Navigate(typeof(Views.MainPage));
            return Task.CompletedTask;
        }

        // hide and show busy dialog
        public static void SetBusy(bool busy, string text = null)
        {
            WindowWrapper.Current().Dispatcher.Dispatch(() =>
            {
                var instance = Current as App;
                var control = (instance.ModalContent = (instance.ModalContent ?? new Views.Busy())) as Views.Busy;
                control.IsBusy = instance.ModalDialog.IsModal = busy;
                control.BusyText = text;
            });
        }
    }
}
