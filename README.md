
# Xamarin ShowcaseView 

[![platform](https://img.shields.io/badge/platform-Xamarin.Android-brightgreen.svg)](https://www.xamarin.com/)
[![API](https://img.shields.io/badge/API-10%2B-orange.svg?style=flat)](https://android-arsenal.com/api?level=10s)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![NuGet](https://img.shields.io/nuget/v/xamarin.android.showcaseview.svg?label=NuGet)](https://www.nuget.org/packages/xamarin.android.showcaseview/)
![Build: Passing](https://img.shields.io/badge/Build-Passing-green.svg)

## Installation

![](https://github.com/DigitalSa1nt/Xamarin.ShowcaseView/blob/master/images/nugetIcon.png)

Simply install the [NuGet package](https://www.nuget.org/packages/Xama.JTPorts.ShowcaseView/) into your Xamarin.Android application and use as below.

This library now supports AndroidX libraries rather than v7 support libraries so it may ask you to install these dependencies in order to carry on using this control.

Package Manager:
> Install-Package Xama.JTPorts.ShowcaseView -Version 1.0.1

.NET CLI:
> dotnet add package Xama.JTPorts.ShowcaseView --version 1.0.1

### Namespace: Xama.JTPorts.ShowcaseView

_Xamarin.Android_ Native showcase view. An easy-to-use customizable show case view with circular reveal animation, ported from [FancyShowCaseView](https://github.com/faruktoptas/FancyShowCaseView) by [Faruk Toptaş](https://github.com/faruktoptas)

This is a ported build, converted from Java to C# for use with the Xamarin MonoFramework. There are only a couple of new additions from the original library currently.

# Features

## Ported Functionality
- Circular reveal animation (API Level 21+).
- Custom Background colors with opaque variances.
- Circle and Rounded Rectangle focus shapes.
- Custom title styles and position.
- Custom view inflation.
- Custom enter/exit animations.
- Chaining multiple Showcase view instances.
- Showing only one time.

## Additional Functionality
- Auto move-on for showcase queues.
- Auto dismiss for showcases.

# Customary sample GIF

![!gif](https://github.com/DigitalSa1nt/Xamarin.ShowcaseView/blob/master/images/Sample.gif)

<br>

# Basic usage
```
          ShowCaseView showcase = new ShowCaseView.Builder()
                .Context(this)
                .CloseOnTouch(true)
                .FocusOn(ControlToFocusOn)
                .BackgroundColor(Color.DarkRed)
                .FocusBorderColor(Color.White)
                .FocusBorderSize(15)
                .Title("Showcase text")
                .FocusCircleRadiusFactor(1.5)
                .Build();
                
           showcase.Show();
```

# Contribution

More than happy for people to raise issues, or submit pull requests on adjustments, optimisations or improvements to the existing port. It's been migrated over to AndroidX support libraries so there's room to improve the ways the UI animations are created potentially.

# Useful?

<a href="https://www.buymeacoffee.com/JTT" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-red.png" alt="Buy Me A Coffee" tyle="height: 41px !important;width: 174px !important;box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;-webkit-box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;" ></a>

_You know, only if you want to._
