using Godot;
using System;

public partial class Ball : CharacterBody2D
{

  [Export]
  public int Speed { get; set; } = 600;

  public Vector2 velocity { get; set; } = new Vector2();

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    // velocity = new Vector2((float)1, (float)-1).Normalized() * Speed;
  }

  public void Start(Vector2 startPosition)
  {
    Position = startPosition;
    velocity = new Vector2((float)1, (float)-1).Normalized() * Speed;
  }

  public override void _PhysicsProcess(double delta)
  {
    var collision = MoveAndCollide(velocity * (float)delta);

    if (collision == null)
    {
      return;
    }

    var collider = collision.GetCollider() as Node2D;

    if (collider.IsInGroup("balls"))
    {
      GD.Print("Ball collided with another ball");
      velocity = velocity.Bounce(collision.GetNormal());
      return;
    }

    if (collider.IsInGroup("enemies"))
    {
      var enemy = (Enemy)collider;
      GD.Print("Ball collided with an enemy");
      var normal = collision.GetNormal();
      enemy.AddDamage(1, normal);
      velocity = velocity.Bounce(normal);
      return;
    }

    if (collider.IsInGroup("bricks"))
    {
      var brick = (Brick)collider;
      GD.Print("Ball collided with a brick");
      brick.AddDamage(1);
      velocity = velocity.Bounce(collision.GetNormal());
      return;
    }

    if (collider.IsInGroup("paddles"))
    {
      GD.Print("Ball collided with the paddle");
      velocity = velocity.Bounce(collision.GetNormal());
      return;
    }

    if (collider.IsInGroup("walls"))
    {
      GD.Print("Ball collided with a wall");
      velocity = velocity.Bounce(collision.GetNormal());
      return;
    }
  }

}
