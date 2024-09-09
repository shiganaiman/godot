using System;
using Godot;

public partial class Brick : StaticBody2D
{
  public int hp = 3;

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    string[] mobTypes = animatedSprite2D.SpriteFrames.GetAnimationNames();
    animatedSprite2D.Play(mobTypes[GD.Randi() % mobTypes.Length]);

    var hpLabel = GetNode<Label>("HpLabel");
    hpLabel.Text = hp.ToString();
  }

  public void AddDamage(int damage)
  {
    hp -= damage;
    if (hp <= 0)
    {
      QueueFree();
    }
    var hpLabel = GetNode<Label>("HpLabel");
    hpLabel.Text = hp.ToString();
  }

}
