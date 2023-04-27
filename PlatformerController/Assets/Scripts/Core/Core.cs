using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement
    {
        get => GenericNotImplementedError<Movement>.TryGet(_movement, transform.parent.name);
        private set => _movement = value;
    }

    public CollisionSenses CollisionSenses
    {
        get => GenericNotImplementedError<CollisionSenses>.TryGet(_collisionSenses, transform.parent.name);
        private set => _collisionSenses = value;
    }

    public Combat Combat
    {
        get => GenericNotImplementedError<Combat>.TryGet(_combat, transform.parent.name);
        private set => _combat = value;
    }

    private Movement _movement;
    private CollisionSenses _collisionSenses;
    private Combat _combat;

    private void Awake()
    {
        _movement = GetComponentInChildren<Movement>();
        _collisionSenses = GetComponentInChildren<CollisionSenses>();
        _combat = GetComponentInChildren<Combat>();
    }

    public void LogicUpdate()
    {
        Movement.LogicUpdate();
        Combat.LogicUpdate();
    }
}
