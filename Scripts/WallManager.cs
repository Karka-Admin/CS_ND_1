using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class WallManager : Node2D
{
    [Export]
    private PackedScene WallObjectScene;
    [Export]
    private BirdController Bird;
    [Export]
    private int NumberOfWalls = 1;      // Total number of walls to generate
    [Export]
    private float InitialYPosition = 0f; // Starting Y position for the walls
    [Export]
    private float WallSpacing = 400f;    // Horizontal spacing between walls
    [Export]
    private float WallGapSize = 200f;    // Vertical gap size between walls
    [Export]
    private float MinYPosition = -200f;  // Minimum Y deviation for randomness
    [Export]
    private float MaxYPosition = 200f;   // Maximum Y deviation for randomness

    private float GameViewCenterX = 480 / 2;
    private float GameViewCenterY = 800 / 2;
    private float LastWallPositionX = 200f;

    private List<WallObject> walls = new List<WallObject>();

    public override void _Ready()
    {
        foreach (var yPos in GenerateWallPositions())
        {
            SpawnWall(LastWallPositionX, yPos);
        }

        Bird.Scored += OnScored;
    }

    private void OnScored()
    {
        float newYPosition = GameViewCenterY + (float)GD.RandRange(MinYPosition, MaxYPosition);
        SpawnWall(LastWallPositionX, newYPosition);
    }

    private IEnumerable<float> GenerateWallPositions()
    {
        return Enumerable.Range(0, NumberOfWalls)
                         .Select(i => InitialYPosition + (GameViewCenterY + (float)GD.RandRange(MinYPosition, MaxYPosition)));
    }

    private void SpawnWall(float spawnPositionX, float spawnPositionY)
    {
        var WallObject = WallObjectScene.Instantiate<WallObject>();

        WallObject.Position = new Vector2(spawnPositionX + WallSpacing, spawnPositionY);
        LastWallPositionX = WallObject.Position.X;

        walls.Add(WallObject);

        CallDeferred("add_child", WallObject);
    }
}