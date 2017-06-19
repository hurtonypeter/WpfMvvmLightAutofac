/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:WpfMvvmLightAutofac.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using Autofac;
using Autofac.Extras.CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using WpfMvvmLightAutofac.Bootstrap;
using WpfMvvmLightAutofac.Model;

namespace WpfMvvmLightAutofac.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            if (!ServiceLocator.IsLocationProviderSet)
            {
                RegisterServices(registerFakes: true);
            }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }

        public static void RegisterServices(bool registerFakes = false)
        {
            var builder = new ContainerBuilder();

            // you only need this if-clause if you'd like to use design-time data which is only supported on XAML-based platforms
            if (ViewModelBase.IsInDesignModeStatic || registerFakes)
            {
                builder.RegisterModule<AutofacModule>();
            }
            else
            {
                // just use this one if you don't use design-time data
                builder.RegisterModule<AutofacModule>();
            }

            // viewmodel registrations
            builder.RegisterType<MainViewModel>().AsSelf().InstancePerLifetimeScope();

            var container = builder.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
        }
    }
}