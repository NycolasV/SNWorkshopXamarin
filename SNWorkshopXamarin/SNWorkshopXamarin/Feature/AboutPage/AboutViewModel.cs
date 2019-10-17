using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace SNWorkshopXamarin.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Sobre o Xamarin";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
            OpenSpaceCommand = new Command(() => Device.OpenUri(new Uri("https://eusou.space/")));
        }

        public ICommand OpenWebCommand { get; }
        public ICommand OpenSpaceCommand { get; }
    }
}