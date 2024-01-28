using System.Numerics;
using Lesson_1_2.Contracts;
using static System.Math;

namespace Lesson_1_2.Commands;

public class Rotate
{
    public void Execute(IRotatable rotatable)
    {
        if (rotatable == null)
            ThrowError(new ArgumentNullException(nameof(rotatable)));

        try
        {
            var angle = rotatable.AngularVelocityRad;
            var x = rotatable.Orientation.X;
            var y = rotatable.Orientation.Y;

            var rotatedX = x * Cos(angle) - y * Sin(angle);
            var rotatedY = x * Sin(angle) + y * Cos(angle);
            rotatable.Orientation = new Vector2((float)rotatedX, (float)rotatedY);
        }
        catch (Exception e)
        {
            ThrowError(e);
        }
    }

    public void ThrowError(Exception exception)
    {
        throw new RotateException("Rotate error", exception);
    }
}
