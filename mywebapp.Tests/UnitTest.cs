using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;
using mywebapp.ServiceInterface;
using mywebapp.ServiceModel;

namespace mywebapp.Tests;

public class UnitTest
{
    private readonly ServiceStackHost appHost;

    public UnitTest()
    {
        appHost = new BasicAppHost().Init();
        appHost.Container.AddTransient<MyServices>();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => appHost.Dispose();

    [Test]
    public void Can_call_MyServices()
    {
        var service = appHost.Container.Resolve<MyServices>();

        var response = (HelloResponse)service.Any(new Hello { Name = "World" }).Result;

        Assert.That(response.Result, Is.EqualTo("Hello, World!"));
    }
}