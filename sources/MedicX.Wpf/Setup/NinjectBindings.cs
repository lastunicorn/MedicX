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

using DustInTheWind.MedicX.Application;
using DustInTheWind.MedicX.Application.ExitApplication;
using DustInTheWind.MedicX.Domain.DataAccess;
using DustInTheWind.MedicX.Persistence;
using DustInTheWind.MedicX.RequestBusModel;
using EventBusModel;
using MedicX.Wpf.UI.Areas.Main.Commands;
using Ninject.Modules;

namespace DustInTheWind.MedicX.Wpf.Setup
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<RequestBus>().ToSelf().InSingletonScope();
            Bind<EventAggregator>().ToSelf().InSingletonScope();
            Bind<IRequestHandlerFactory>().To<NinjectRequestHandlerFactory>();
            Bind<MedicXApplication>().ToSelf().InSingletonScope();
            Bind<ISaveConfirmationQuestion>().To<SaveConfirmationQuestion>();
            Bind<IUnitOfWorkBuilder>().To<UnitOfWorkBuilder>();
        }
    }
}