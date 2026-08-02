using Godot;
using System;
using System.Security.Cryptography.X509Certificates;
using static Godot.TextServer;

public partial class Player : CharacterBody3D
{
    [Export] public float Speed = 5.0f;
    [Export] public float JumpVelocity = 8f;
    [Export] public float RotationSpeed = 10.0f;
    [Export] public float BodyRotate = 5f;
    [Export] public float GravityMultiplie = 2;

    private SpringArm3D _springArm3D;
    private Camera3D _camera;
    private float _gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    public override void _Ready()
    {
        _springArm3D = GetNode<SpringArm3D>("SpringArm3D");
        _camera = _springArm3D.GetNode<Camera3D>("Camera3D");
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector3 velocity = Velocity;
        Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_forward", "move_back");
        Vector3 direction = new Vector3(inputDir.X, 0, inputDir.Y).Normalized();

        direction = direction.Rotated(Vector3.Up, _springArm3D.GlobalRotation.Y);
        //Falling
        if (!IsOnFloor())
        {
            velocity.Y -= _gravity * GravityMultiplie * (float)delta;
        }
        //Falling

        ////Jump
        if (Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            velocity.Y = JumpVelocity;
        }
        //Jump

        //Moving
        if (direction != Vector3.Zero)
        {
            velocity.X = direction.X * Speed;
            velocity.Z = direction.Z * Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(velocity.X, 0, Speed);
            velocity.Z = Mathf.MoveToward(velocity.Z, 0, Speed);
        }
        //Moving

        Velocity = velocity;
        MoveAndSlide();
    }
}
