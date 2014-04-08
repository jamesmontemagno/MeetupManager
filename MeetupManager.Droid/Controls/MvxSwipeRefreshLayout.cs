/* MeetupManager:
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

using System;
using Android.Support.V4.Widget;
using Android.Util;
using System.Windows.Input;
using Android.Content;

namespace MeetupManager.Droid.Controls
{
	public class MvxSwipeRefreshLayout : SwipeRefreshLayout
	{
		/// <summary>
		/// Gets or sets the refresh command.
		/// </summary>
		/// <value>The refresh command.</value>
		public ICommand RefreshCommand { get; set;}

		public MvxSwipeRefreshLayout(Context context, IAttributeSet attrs)
			: base(context, attrs)
		{
			Init ();
		}

		public MvxSwipeRefreshLayout(Context context)
			: base(context)
		{
			Init ();
		}

	
		private void Init()
		{
			//This gets called when we pull down to refresh to trigger command
			this.Refresh += (object sender, EventArgs e) => {
				var command = RefreshCommand;
				if (command == null)
					return;

				command.Execute (null);
			};
		}
	}
}

