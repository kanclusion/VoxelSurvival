using Godot;
using Godot.Collections;
using System;

public partial class CameraSpringArm : SpringArm3D
{
    [Export] public float MouseSensibility = 0.005f;

    public override void _Ready()
	{
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _UnhandledInput(InputEvent @event)
    {

        if (@event is InputEventMouseMotion mouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured)
		{
            float newRotationX = Rotation.X - mouseMotion.Relative.Y * MouseSensibility;
            float newRotationY = Rotation.Y - mouseMotion.Relative.X * MouseSensibility;

            newRotationY = Mathf.Wrap(newRotationY, 0.0f, MathF.Tau);
            newRotationX = Mathf.Clamp(newRotationX, -Mathf.Pi/2, Mathf.Pi / 4);

            Rotation = new Vector3(newRotationX, newRotationY, Rotation.Z);
        }

        if (@event.IsActionPressed("wheel_up"))
        {
            float newSpringLength = SpringLength - 1;
            newSpringLength = Mathf.Clamp(newSpringLength, 3, 10);
            SpringLength = newSpringLength;
        }
        if (@event.IsActionPressed("wheel_down"))
        {
            float newSpringLength = SpringLength + 1;
            newSpringLength = Mathf.Clamp(newSpringLength, 3, 10);
            SpringLength = newSpringLength;
        }

        if (@event.IsActionPressed("mouse_mode_toggle"))
        {
            if (Input.MouseMode == Input.MouseModeEnum.Captured)
            {
                Input.MouseMode = Input.MouseModeEnum.Visible;
            } else
            {
                Input.MouseMode = Input.MouseModeEnum.Captured;
            }
        }
    }

}
