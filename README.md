# WinRTUtils

A collection of useful behaviors, commands and converters which might be essential for MVVM apps.

## Features

### Actions

* `OpenFlyoutAction`

### Behaviors

* `BindKeyToCommandBehavior`: a behavior that executes a given command if a certain key is pressed
* `BindTextboxBehavior`: enables two-mode binding for `Textbox.Text` property
* `FocusTextboxBehavior`: a behavior that sets the focus to the attached `TextBox` after the Loaded event is fired
* `SelectionModeBehavior`: emulates a `IsSelectionEnabled` property for `ListView`s
* `UpdateSelectedItemsBehavior`: a behavior that automatically synchronises selected items of `ListView`s to a given List (inside the ViewModel)
* `ProgressIndicatorBehavior`: a behavior which makes the ProgressIndicator accessible through XAML (Windows Phone only)

### Collections

* `RangeObservableCollection`: an `ObservableCollection` that supports adding and removing ranges. Caution: this collection is NOT able to be data bound (so far)
* `SortedObservableCollection`: an `ObservableCollection` which automatically sorts items to a given strategy

### Commands

* `ItemClickCommand`: adds command binding for the `ListView.ItemsClick` event

### Converters

* `BooleanToVisibilityConverter`
* `InverseBooleanToVisibilityConverter`
* `InverseNullToVisibilityConverter`
* `NullToVisibilityConverter`
* `StringFormatConverter`
* `ToLowercaseConverter`
* `ToUppercaseConverter`
* `ValueConverterGroup`: emulates a MultiValueConverter

### Messages

These are useful messages to be used with MvvmLights `Messenger` implementation.

* `CallbackMessageBase`: all messages with a callback can inherit from this to get basic functionality as a `SuccessCallback` and `CancelCallback`
* `NavigateBackMessage`
* `NavigateMessage`: a message to be used for navigation between views

## Installation

1. Install package using NuGet:

	`Install-Package WinRTUtils`
	
2. Reference "Behaviors SDK (XAML)" to your project. This SDK comes with Visual Studio.

## Documentation

Currently only available as comments in the source code. You may take a look at the sample project which is a universal project, so you have samples for both Windows and Windows Phone 8.1.

## Contribute!

Feel free to contribute to this project :)
