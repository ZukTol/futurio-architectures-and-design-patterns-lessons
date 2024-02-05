using System.Numerics;
using Lesson_1_2.Commands;
using Lesson_1_2.Contracts;
using Moq;

namespace Lesson_1_2.Tests;

public class MoveTests
{
    [Theory]
    [InlineData(12f, 5f, -7f, 3f, 5f, 8f)]
    [InlineData(0f, 0f, 1f, 1f, 1f, 1f)]
    [InlineData(0f, 1f, 1f, 1f, 1f, 2f)]
    public void Execute_CorrectObject_Ok(float x1, float y1, float vx, float vy, float x2, float y2)
    {
        var sut = new Move();
        var movableMock = new Mock<IMovable>();
        movableMock.Setup(x => x.GetVelocity()).Returns(new Vector2(vx, vy));
        movableMock.SetupProperty(x => x.Position, new Vector2(x1, y1));

        sut.Execute(movableMock.Object);

        Assert.Equal(new Vector2(x2, y2), movableMock.Object.Position);

    }

    [Fact]
    public void Execute_CantReadVelocity_ThrowsException()
    {
        var sut = new Move();
        var movableMock = new Mock<IMovable>();
        movableMock.Setup(x => x.GetVelocity()).Throws<InvalidOperationException>();
        movableMock.SetupProperty(x => x.Position, new Vector2(12, 5));

        Assert.Throws<MoveException>(() => sut.Execute(movableMock.Object));

    }

    [Fact]
    public void Execute_CantReadPosition_ThrowsException()
    {
        var sut = new Move();
        var movableMock = new Mock<IMovable>();
        movableMock.Setup(x => x.GetVelocity()).Returns(new Vector2(-7, 3));
        movableMock.SetupGet(x => x.Position).Throws<Exception>();

        Assert.Throws<MoveException>(() => sut.Execute(movableMock.Object));

    }

    [Fact]
    public void Execute_CantSetPosition_ThrowsException()
    {
        var sut = new Move();
        var movableMock = new Mock<IMovable>();
        movableMock.Setup(x => x.GetVelocity()).Returns(new Vector2(-7, 3));
        movableMock.SetupGet(x => x.Position).Returns(new Vector2(12, 5));
        movableMock.SetupSet(x => x.Position).Throws(new Exception());

        Assert.Throws<MoveException>(() => sut.Execute(movableMock.Object));
    }

    [Fact]
    public void Execute_MovableNull_ThrowsException()
    {
        var sut = new Move();
        Assert.Throws<MoveException>(() => sut.Execute(null));
    }
}
