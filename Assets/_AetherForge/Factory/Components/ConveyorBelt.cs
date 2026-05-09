using Unity.Entities;

public struct ConveyorBelt : IComponentData
{
    public float Speed;
    public Direction Direction;
}

public enum Direction { North, East, South, West }