using System.Numerics;
using Lesson_1_2.Commands;
using Lesson_1_2.Contracts;
using Moq;
using Xunit;

namespace Lesson_1_2.Tests;

public class MoveTests
{
    [Fact]
    public void Execute_CorrectObject_Ok()
    {
        var sut = new Move();
        var movableMock = new Mock<IMovable>();
        movableMock.Setup(x => x.GetVelocity()).Returns(new Vector2(-7, 3));
        movableMock.SetupProperty(x => x.Position, new Vector2(12, 5));

        sut.Execute(movableMock.Object);

        Assert.Equal(new Vector2(5, 8), movableMock.Object.Position);
    }

    [Fact]
    public void Execute_CantReadVelocity_ThrowsException()
    {
        var sut = new Move();
        var movableMock = new Mock<IMovable>();
        movableMock.Setup(x => x.GetVelocity()).Throws<InvalidOperationException>();
        movableMock.SetupProperty(x => x.Position, new Vector2(12, 5));

        Assert.Throws<InvalidOperationException>(() => sut.Execute(movableMock.Object));

    }
}
