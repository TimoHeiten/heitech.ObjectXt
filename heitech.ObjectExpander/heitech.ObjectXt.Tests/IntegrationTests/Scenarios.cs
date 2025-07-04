using FluentAssertions;
using heitech.ObjectXt.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace heitech.ObjectXt.Tests.IntegrationTests;

/// <summary>
/// Kinda to show the cases this should be used for ;)
/// </summary>
[TestClass]
public sealed class Scenarios
{
    private MyObj sut = new();

    [TestInitialize]
    public void Setup()
        => ObjectExtender.Entry();

    [TestMethod]
    [DoNotParallelize]
    public void Add_Action_Func_Call_Both_And_Call_Keys_That_Do_Not_Exist()
    {
        // Arrange
        ObjectExtenderConfig.IgnoreExceptions(true);
        var counter = 0;
        sut.RegisterAction("count", () => ++counter);
        sut.RegisterAction("countdown", () => --counter);
        sut.RegisterFunc("getCounter", () => counter);
        sut.RegisterFunc<string, int, int>("getCounterModulo", (modulo) => counter % modulo);
        
        // Act
        sut.Call("count");
        sut.Call("count");
        sut.Call("count");
        sut.Call("count");
        sut.Call("countdown");
        var counterval = sut.Invoke<string, int>("getCounter");
        var moduloval = sut.Invoke<string, int, int>("getCounterModulo", 2);
        
        sut.Call("dont exist");
        
        // Assert
        counterval.Should().Be(3);
        moduloval.Should().Be(1);
    }

    [TestMethod]
    public void AddAtribute_On_Extender_Can_Be_Accessed_Via_indexer()
    {
        // Arrange
        var item = heitech.ObjectXt.AttributeExtension.AttributeExtenderFactory.Create<string>();
        item["prop1"] = "value1";
        
        // Act
        var result = item["prop1"];
        
        // Assert
        result.Should().Be("value1");
    }

    private sealed record MyObj : IMarkedExtendable;
}