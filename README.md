
# Xamarin ShowcaseView 

[![platform](https://img.shields.io/badge/platform-Xamarin.Android-brightgreen.svg)](https://www.xamarin.com/)
[![API](https://img.shields.io/badge/API-10%2B-orange.svg?style=flat)](https://android-arsenal.com/api?level=10s)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![NuGet](https://img.shields.io/nuget/v/xamarin.android.showcaseview.svg?label=NuGet)](https://www.nuget.org/packages/xamarin.android.showcaseview/)
![Build: Passing](https://img.shields.io/badge/Build-Passing-green.svg)

![](https://github.com/DigitalSa1nt/Xamarin.ShowcaseView/blob/master/images/nugetIcon.png) [Nuget Link](https://www.nuget.org/packages/xamarin.android.showcaseview/) 

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

# Useful?

<a href="https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=PFBEH42KW5P84" method="post" target="_top"><img src="https://camo.githubusercontent.com/b8efed595794b7c415163a48f4e4a07771b20abe/68747470733a2f2f7777772e6275796d6561636f666665652e636f6d2f6173736574732f696d672f637573746f6d5f696d616765732f707572706c655f696d672e706e67" alt="Buy Me A Coffee" style="height: auto !important;width: auto !important;" ></a>

 _You know, only if you want to_
