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
using System.Windows;
using DustInTheWind.MedicX.Application.ExitApplication;
using DustInTheWind.MedicX.Application.GetCurrentProject;
using DustInTheWind.MedicX.Application.GetCurrentProjectStatus;
using DustInTheWind.MedicX.Application.LoadProject;
using DustInTheWind.MedicX.Application.SaveProject;
using DustInTheWind.MedicX.Application.SetCurrentItem;
using DustInTheWind.MedicX.Domain;
using DustInTheWind.MedicX.Domain.Entities;
using DustInTheWind.MedicX.RequestBusModel;
using DustInTheWind.MedicX.Wpf.Areas.Main.ViewModels;
using DustInTheWind.MedicX.Wpf.Areas.Main.Views;
using Ninject;

namespace DustInTheWind.MedicX.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private IKernel kernel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ConfigureContainer();
            ConfigureRequestBus();

            LoadProject();

            Current.MainWindow = CreateMainWindow();
            Current.MainWindow?.Show();
        }

        private void LoadProject()
        {
            AsyncUtil.RunSync(() =>
            {
                RequestBus requestBus = kernel.Get<RequestBus>();
                LoadProjectRequest request = new LoadProjectRequest
                {
                    FileName = "medicx.zmdx"
                };

                return requestBus.ProcessRequest<LoadProjectRequest, MedicXProject>(request);
            });
        }

        private void ConfigureContainer()
        {
            kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
        }

        private void ConfigureRequestBus()
        {
            RequestBus requestBus = kernel.Get<RequestBus>();

            requestBus.Register<LoadProjectRequest, LoadProjectRequestHandler>();
            requestBus.Register<SaveProjectRequest, SaveProjectRequestHandler>();
            requestBus.Register<GetCurrentProjectRequest, GetCurrentProjectRequestHandler>();
            requestBus.Register<GetCurrentProjectStatusRequest, GetCurrentProjectStatusRequestHandler>();
            requestBus.Register<ExitApplicationRequest, ExitApplicationRequestHandler>();
            requestBus.Register<SetCurrentItemRequest, SetCurrentItemRequestHandler>();
        }

        private Window CreateMainWindow()
        {
            MainWindow mainWindow = kernel.Get<MainWindow>();
            mainWindow.DataContext = kernel.Get<MainViewModel>();
            return mainWindow;
        }
    }
}