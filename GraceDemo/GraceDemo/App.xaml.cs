using GraceDemo.Ioc;
using GraceDemo.Views;
using Prism;
using Prism.Ioc;
using Prism.Plugin.Popups;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace GraceDemo
{
    public partial class App
    {
        public App() : base() { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override IContainerExtension CreateContainerExtension() =>
            new GraceContainerExtension();

        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync("MainPage?message=Hello%20From%20Prism");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterPopupNavigationService();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<PopupA>();
        }
    }
}
