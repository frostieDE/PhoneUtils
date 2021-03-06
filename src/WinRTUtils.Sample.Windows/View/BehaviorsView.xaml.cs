﻿using System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WinRTUtils.Sample.Common;
using WinRTUtils.Sample.Messages;
using WinRTUtils.Sample.ViewModel;

namespace WinRTUtils.Sample.View
{
    public sealed partial class BehaviorsView : Page
    {
        private NavigationHelper navigationHelper;

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        private bool IsDialogOpen { get; set; }

        public BehaviorsView()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);

            var vm = (BehaviorsViewModel)DataContext;

            Loaded += (s, e) =>
            {
                vm.Messenger.Register<SampleMessage>(this, async msg =>
                {
                    if (!IsDialogOpen)
                    {
                        var dialog = new MessageDialog("search button pressed", "command execution");

                        IsDialogOpen = true;
                        await dialog.ShowAsync();
                        IsDialogOpen = false;
                    }
                });
            };

            Unloaded += (s, e) =>
            {
                vm.Messenger.Unregister(this);
            };
        }

        #region NavigationHelper

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}
