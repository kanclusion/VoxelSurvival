using Godot;
using System;
using System.Security.Cryptography.X509Certificates;
using static Godot.TextServer;

public partial class Player : CharacterBody3D
{
    [Export] public float Speed = 5.0f;
    [Export] public float JumpVelocity = 6f;
    [Export] public float MouseSensitivity = 0.005f;
    [Export] public float RotationSpeed = 10.0f;

    private Node3D _cameraPivot;
    private Camera3D _camera;
    private float _gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    public override void _Ready()
    {


    }

    public override void _PhysicsProcess(double delta)
    {
        Vector3 velocity = Velocity;

        //Jump
        if (!IsOnFloor())
        {
            velocity.Y -= _gravity * (float)delta;
        }
        
        if (Input.IsActionJustPressed("jump"))
        {
            velocity.Y += JumpVelocity;
        }
        //Jump

        Velocity = velocity;
        MoveAndSlide();
    }
}
