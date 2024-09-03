using Godot;
using System;

public partial class Main : Node
{
  [Export]
  public PackedScene MobScene { get; set; }

  private int _score;

  public override void _Ready()
  {
    // シーンのリソースを指定
    MobScene = (PackedScene)ResourceLoader.Load("res://mob.tscn");

    var player = GetNode<Player>("/root/Main/Player");
    player.Hit += GameOver;

    var startTimer = GetNode<Timer>("StartTimer");
    startTimer.Timeout += OnStartTimerTimeout;

    var mobTimer = GetNode<Timer>("MobTimer");
    mobTimer.Timeout += OnMobTimerTimeout;
    
    var scoreTimer = GetNode<Timer>("ScoreTimer");
    scoreTimer.Timeout += OnScoreTimerTimeout;

    var hud = GetNode<HUD>("HUD");
    hud.StartGame += NewGame;

  }

  public void GameOver()
  {
    GetNode<Timer>("MobTimer").Stop();
    GetNode<Timer>("ScoreTimer").Stop();
    GetNode<HUD>("HUD").ShowGameOver();
    GetNode<AudioStreamPlayer2D>("Music").Stop();
    GetNode<AudioStreamPlayer2D>("DeathSound").Play();
  }

  public void NewGame()
  {
    GD.Print("NewGame method called.");
    _score = 0;
    GetTree().CallGroup("mobs", Node.MethodName.QueueFree);

    var player = GetNode<Player>("/root/Main/Player");
    var startPosition = GetNode<Marker2D>("StartPosition");
    player.Start(startPosition.Position);

    var hud = GetNode<HUD>("HUD");
    hud.UpdateScore(_score);
    hud.ShowMessage("Get Ready!");

    GetNode<Timer>("StartTimer").Start();
    GetNode<AudioStreamPlayer2D>("Music").Play();
  }

  // We also specified this function name in PascalCase in the editor's connection window.
  private void OnScoreTimerTimeout()
  {
    _score++;
    GetNode<HUD>("HUD").UpdateScore(_score);
  }

  // We also specified this function name in PascalCase in the editor's connection window.
  private void OnStartTimerTimeout()
  {
    GetNode<Timer>("MobTimer").Start();
    GetNode<Timer>("ScoreTimer").Start();
  }

  private void OnMobTimerTimeout()
  {
    // GD.Print("MobTimerTimeout");
    // Note: Normally it is best to use explicit types rather than the `var`
    // keyword. However, var is acceptable to use here because the types are
    // obviously Mob and PathFollow2D, since they appear later on the line.
    // Create a new instance of the Mob scene.
    Mob mob = MobScene.Instantiate<Mob>();

    // Choose a random location on Path2D.
    var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
    mobSpawnLocation.ProgressRatio = GD.Randf();

    // Set the mob's direction perpendicular to the path direction.
    float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

    // Set the mob's position to a random location.
    mob.Position = mobSpawnLocation.Position;
    // GD.Print(mob.Position.x, mob.Position.y);

    // Add some randomness to the direction.
    direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
    mob.Rotation = direction;

    // Choose the velocity.
    var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
    mob.LinearVelocity = velocity.Rotated(direction);

    // Spawn the mob by adding it to the Main scene.
    AddChild(mob);
  }

}
