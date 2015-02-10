# Plasma [![Build status](https://ci.appveyor.com/api/projects/status/iqqdm2ymtt0vc81w/branch/master?svg=true)](https://ci.appveyor.com/project/Plasma/plasma/branch/master)

 * What's Plasma?
 * How does it work?
 * Plasmas libraries
 * Getting started
 * Supported Frameworks

 ---

### What's Plasma?

Plasma is an in memory web automation framework for ASP.NET web applications (MVC, WebForms and WebPages - anything that runs in a standard ASP.NET AppDomain).

Using Plasma, you can execute the kind of tests that would otherwise require IIS, Casini, or IIS Express, in memory, hosted inside your test runner. These tests can be run on a build server, run via ReSharper, NCrunch, or your test runner of choice.

### How does it work?

Plasma creates and hosts ASP.NET application domains inside of your test framework, loading your application and all its assemblies - just like IIS would. Because both your tests and your application are then running inside the same process, they can interact with each other.

You can manipulate dependencies, and use the Plasma libraries to make HTTP style requests to your running web app.

The entire ASP.NET application pipeline is executed, returning the raw HTTP responses that would have been otherwise pushed across a TCP connection.

### Plasmas libraries

##### Plasma.Core

Contains the core plasma runtime and app domain spoofing code, but you interact with it through a "client-like" library.

##### Plasma.HttpClient `Introduced in v.2.0`

Provides a builder class to use a standard `System.Net.Http.HttpClient` (available in the package dependency `Microsoft.Net.Http`) to make calls to your application.

##### Plasma.WebDriver

Provides a **partial implementation** of the Selenium [Webdriver](http://code.google.com/p/selenium/?redir=1) interfaces. These provide a useful way of looking up specific page elements and their values.

*Examples for `Plasma.WebDriver` are available on the old  [wiki](https://github.com/jennifersmith/plasma/wiki).*

##### Plasma.Http `Deprecated`

The original (pre-`System.Net.Http.HttpClient`) implementation of "Http-to-Plasma". Will be removed once v.2 stabalises - but maintained for backwards compatibility. **Prefer Plasma.HttpClient.**

Each of these libraries are available as **nuget packages**.

## Getting started

The simplest way to get started is to use `Plasma.HttpClient`. It uses `System.Net.Http.HttpClient` to communicate with your app, and should be very familiar if you've ever done any HTTP in C#.

Install the `Plasma.HttpClient` nuget package

    PM > Install-Package Plasma.HttpClient

You can then write tests that look like this:

```csharp
var client = PlasmaClient.For<MvcApplication>();

var response = await client.GetAsync("/some-path");

Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
Assert.That(response.Content.ReadAsStringAsync().Result, Is.StringContaining("Hello world"));
```

In the above sample, `MvcApplication` is the name of the class behind your `Global.asax` file.
The PlasmaClient helper will detect the location of your webapp assemblies, and load them all into the app-domain automatically. Everything should just work.

The `client` generated is a standard `System.Net.Http.HttpClient` and you can use it as you would regularly, adding HTTP headers, cookies and content. By combining the above snippet with the `CsQuery` package you can use JQuery like selectors over the response body.

## Support

* Plasma.Core `NET2` `NET35`
* Plasma.Http `NET2` `NET35`
* Plasma.WebDriver `NET2` `NET35`
* Plasma.HttpClient `NET45`

**Frameworks**
 * ASP.NET MVC3-5
 * ASP.NET WebForms
 * ASP.NET WebPages
 * WebAPI

**ASP.NET vNext?**

ASP.NET vNext fundamentally changes the way ASP.NET websites are hosted, and involves a ground-up re-write of many core components and frameworks (ASP.NET MVC, WebAPI).  It is unlikely that Plasma will support ASP.NET vNext as in-memory test support via OWIN will be possible using Katana and Microsofts existing Katana based testing libraries.

[See also: Testing WebAPI in memory with Katana and OWIN](http://www.davidwhitney.co.uk/Blog/2015/01/07/testing-an-asp-net-webapi-app-in-memory/).

**JavaScript**

Plasma tests do not execute in a browser, so javascript and single page apps are not supported. It's suggested you **use WebDriver and PhantomJS for these scenarios.**

## See also

* [Contributors](contributors.md)
* [Contributing](contributing.md)
* [Roadmap](roadmap.md)

## Licence

Plasma is distributed under the terms of the [Microsoft Permissive Licence](http://www.microsoft.com/opensource/licenses.mspx#Ms-PL)
