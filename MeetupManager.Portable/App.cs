/*
 * MeetupManager:
 * Copyright (C) 2013 Refractored LLC: 
 * http://github.com/JamesMontemagno
 * http://twitter.com/JamesMontemagno
 * http://refractored.com
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
//#define MOCK
using Cirrious.CrossCore.IoC;
using Cirrious.CrossCore;
using MeetupManager.Portable.Interfaces.Database;
using MeetupManager.Portable.Services.Database;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;


namespace MeetupManager.Portable
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
			string service = "Service";
			#if MOCK
			service = "ServiceMock";
			#endif

            CreatableTypes()
				.EndingWith(service)
                .AsInterfaces()
                .RegisterAsLazySingleton();

			#if MOCK
			Mvx.RegisterSingleton<IDataService>(()=>new DataService(Mvx.Resolve<ISQLiteConnectionFactory>()));
			#endif
				
			RegisterAppStart<ViewModels.LoginViewModel>();
        }
    }
}