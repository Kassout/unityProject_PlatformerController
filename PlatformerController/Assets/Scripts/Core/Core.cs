using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement {
        get
        {
            if (_movement != null)
            {
                return _movement;
            }
            
            Debug.LogError("No Movement Core Component on " + transform.parent.name);
            return null;
        }
        
        private set { _movement = value; }
    }

    public CollisionSenses CollisionSenses
    {
        get
        {
            if (_collisionSenses != null)
            {
                return _collisionSenses;
            }
            
            Debug.LogError("No Collision Senses Core Component on " + transform.parent.name);
            return null;
        }
        
        private set { _collisionSenses = value; }
    }

    private Movement _movement;
    private CollisionSenses _collisionSenses;

    private void Awake()
    {
        _movement = GetComponentInChildren<Movement>();
        _collisionSenses = GetComponentInChildren<CollisionSenses>();

        if (!Movement || !CollisionSenses)
        {
            Debug.LogError("Missing Core Component.");
        }
    }

    public void LogicUpdate()
    {
        Movement.LogicUpdate();
    }
}
