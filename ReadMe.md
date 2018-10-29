# Prism Ioc Demo

The Prism team cannot support every container out there. Looking at Ioc Performance Benchmarks you may feel you want to use a non-supported container such as Grace. While Grace's [performance metrics](https://github.com/danielpalme/IocPerformance#basic-features) are fantastic, it currently does not have enough of a following to justify adding it as an official container.

## Popup Plugin Compatible

Because of the container abstraction, the Popup Plugin remains fully compatible with our application while only ever providing that single class.

## Container Extension

Prism 7's Ioc Abstractions make supporting new containers extremely easy. As you'll see from this demo app, we only have to add a single class implementing IContainerExtension.

```cs
public class GraceContainerExtension : IContainerExtension<IInjectionScope>
{
    public GraceContainerExtension()
        : this(new DependencyInjectionContainer())
    {
    }

    public GraceContainerExtension(IInjectionScope injectionScope)
    {
        Instance = injectionScope;
    }

    public IInjectionScope Instance { get; }

    public bool SupportsModules => true;

    public void FinalizeExtension() { }

    public void Register(Type from, Type to) =>
        Instance.Configure(c => c.Export(to).As(from));

    public void Register(Type from, Type to, string name) =>
        Instance.Configure(c => c.Export(to).AsKeyed(from, name));

    public void RegisterInstance(Type type, object instance) =>
        Instance.Configure(c => c.ExportInstance(instance).As(type));

    public void RegisterSingleton(Type from, Type to) =>
        Instance.Configure(c => c.Export(to).As(from).Lifestyle.Singleton());

    public object Resolve(Type type) =>
        Instance.Locate(type);

    public object Resolve(Type type, string name) =>
        Instance.Locate(type, withKey: name);

    public object ResolveViewModelForView(object view, Type viewModelType)
    {
        Page page = null;

        switch(view)
        {
            case Page viewAsPage:
                page = viewAsPage;
                break;
            case VisualElement visualElement:
                page = GetPage(visualElement);
                break;
            default:
                return Instance.Locate(viewModelType);
        }

        var navService = Instance.Locate<INavigationService>(withKey: PrismApplicationBase.NavigationServiceName);
        ((IPageAware)navService).Page = page;
        return Instance.Locate(viewModelType, new[] { navService });
    }

    private Page GetPage(Element visualElement)
    {
        switch(visualElement.Parent)
        {
            case Page page:
                return page;
            case null:
                return null;
            default:
                return GetPage(visualElement.Parent);
        }
    }
}
```

Instead of inheriting from a PrismApplication we can inherit directly from PrismApplicationBase and pass back our GraceContainerExtension for `CreateContainerExtension()`.

```cs
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
        containerRegistry.RegisterForNavigation<MainPage>();
    }
}
```