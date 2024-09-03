using Godot;
using System;

public partial class HUD : CanvasLayer
{
  [Signal]
  public delegate void StartGameEventHandler();

  public override void _Ready()
  {
    GetNode<Button>("StartButton").Pressed += OnStartButtonPressed;
    GetNode<Timer>("MessageTimer").Timeout += OnMessageTimerTimeout;
  }

  public void UpdateScore(int score)
  {
    GetNode<Label>("ScoreLabel").Text = score.ToString();
  }

  // We also specified this function name in PascalCase in the editor's connection window.
  private void OnStartButtonPressed()
  {
    GetNode<Button>("StartButton").Hide();
    GD.Print("Start button pressed, emitting StartGame signal.");
    EmitSignal(SignalName.StartGame);
  }

  // We also specified this function name in PascalCase in the editor's connection window.
  private void OnMessageTimerTimeout()
  {
    GetNode<Label>("Message").Hide();
  }

  public void ShowMessage(string text)
  {
    var message = GetNode<Label>("Message");
    message.Text = text;
    message.Show();

    GetNode<Timer>("MessageTimer").Start();
  }

  async public void ShowGameOver()
  {
    ShowMessage("Game Over");

    var messageTimer = GetNode<Timer>("MessageTimer");
    await ToSignal(messageTimer, Timer.SignalName.Timeout);

    var message = GetNode<Label>("Message");
    message.Text = "Dodge the Creeps!";
    message.Show();

    await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
    GetNode<Button>("StartButton").Show();
  }
}
