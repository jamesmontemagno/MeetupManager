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
Everything is written in C# with Xamarin. I use the amazing MvvmCross framework to bring databinding, IoC, and more to Android and iOS. I used a PCL with Profile 7 to target multiple platforms. In the PCL I use multiple MvvmCross plugins including my own Settings plugin as well. Json.Net is used for all deserialization of Meetup.com's APIs and local data is stored in a SQLite.net database. On Android I am using Google Analytics component, Support v7 AppCompat component, AndHUD, and Xamarin.Auth for oAuth 2.0.

## License
Licensed under the [Apache License, Version 2.0](http://www.apache.org/licenses/LICENSE-2.0.html)
