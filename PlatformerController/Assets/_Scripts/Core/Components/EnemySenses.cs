using UnityEngine;

public class EnemySenses : CoreComponent
{
    #region Fields
    
    [SerializeField] private float minAggroDistance = 3f;
    [SerializeField] private float maxAggroDistance = 4f;
    [SerializeField] private float closeRangeActionDistance = 1f;
    
    [SerializeField] private LayerMask whatIsPlayer;
    
    [SerializeField] private Transform playerCheck;

    #endregion

    #region Properties

    public bool PlayerInMinAggroRange => Physics2D.Raycast(playerCheck.position, transform.right, 
        minAggroDistance, whatIsPlayer);

    public bool PlayerInMaxAggroRange => Physics2D.Raycast(playerCheck.position, transform.right, 
        maxAggroDistance, whatIsPlayer);

    public bool PlayerInCloseRangeAction => Physics2D.Raycast(playerCheck.position, transform.right, 
        closeRangeActionDistance, whatIsPlayer);
    
    public float MinAggroDistance => minAggroDistance;
    public float MaxAggroDistance => maxAggroDistance;
    public float CloseRangeActionDistance => closeRangeActionDistance;
    
    public LayerMask WhatIsPlayer => whatIsPlayer;
    
    public Transform PlayerCheck => GenericNotImplementedError<Transform>.TryGet(playerCheck, _core.transform.parent.name);

    #endregion
    
    #region Private

    private void OnDrawGizmos()
    {
        if (_core)
        {
            if (PlayerCheck)
            {
                var playerCheckPosition = playerCheck.position;
                Gizmos.DrawWireSphere(playerCheckPosition + Vector3.right * closeRangeActionDistance, 0.2f);
                Gizmos.DrawWireSphere(playerCheckPosition + Vector3.right * minAggroDistance, 0.2f);
                Gizmos.DrawWireSphere(playerCheckPosition + Vector3.right * maxAggroDistance, 0.2f);  
            }
        }
    }

    #endregion
}
