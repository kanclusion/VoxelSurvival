using Godot;
using System;
using System.Security.Cryptography.X509Certificates;
using static Godot.TextServer;

public partial class Player : CharacterBody3D
{
    [Export] public float WalkSpeed = 5.0f;
    [Export] public float SprintSpeed = 9.0f;
    [Export] public float Acceleration = 20.0f;
    [Export] public float Deceleration = 30.0f;
    [Export] public float JumpVelocity = 8f;
    [Export] public float RotationSpeed = 10.0f;
    [Export] public float BodyRotate = 5f;
    [Export] public float GravityMultiplie = 2;

    private Node3D _head;
    private SpringArm3D _springArm3D;
    private Camera3D _camera;
    private float _gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    private Area3D _deathZone;

    public override void _Ready()
    {
        _head = GetNode<Node3D>("Head");
        _springArm3D = _head.GetNode<SpringArm3D>("SpringArm3D");
        _deathZone = GetNode<Area3D>("/root/Main/DeathZone");
        _deathZone.BodyEntered += OnBodyEnteredDeathZone;

    }

    public override void _PhysicsProcess(double delta)
    {
        Vector3 velocity = Velocity;
        Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_forward", "move_back");
        Vector3 direction = new Vector3(inputDir.X, 0, inputDir.Y).Normalized();

        direction = Transform.Basis * direction;
        direction = direction.Normalized();

        float currentSpeed;

        if (Input.IsActionPressed("sprint"))
        {
            currentSpeed = SprintSpeed;
        } else
        {
            currentSpeed = WalkSpeed;
        }
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
            velocity.X = Mathf.MoveToward(velocity.X, direction.X * currentSpeed, Acceleration * (float)delta);
            velocity.Z = Mathf.MoveToward(velocity.Z, direction.Z * currentSpeed, Acceleration * (float)delta);

        }
        else
        {
            velocity.X = Mathf.MoveToward(velocity.X, 0, Deceleration * (float)delta);
            velocity.Z = Mathf.MoveToward(velocity.Z, 0, Deceleration * (float)delta);
        }



        //Moving
        GD.Print(Velocity);
        Velocity = velocity;
        MoveAndSlide();
    }
    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion motion &&
            Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            RotateY(-motion.Relative.X * 0.005f);
        }
    }

    private void OnBodyEnteredDeathZone(Node3D body)
    {
        if (body == this)
        {
            // Используем CallDeferred для безопасной перезагрузки
            CallDeferred(MethodName.ReloadScene);
        }
    }

    private void ReloadScene()
    {
        GetTree().ReloadCurrentScene();
    }
}
