using UnityEngine;

public interface IKnockBackable
{
    public bool KnockBackable { get; }
    public void KnockBack(Vector2 angle, float strength, int direction);
}
