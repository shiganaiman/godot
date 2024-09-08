using Godot;
using System;

public partial class Paddle : CharacterBody2D
{


    [Export]
    public int Speed { get; set; } = 700;


    public override void _Ready()
    {

    }

    public override void _PhysicsProcess(double delta)
    {
        var velocity = Vector2.Zero;

        if (Input.IsActionPressed("move_right"))
        {
            velocity.X += 1;
        }

        if (Input.IsActionPressed("move_left"))
        {
            velocity.X -= 1;
        }

        if (velocity.Length() > 0)
        {
            velocity = velocity.Normalized() * Speed;
        }
        else
        {
            velocity = Vector2.Zero;
        }

        MoveAndCollide(velocity * (float)delta);

    }

}
