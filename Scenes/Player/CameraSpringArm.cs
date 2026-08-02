using Godot;
using Godot.Collections;
using System;

public partial class CameraSpringArm : SpringArm3D
{
    [Export] public float MouseSensibility = 0.005f;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _UnhandledInput(InputEvent @event)
    {

        if (@event is InputEventMouseMotion mouseMotion)
		{
            float newRotationX = Rotation.X - mouseMotion.Relative.Y * MouseSensibility;
            float newRotationY = Rotation.Y - mouseMotion.Relative.X * MouseSensibility;

            newRotationY = Mathf.Wrap(newRotationY, 0.0f, MathF.Tau);
            newRotationX = Mathf.Clamp(newRotationX, -Mathf.Pi/2, Mathf.Pi / 4);

            Rotation = new Vector3(newRotationX, newRotationY, Rotation.Z);
        }
    }

}
