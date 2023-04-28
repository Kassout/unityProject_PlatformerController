using UnityEngine;

public class CombatDummyController : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float knockBackSpeedX;
    [SerializeField] private float knockBackSpeedY;
    [SerializeField] private float knockBackDuration;
    [SerializeField] private float knockBackDeathSpeedX;
    [SerializeField] private float knockBackDeathSpeedY;
    [SerializeField] private float deathTorque;
    [SerializeField] private bool applyKnockBack;
    [SerializeField] private GameObject hitParticle;

    private bool _playerOnLeft;
    private bool _knockBack;

    private int _playerFacingDirection;

    private float _currentHealth;
    private float _knockBackStart;

    private GameObject _aliveGameObject;
    private GameObject _brokenTopGameObject;
    private GameObject _brokenBottomGameObject;

    private Rigidbody2D _aliveRigidbody;
    private Rigidbody2D _brokenTopRigidbody;
    private Rigidbody2D _brokenBottomRigidbody;

    private Animator _aliveAnimator;

    private void Start()
    {
        _currentHealth = maxHealth;

        _aliveGameObject = transform.Find("Alive").gameObject;
        _brokenTopGameObject = transform.Find("Broken Top").gameObject;
        _brokenBottomGameObject = transform.Find("Broken Bottom").gameObject;

        _aliveAnimator = _aliveGameObject.GetComponent<Animator>();

        _aliveRigidbody = _aliveGameObject.GetComponent<Rigidbody2D>();
        _brokenTopRigidbody = _brokenTopGameObject.GetComponent<Rigidbody2D>();
        _brokenBottomRigidbody = _brokenBottomGameObject.GetComponent<Rigidbody2D>();

        _aliveGameObject.SetActive(true);
        _brokenTopGameObject.SetActive(false);
        _brokenBottomGameObject.SetActive(false);
    }

    private void Update()
    {
        CheckKnockBack();
    }

    private void CheckKnockBack()
    {
        if (Time.time >= _knockBackStart + knockBackDuration && _knockBack)
        {
            _knockBack = false;
            _aliveRigidbody.velocity = new Vector2(0.0f, _aliveRigidbody.velocity.y);
        }
    }
}