using Godot;
using System;

public partial class Main : Node
{
  [Export]
  public PackedScene BallScene { get; set; }

  public override void _Ready()
  {
        // シーンのリソースを指定
    BallScene = (PackedScene)ResourceLoader.Load("res://ball.tscn");
  }

  public override void _Process(double delta)
  {
    if (Input.IsActionJustPressed("space"))
    {
      var paddle = GetNode<Paddle>("Paddle");
      // var ball = GetNode<Ball>("Ball");
      var ball = BallScene.Instantiate<Ball>();
      ball.AddToGroup("balls");
      ball.Start(paddle.Position + new Vector2(0, -32));
      AddChild(ball);
    }
  }
}
