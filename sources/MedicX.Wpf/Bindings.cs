﻿// MedicX
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
using DustInTheWind.MedicX.RequestBusModel;
using DustInTheWind.MedicX.Wpf.Commands;
using Ninject.Modules;

namespace DustInTheWind.MedicX.Wpf
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<MedicXApplication>().ToSelf().InSingletonScope();
            Bind<RequestBus>().ToSelf().InSingletonScope();
            Bind<IRequestHandlerFactory>().To<NinjectRequestHandlerFactory>();
            Bind<ISaveConfirmationQuestion>().To<SaveConfirmationQuestion>();
        }
    }
}