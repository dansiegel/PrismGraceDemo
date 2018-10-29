using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraceDemo.ViewModels
{
    public class PopupAViewModel : BindableBase, INavigatedAware
    {
        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Message = parameters.GetValue<string>("message");
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            parameters.Add("message", "Hello from PopupA");
        }
    }
}
