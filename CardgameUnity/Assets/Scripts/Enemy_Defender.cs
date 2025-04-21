using UnityEngine;

public class Enemy_Defender : Enemy
{
    public override bool IsDefender => true;
    public override int StackPriority => 1;

    protected override void Awake()
    {
        base.Awake();
        Attack = 1;
        HitPoints = 5;
        Level = 1;
    }

    public override void Initialize()
    {
        base.Initialize();
    }
}
