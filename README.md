# Steven Volckaert's Enterprise Library [![Build status](https://ci.appveyor.com/api/projects/status/oywm2xiccjbfmj6r?svg=true)](https://ci.appveyor.com/project/stevenvolckaert/enterprise-library)

**Steven Volckaert's Enterprise Library** is a collection of .NET *class libraries* that contain reusable software
components designed to assist software developers with writing less and semantically more meaningful code.

In practice, the class libraries extend on types available in the various namespaces of the
[Framework Class Library (FCL)][1] (CoreFX for the .NET Standard 1.5 target). Therefore, it contains no
application-specific code and has no depencencies to third-party .NET libraries or frameworks.

## Library contents

Steven Volckaert's Enterprise Library comprises a collection of class libraries.

| Assembly name | Description |
|---------------|-------------|
| `StevenVolckaert.Core` | Contains commonly used functions extending types defined in the `System` namespace. |

## NuGet

Enterprise Library is available on NuGet.

* [StevenVolckaert.Core](https://www.nuget.org/packages/StevenVolckaert.Core/) (.NETFramework 3.5, .NETFramework 4.5.2,
  .NETStandard 1.5)

## Design

* The library is built with modularity and [separation of concerns][3] in mind.
* Class libraries, namespaces and types are named in accordance with the [.NET Framework Design Guidelines][4].

## Forking and compiling from source

One of the frameworks the library targets is `.NET Framework 3.5`, which is not installed by default on Windows 8,
Windows 8.1 or Windows 10. Instead, you need to install the framework manually.
[Installing the .NET Framework 3.5 on Windows 8, Windows 8.1 and Windows 10][6] explains how to do this.

Using this method didn't work for me on Windows 10, but I managed to install .NET Framework 3.5 using the `DISM.exe`
tool. See [Can't install .NET 3.5 on Windows 10](http://superuser.com/q/946988/319367) on StackExchange for more
information.

Alternatively, you can remove the .NET Framework 3.5 build target: Simply remove it from the `project.json`.

## License

Steven Volckaert's Enterprise Library is licensed under the [MIT license](LICENSE).

## External links

* [Understanding .NET Core, NETStandard, .NET Core applications and ASP.NET Core][5]. Lock, Andrew. May 11, 2016.
  Retrieved December 10, 2016.
* [.NET Standard Library][2]. Retrieved December 11, 2016.

[1]: https://msdn.microsoft.com/en-us/library/gg145045(v=vs.110).aspx
[2]: https://docs.microsoft.com/en-us/dotnet/articles/standard/library
[3]: https://en.wikipedia.org/wiki/Separation_of_concerns
[4]: https://msdn.microsoft.com/en-us/library/ms229042(v=vs.110).aspx
[5]: http://andrewlock.net/understanding-net-core-netstandard-and-asp-net-core/
[6]: https://msdn.microsoft.com/en-us/library/hh506443%28v=vs.110%29.aspx
