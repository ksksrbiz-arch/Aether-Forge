using Unity.Entities;

public struct Machine : IComponentData
{
    public MachineType Type;
    public float ProcessingTime;
    public float CurrentProgress;
}

public enum MachineType
{
    Smelter,
    Assembler
}