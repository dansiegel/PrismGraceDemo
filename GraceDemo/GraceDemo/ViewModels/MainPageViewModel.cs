using Prism.Commands;
using Prism.Common;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace GraceDemo.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigatedAware
    {
        private INavigationService _navigationService { get; }
        private IPageDialogService _pageDialogService { get; }

        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            HasNavigationService = navigationService != null && 
                ((IPageAware)navigationService).Page != null;

            ShowAlert = new DelegateCommand(() => _pageDialogService.DisplayAlertAsync("Alert", "This is an alert from Prism's IPageDialogService", "Ok"));
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public bool HasNavigationService { get; }

        public DelegateCommand ShowAlert { get; }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Message = parameters.GetValue<string>("message");
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            Message = "Left Main Page";
        }
    }
}
