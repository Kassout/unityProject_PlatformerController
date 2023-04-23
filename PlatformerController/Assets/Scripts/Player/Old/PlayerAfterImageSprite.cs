using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAfterImageSprite : MonoBehaviour
{
    [SerializeField] private float activeTime = 0.1f;

    [SerializeField] private float alphaSet = 0.8f;
    [SerializeField] private float alphaDecay = 0.85f;

    private float _timeActivated;
    private float _alpha;

    private Transform _player;

    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _playerSpriteRenderer;

    private Color _color;

    private void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerSpriteRenderer = _player.GetComponent<SpriteRenderer>();

        _alpha = alphaSet;
        _spriteRenderer.sprite = _playerSpriteRenderer.sprite;
        transform.position = _player.position;
        transform.rotation = _player.rotation;

        _timeActivated = Time.time;
    }

    private void Update()
    {
        _alpha -= alphaDecay * Time.deltaTime;
        _color = new Color(1f, 1f, 1f, _alpha);
        _spriteRenderer.color = _color;

        if (Time.time >= (_timeActivated + activeTime))
        {
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }
    }
}