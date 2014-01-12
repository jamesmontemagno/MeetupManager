Meetup Manager
===========

A Cross-Platform app for Meetup.com organizers to help track attendees at events.

Written in C# with ([Xamarin](http://www.xamarin.com))  **Created in Visual Studio and Xamarin Studio**

Open Source Project by ([@JamesMontemagno](http://www.twitter.com/jamesmontemagno)) 

Copyright 2014 ([Refractored LLC](http://www.refractored.com))

## Available for free on:
* ([Android](https://play.google.com/store/apps/details?id=com.refractored.meetupmanager))
* iPhone (Soon)


## Why did I create this?
As a user group organizer of ([Seattle Mobile .NET Developers Group](http://www.meetup.com/SeattleMobileDevelopers/)) I found our right away that there was an issue with finding out not only how many people attended the event but also who was there. I thought that there had to be a better way of checking people in at the door and the using this data to do giveaways as well. After realizing that Meetup.com had a great restful API that I could tap into I spent my holiday break between Dec 24th and Dec 30th to create Meetup Manager v1.

## How much code is shared?
To be honest 90%+ of the code will be shared between all platforms. While currently only the Android version is complete all of the Models, Services, View Models, and tons of helper classes are all found in one single PCL library. I should be able to pull in the iOS UI in another week. (I am more comfortable with Android which is why I started there)

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
