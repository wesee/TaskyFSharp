
Tasky F
=======

Introduction
------------

The current version converts the Droid version of Tasky Pro to a hybrid version of Android app in C# and F# using Xamarin Studio.  Only the UI code was converted to F#, the rest (BL, DAL, DL) remain in C#.


Background
----------

I was browsing Xamarin portal and found out that Xamarin supports F#.  I am new to F# and want to try it out.  Instead of converting everything to F#, I was wondering if I can reuse part of the C# as a library and convert only the UI part.  I googled and found a C# console app which calls a middle layer which coded in F# and this middle layer reuses some C# codes.  

Based on that I wrote a F# console app which calls the middle F# layer.  After that, I continue to convert the Tasky Pro Android app using F#.


Code and Xamarin Studio
-----------------------




Screenshots
-----------





Authors
-------
Vincent Wong



Section below is the original version.



Tasky Pro
=========

TaskyPro is a simple cross-platform todo/task application sample that allows
you to track todo tasks. It illustrates proper application architecture
layering and uses a common code base for the Business Layer, Data Access
Layer, and Data Layer layers. It then separates out the User
Interface and Application Layer into the appropriate device-applications.

The application runs on iOS, Android and Windows Phone 7 with a set of 
common classes shared across all three platforms. Open the TaskyMD_Mac.sln
solution to see the iOS and Android apps on Mac OS-X and use the 
TaskyVS.sln with Visual Studio to see the Android and Windows Phone apps.

![screenshot](https://github.com/xamarin/mobile-samples/raw/master/TaskyPro/Screenshots/all-small.png "iOS, Android and Windows Phone")

NOTE: it also supports some basic iOS localization to [Spanish](https://github.com/xamarin/mobile-samples/raw/master/TaskyPro/Screenshots/IOS/03-detail_spanish.png) and [Japanese](https://github.com/xamarin/mobile-samples/raw/master/TaskyPro/Screenshots/iOS/04-detail_japanese.png).

Xamarin.Forms Version
---------------------
The equivalent app written with [Xamarin.Forms](http://xamarin.com/forms) is called [Todo](https://github.com/xamarin/xamarin-forms-samples/tree/master/Todo).

Authors
-------

Bryan Costanich, Craig Dunn
