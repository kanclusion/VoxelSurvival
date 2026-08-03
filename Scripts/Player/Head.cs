using Godot;
using System;

public partial class Head : Node3D
{
    [Export] public float MouseSensitivity = 0.005f;
    [Export] public float MinPitch = -90f;
    [Export] public float MaxPitch = 45f;

    private SpringArm3D _springArm;

    public override void _Ready()
    {
        _springArm = GetNode<SpringArm3D>("SpringArm3D");
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        // Поворот камеры вверх/вниз
        if (@event is InputEventMouseMotion mouseMotion &&
            Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            Vector3 rotation = Rotation;

            rotation.X -= mouseMotion.Relative.Y * MouseSensitivity;
            rotation.X = Mathf.Clamp(
                rotation.X,
                Mathf.DegToRad(MinPitch),
                Mathf.DegToRad(MaxPitch));

            Rotation = rotation;
        }

        // Приближение камеры
        if (@event.IsActionPressed("wheel_up"))
        {
            _springArm.SpringLength =
                Mathf.Clamp(_springArm.SpringLength - 1f, 3f, 10f);
        }

        // Отдаление камеры
        if (@event.IsActionPressed("wheel_down"))
        {
            _springArm.SpringLength =
                Mathf.Clamp(_springArm.SpringLength + 1f, 3f, 10f);
        }

        // Освободить/захватить мышь
        if (@event.IsActionPressed("mouse_mode_toggle"))
        {
            Input.MouseMode =
                Input.MouseMode == Input.MouseModeEnum.Captured
                ? Input.MouseModeEnum.Visible
                : Input.MouseModeEnum.Captured;
        }
    }
}