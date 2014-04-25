Meetup Manager
===========

A Cross-Platform app for Meetup.com organizers to help track attendees at events.

Written in C# with ([Xamarin](http://www.xamarin.com))  **Created in Visual Studio and Xamarin Studio**

Open Source Project by ([@JamesMontemagno](http://www.twitter.com/jamesmontemagno)) 

Copyright 2014 ([Refractored LLC](http://www.refractored.com))

## Available for free on:
* Android: Available on [Google Play](https://play.google.com/store/apps/details?id=com.refractored.meetupmanager)
* iPhone: Available on [App Store](https://itunes.apple.com/us/app/meetup-manager-for-organizers/id796213890?ls=1&mt=8)
* Windows Phone 8: Available on [Windows Phone Marketplace](http://www.windowsphone.com/en-us/store/app/meetup-manager-for-organizers/38ef03e9-3dfe-4150-b797-5ec9ed81b8cd)
* Windows 8 (Soon, feel free to fork and start)


## Why did I create this?
As a user group organizer of ([Seattle Mobile .NET Developers Group](http://www.meetup.com/SeattleMobileDevelopers/)) I found our right away that there was an issue with finding out not only how many people attended the event but also who was there. I thought that there had to be a better way of checking people in at the door and the using this data to do giveaways as well. After realizing that Meetup.com had a great restful API that I could tap into I spent my holiday break between Dec 24th and Dec 30th to create Meetup Manager v1.

## How much code is shared?
I have included an "Analysis Project", which will count the shared lines of code. 70%+ of the code is shared between projects. All of the Models, Services, View Models, and tons of helper classes are all found in one single PCL library. The major difference is in both iOS and Android I have some custom views which act as the platform specific code. I am not counting any plugins that handle platform specific code for me since I did not have to write it.  You can find a full breakdown of shared code on ([Google Drive](https://docs.google.com/spreadsheet/ccc?key=0Aj1h7Abl0UIAdExNdHdSb093RDBnSkZ0SWZLSld1MEE&usp=sharing))

## How you can test it. 
I have my own API keys that I ship with, but you can use these test keys for yourself:

ClientId: h0hrbvn9df1d817alnluab6d1s
SecreteId = o9dv5n3uanhrp08fdmas8jdaqb

Just update that code here: https://github.com/jamesmontemagno/MeetupManager/blob/master/MeetupManager.Portable/Services/MeetupService.cs#L51


## What technology is used?
Everything is written in C# with Xamarin with a base PCL library. This project couldn't have been done without the following:

### MvvmCross 
https://github.com/MvvmCross/MvvmCross: an amazing MVVM framework built on top of Xamarin to bring databinding, IoC, and much much more to iOS. Some plugins I use are: File, DownloadCache, Binding, Visibility, SQLite-Community (for local data storage).

### MvvmCross - Settings Plugin
https://www.nuget.org/packages/Mvx.Plugins.Settings/ - I wrote this, so you should probably use it for cross platform settings :)

### Json.NET
https://components.xamarin.com/view/json.net - I use both the NuGet in the PCL and component for iOS for facade linking. One of the most wonderful Json libraries that I simply love. It is used to deserialize all information coming from the meetup.com APIs.

### Xamarin.Auth
https://components.xamarin.com/view/xamarin.auth - Allowed me to integrate in oAuth 2.0 from Meetup.com in under 1 hour. 

### Support v7 AppCompat
https://components.xamarin.com/view/xamandroidsupportv7appcompat - Allowing me to have Meetup Manager have a beautiful user interface al the way back to Android 2.1 with a great actionbar!

### Discreet Notifications
https://components.xamarin.com/view/gcdiscreetnotification - Bringing toast notifications to iOS

### Google Analytics
https://components.xamarin.com/view/googleanalytics - Allowing me to see what devices and OS versions you guys are using.


## License
Licensed under the [Apache License, Version 2.0](http://www.apache.org/licenses/LICENSE-2.0.html)
