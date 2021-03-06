﻿using Grace.DependencyInjection;
using Prism;
using Prism.Common;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using Xamarin.Forms;

namespace GraceDemo.Ioc
{
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
                case BindableObject bindable:
                    page = bindable.GetValue(ViewModelLocator.AutowirePartialViewProperty) as Page;
                    break;
                default:
                    return Instance.Locate(viewModelType);
            }

            var navService = Instance.Locate<INavigationService>(withKey: PrismApplicationBase.NavigationServiceName);
            ((IPageAware)navService).Page = page;
            return Instance.Locate(viewModelType, new[] { navService });
        }
    }
}
