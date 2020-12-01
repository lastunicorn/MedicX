// MedicX
// Copyright (C) 2017-2018 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Reflection;
using DustInTheWind.MedicX.GuiApplication;
using DustInTheWind.MedicX.GuiApplication.ApplicationInitialization;
using DustInTheWind.MedicX.RequestBusModel;
using DustInTheWind.MedicX.Wpf.UI;
using DustInTheWind.MedicX.Wpf.UI.Areas.Main.ViewModels;
using DustInTheWind.MedicX.Wpf.UI.Areas.Main.Views;
using Ninject;

namespace DustInTheWind.MedicX.Wpf.Bootstrapper
{
    internal class Bootstrapper
    {
        private IKernel kernel;

        public void Run()
        {
            kernel = new StandardKernel();

            ConfigureServices();
            ConfigureRequestBus();
            LoadProject();
            RunUiApplication();
        }

        private void ConfigureServices()
        {
            kernel.Load(Assembly.GetExecutingAssembly());
        }

        private void ConfigureRequestBus()
        {
            RequestBus requestBus = kernel.Get<RequestBus>();
            RequestBusConfig.Configure(requestBus);
        }

        private void LoadProject()
        {
            AsyncUtil.RunSync(() =>
            {
                RequestBus requestBus = kernel.Get<RequestBus>();
                ApplicationInitializationRequest request = new ApplicationInitializationRequest();

                return requestBus.ProcessRequest(request);
            });
        }

        private void RunUiApplication()
        {
            MainWindow mainWindow = kernel.Get<MainWindow>();
            MainViewModel mainViewModel = kernel.Get<MainViewModel>();
            mainWindow.DataContext = mainViewModel;

            System.Windows.Application.Current.MainWindow = mainWindow;
            System.Windows.Application.Current.MainWindow?.Show();
        }
    }
}