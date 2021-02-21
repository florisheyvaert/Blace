# BLACE
ACE editor for Blazor

This is a minimalistic implementation of the ACE editor (https://ace.c9.io/) as a Blazor component. 

Currently supported: 
* open/close multiple files
* save file (ctrl + s)
* select a theme/mode.

In the future this can be expanded to: 
* configurable shortcuts
* splitted view of files
* multiple actions (close all, close others, ...)

Suggestions are welcome.

## How to use
### Install NuGet package Blace

Search for package 'Blace' or install through Package Manager Console:
```
Install-Package Blace -Version 1.0.10
```
Source: https://www.nuget.org/packages/Blace/

### Reference JS & CSS
Add following js and css line to _Host.cshtml.

```html
<link href="/_content/Blace/css/site.css" rel="stylesheet" />
```

```html
<script src="/_content/Blace/ace/ace.js"></script>
```

### Add namespace to _Imports.razor
Add following line to imports file:
```csharp
@using Blace.Components 
```
* Note that this step is optional

### Use component
Use following line to create the editor component:

```csharp
<Blace.Components.Editor Files="Files" />
```

The property Files is a list of type BaseEditorFile. You should create a new class which inherits from this base class.
Here you can specify how to load the content of the file, as what to so if saving the file. An example is listed below.

```csharp
    public class TestEditorFile : BaseEditorFile
    {
        public TestEditorFile(string name)
        {
            Name = name;
        }

        public override Task<string> LoadContent()
        {
            return Task.FromResult("This is the content");
        }

        public override Task<bool> SaveContent()
        {
            return Task.FromResult(true);
        }
    }
```
