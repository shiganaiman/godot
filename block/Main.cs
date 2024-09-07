using Godot;
using System;

public partial class Main : Node
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // var paddle = GetNode<Paddle>("Paddle");
        // var ball = GetNode<Ball>("Ball");
        // var bricks = GetNode<Brick>("Bricks");

        // ball.BodyEntered += (body) =>
        // {
        //   if (body.IsInGroup("bricks"))
        //   {
        //     body.QueueFree();
        //   }
        // };

        // foreach (var brick in bricks.GetChildren())
        // {
        //   if (brick is Brick)
        //   {
        //     var animatedSprite2D = (brick as Brick).GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        //     string[] mobTypes = animatedSprite2D.SpriteFrames.GetAnimationNames();
        //     animatedSprite2D.Play(mobTypes[GD.Randi() % mobTypes.Length]);
        //   }
        // }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
