# Steven Volckaert's Enterprise Library

**Steven Volckaert's Enterprise Library** is a collection of .NET *class libraries* that contain reusable software
components designed to assist software developers with writing less and semantically more meaningful code.

In practice, the class libraries extend on types available in the various namespaces of the
[.NET Framework Class Library (FCL)][1]. Therefore, it contains no application-specific code and has no depencencies
to third-party .NET libraries or frameworks.

## NuGet

Enterprise Library will be available on [NuGet](https://www.nuget.org/) later.

## Library contents

The library comprises a collection of class libraries:

| Assembly name | Description |
|---------------|-------------|
| `StevenVolckaert.Core` | Contains commonly used functions extending types defined in the `System` namespace. |

All libraries target multiple .NET platforms:

* .NET Framework 4.5
* .NET Framework 4.6.2
* [.NET Standard 1.6][2]

## Design

* The library is built with modularity and [separation of concerns][3] in mind.
* Class libraries, namespaces and types are named in accordance with the [.NET Framework Design Guidelines][4].

## License

Steven Volckaert's Enterprise Library is licensed under the [MIT license](LICENSE).

## External links

* [Understanding .NET Core, NETStandard, .NET Core applications and ASP.NET Core][2]. Lock, Andrew. May 11, 2016.
  Retrieved December 10, 2016.

[1]: https://msdn.microsoft.com/en-us/library/gg145045(v=vs.110).aspx
[2]: https://docs.microsoft.com/en-us/dotnet/articles/standard/library
[3]: https://en.wikipedia.org/wiki/Separation_of_concerns
[4]: https://msdn.microsoft.com/en-us/library/ms229042(v=vs.110).aspx
[5]: http://andrewlock.net/understanding-net-core-netstandard-and-asp-net-core/
