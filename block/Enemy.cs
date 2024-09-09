using System;
using Godot;

public partial class Enemy : CharacterBody2D
{
  public int hp = 100;

  private Vector2 originalPosition;
  private float shakeAmplitude = 10f;  // 揺れの振れ幅
  private float shakeSpeed = 30f;       // 揺れの速度
  private Vector2 shakeNormal = new Vector2();
  private float time = 0f;
  private bool isHit = false;

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    originalPosition = Position;

    var hpLabel = GetNode<Label>("HpLabel");
    hpLabel.Text = hp.ToString();

    var hitTimer = GetNode<Timer>("HitTimer");
    hitTimer.Timeout += OnHitTimerTimeout;

  }

  public void AddDamage(int damage, Vector2 normal)
  {
    hp -= damage;
    if (hp <= 0)
    {
      QueueFree();
    }
    var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    animatedSprite2D.Animation = "hit";
    var hitTimer = GetNode<Timer>("HitTimer");
    hitTimer.Start();
    isHit = true;
    shakeNormal = normal;
    var hpLabel = GetNode<Label>("HpLabel");
    hpLabel.Text = hp.ToString();

  }

  public override void _PhysicsProcess(double delta)
  {
    // 時間経過に基づいて揺れを実現
    //
    if (!isHit)
    {
      return;
    }
    time += (float)delta * shakeSpeed;
    Vector2 shakeOffset = shakeNormal * Mathf.Sin(time) * shakeAmplitude;
    Position = originalPosition + shakeOffset;
  }

  public void OnHitTimerTimeout()
  {
    var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    animatedSprite2D.Animation = "default";
    isHit = false;
  }

}
