using System;
using Godot;

public partial class Mob : RigidBody2D
{

  public override void _Ready()
  {
    var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    string[] mobTypes = animatedSprite2D.SpriteFrames.GetAnimationNames();
    animatedSprite2D.Play(mobTypes[GD.Randi() % mobTypes.Length]);

    GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D").ScreenExited += OnVisibleOnScreenNotifier2DScreenExited;
  }

  private void OnVisibleOnScreenNotifier2DScreenExited()
  {
    GD.Print("OnVisibleOnScreenNotifier2DScreenExited");
    QueueFree();
  }
}
