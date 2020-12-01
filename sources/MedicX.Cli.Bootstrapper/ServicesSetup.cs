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

using DustInTheWind.MedicX.CommandApplication.AddMedic;
using DustInTheWind.MedicX.CommandApplication.SearchMedic;
using DustInTheWind.MedicX.Domain;
using DustInTheWind.MedicX.Domain.DataAccess;
using DustInTheWind.MedicX.Persistence;
using DustInTheWind.MedicX.RequestBusModel;
using MedicX.Cli.Presentation;
using MedicX.Cli.Presentation.Views;
using Ninject.Modules;

namespace DustInTheWind.MedicX.Cli.Bootstrapper
{
    public class ServicesSetup : NinjectModule
    {
        public override void Load()
        {
            Bind<RequestBus>().ToSelf().InSingletonScope();
            Bind<IRequestHandlerFactory>().To<RequestHandlerFactory>();
            Bind<MedicXApplication>().ToSelf().InSingletonScope();
            Bind<IUnitOfWorkBuilder>().To<UnitOfWorkBuilder>();
            Bind<ICommandFactory>().To<CommandFactory>();
            Bind<ProjectRepository>().ToSelf().InSingletonScope();
            Bind<IApplicationConfig>().To<ApplicationConfig>();

            Bind<IMedicView>().To<MedicView>();
            Bind<IDisplayMedicsView>().To<DisplayMedicsView>();
        }
    }
}