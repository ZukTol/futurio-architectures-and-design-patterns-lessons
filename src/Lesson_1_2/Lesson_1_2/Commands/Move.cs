using Lesson_1_2.Contracts;

namespace Lesson_1_2.Commands;

public class Move
{
    public void Execute(IMovable movable)
    {
        try
        {
            if (movable == null)
                ThrowError(new ArgumentNullException(nameof(movable)));

            movable.Position += movable.GetVelocity();
        }
        catch (Exception e)
        {
            ThrowError(e);
        }
    }

    public void ThrowError(Exception exception)
    {
        throw new MoveException("Move error", exception);
    }
}
