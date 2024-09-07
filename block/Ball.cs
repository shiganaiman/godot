using Godot;
using System;

public partial class Ball : RigidBody2D
{

    [Export]
    public int Speed { get; set; } = 600;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var velocity = new Vector2((float)1, (float)-1).Normalized() * Speed;
        ApplyCentralImpulse(velocity);

        SetContactMonitor(true);
        SetMaxContactsReported(1);
        BodyEntered += OnBodyEntered;

        // OnBodyEntered += OnBodyEntered;

    }

    // public override void PhysicsProcess(float delta)
    // {
    //   GD.Print("Ball collided with something");
    // }

    public void OnBodyEntered(Node body)
    {
        GD.Print("Ball collided with something");
        GD.Print(body.GetGroups());
        if (body.IsInGroup("bricks"))
        {
            GD.Print("Ball collided with a brick");
            body.QueueFree();
        }
    }
}
