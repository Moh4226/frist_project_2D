using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private PlayerConfiguration m_config;
    private InputControls m_PlayerAction;


    private void Start()
    {
       CameraControllers.Instance.RegisterTarget(transform);
    }

    private void OnDisable()
    {
        if (CameraControllers.Instance != null)
            CameraControllers.Instance.UnregisterTarget(transform);
    }
    public override void Shoot()
    {
        ShootRifile();

    }
    private void Update()
    {
        Vector2 movement = InputManger.Instance.GetPlayerMovement();
        Move(movement, m_config.MovementSpeed);
        SetShooting(InputManger.Instance.GetPlayerShooting());

    }

}
