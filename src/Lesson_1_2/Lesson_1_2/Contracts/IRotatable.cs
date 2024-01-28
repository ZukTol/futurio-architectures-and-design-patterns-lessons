using System.Numerics;

namespace Lesson_1_2.Contracts;

public interface IRotatable
{
    Vector2 Orientation { get; set; }

    float AngularVelocityRad { get; }
}
