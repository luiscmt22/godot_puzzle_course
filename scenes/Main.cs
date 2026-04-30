using Godot;

namespace PuzzleCourse.scenes;
public partial class Main : Node2D
{
    private Sprite2D _sprite;
    private PackedScene _building;
    private Button _placeBuildingButton;
    
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        _building = GD.Load<PackedScene>("res://scenes/Building/Building.tscn");
		_sprite = GetNode<Sprite2D>("Cursor");
        _placeBuildingButton = GetNode<Button>("PlaceBuildingButton");
        
        _placeBuildingButton.Pressed += () => PlaceBuildingAtMousePosition();
        
        
	}

    public override void _UnhandledInput(InputEvent evt)
    {
	    if (evt.IsActionPressed("left_click"))
	    {
			PlaceBuildingAtMousePosition();
	    }
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        _sprite.GlobalPosition = getMouseGridCellPositionVector() * 64; // Snap the sprite to the grid
        GD.Print($"Mouse Position: {getMouseGridCellPositionVector()}, Grid Position: {getMouseGridCellPositionVector()}");
	}

	private Vector2 getMouseGridCellPositionVector()
	{
		var mousePosition = GetGlobalMousePosition();
		var gridPosition = mousePosition / 64; // A grid cell size is 64x64 pixels
		gridPosition = gridPosition.Floor(); // Get the integer grid coordinates
		return gridPosition.Clamp(new Vector2(0, 0), new Vector2(17, 9)); // Clamp to grid bounds (assuming a 10x10 grid)
	}

	private void PlaceBuildingAtMousePosition()
	{	
		var buildingInstance = _building.Instantiate<Node2D>();
		buildingInstance.GlobalPosition = getMouseGridCellPositionVector() * 64; // Place the building at the snapped position
		AddChild(buildingInstance);
	}
}
