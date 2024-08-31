using Godot;
using System;

public partial class sprite_2d : Sprite2D
{
  private float _speed = 200;
  private float _angularSpeed = Mathf.Pi;

  public override void _Ready()
  {
    GD.Print("Hello, world!");

    var timer = GetNode<Timer>("BlinkingTimer");
    if (timer != null)
    {
      GD.Print("Timer found");
      timer.Timeout += OnTimerTimeout;
    }
    else
    {
      GD.Print("Timer not found");
    }

    var button = GetNode<Button>("../ToggleButton");
    if (button != null)
    {
      GD.Print("Button found");
      button.Pressed += OnButtonPressed;
    }
    else
    {
      GD.Print("Button not found");
    }



    // button.Pressed += () => OnButtonPressed();
    // if (button != null)
    // {
    //   button.Connect("pressed", this, nameof(OnButtonPressed)); // シグナル接続
    // }
    // else
    // {
    //   GD.Print("Button node not found");
    // }:

    // button.Pressed += () => OnButtonPressed();
    // button.pressed += OnButtonPressed;
    // button.Connect("pressed", this, nameof(OnButtonPressed));
    // if (button != null)
    // {
    //   button.Connect("pressed", new Callable(this, nameof(OnButtonPressed))); // シグナル接続
    // }
    // else
    // {
    //   GD.Print("Button node not found");
    // }
  }

  public override void _Process(double delta)
  {
    Rotation += _angularSpeed * (float)delta;
    var velocity = Vector2.Up.Rotated(Rotation) * _speed;
    Position += velocity * (float)delta;
  }

  // We also specified this function name in PascalCase in the editor's connection window.
  private void OnButtonPressed()
  {
    GD.Print("OnButtonPressed");
    SetProcess(!IsProcessing());
  }
  private void OnTimerTimeout()
  {
    Visible = !Visible;
  }
}


