using System;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject player;
    [SerializeField] private float respawnTime;

    private float _respawnTimeStart;

    private bool _respawn;
    private CinemachineVirtualCamera _camera;

    private void Start()
    {
        _camera = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        CheckRespawn();
    }

    public void Respawn()
    {
        _respawnTimeStart = Time.time;
        _respawn = true;
    }

    private void CheckRespawn()
    {
        if (Time.time >= _respawnTimeStart + respawnTime && _respawn)
        {
            var playerTemp = Instantiate(player, respawnPoint);
            _camera.m_Follow = playerTemp.transform;
            _respawn = false;
        }
    }
}
