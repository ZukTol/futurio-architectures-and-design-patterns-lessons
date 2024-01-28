using Lesson_1_2.Commands;
using Lesson_1_2.Contracts;
using Moq;
using System.Numerics;

namespace Lesson_1_2.Tests;

public class RotateTests
{
    [Theory]
    [InlineData(1f, 0f, Math.PI, -1f, 0f)]
    [InlineData(0f, 1f, Math.PI, 0f, -1f)]
    [InlineData(1f, 0f, Math.PI/4, 0.707106f, 0.707106f)]
    [InlineData(1f, 0f, Math.PI/2, 0f, 1f)]
    public void Executr_CorrectData_Ok(float x1, float y1, float angle, float x2, float y2)
    {
        var sut = new Rotate();
        var movableMock = new Mock<IRotatable>();
        movableMock.SetupGet(x => x.AngularVelocityRad).Returns(angle);
        movableMock.SetupProperty(x => x.Orientation, new Vector2(x1, y1));

        sut.Execute(movableMock.Object);

        Assert.True(Math.Abs(x2 - movableMock.Object.Orientation.X) < 0.0001);
        Assert.True(Math.Abs(y2 - movableMock.Object.Orientation.Y) < 0.0001);
    }


    [Fact]
    public void Execute_CantReadVelocity_ThrowsException()
    {
        var sut = new Rotate();
        var movableMock = new Mock<IRotatable>();
        movableMock.SetupGet(x => x.AngularVelocityRad).Throws<InvalidOperationException>();
        movableMock.SetupProperty(x => x.Orientation, new Vector2(12, 5));

        Assert.Throws<RotateException>(() => sut.Execute(movableMock.Object));

    }

    [Fact]
    public void Execute_CantReadPosition_ThrowsException()
    {
        var sut = new Rotate();
        var movableMock = new Mock<IRotatable>();
        movableMock.SetupGet(x => x.AngularVelocityRad).Returns(0.5f);
        movableMock.SetupGet(x => x.Orientation).Throws<Exception>();

        Assert.Throws<RotateException>(() => sut.Execute(movableMock.Object));

    }

    [Fact]
    public void Execute_CantSetPosition_ThrowsException()
    {
        var sut = new Rotate();
        var movableMock = new Mock<IRotatable>();
        movableMock.Setup(x => x.AngularVelocityRad).Returns(0.5f);
        movableMock.SetupGet(x => x.Orientation).Returns(new Vector2(12, 5));
        movableMock.SetupSet(x => x.Orientation).Throws(new Exception());

        Assert.Throws<RotateException>(() => sut.Execute(movableMock.Object));
    }
}
