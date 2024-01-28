using System.Numerics;

namespace Lesson_1_2.Contracts;

public interface IMovable
{
    Vector2 Position { get; set; }

    Vector2 GetVelocity();
}
