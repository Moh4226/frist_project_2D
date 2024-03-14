using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    private bool m_isMoving;
    private bool m_isShooting;
    private Vector2 m_direction;
    private CharacterVisual m_characterVisual;
    private InputControls m_inputControls;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_characterVisual = GetComponentInChildren<CharacterVisual>();
    }

    protected void Move(Vector2 direction, float speed)
    {
        direction = direction.normalized;
        m_rigidbody.velocity = direction * speed;

        m_isMoving = direction.magnitude > 0.1f;
        if (m_isMoving)
            m_direction = direction;
    }
    protected void SetShooting(bool Flag) 
    {
        m_isShooting = Flag;
    }
    public bool IsShooting()
    {
        return m_isShooting;
    }
    public virtual void Shoot()
    {
        //m_inputControls.Player.Shoot();
        //Overriden by the child class
    }
    
    protected void ShootRifile() 
    {
        var piont = m_characterVisual.GetShootingPiont();

        RaycastHit2D hit = Physics2D.Raycast(piont.transform.position,m_direction);
        if (hit) {
            m_characterVisual.DrawShootingLine(piont.transform.position,hit.point);
            if (hit.rigidbody !=null && hit.rigidbody.TryGetComponent<HitCollider>(out var collider)) {
                collider.HandleHit();
            
            }
        }
    }

    public CharacterWeapon GetWeapon()
    {
        return CharacterWeapon.Rifle;
    }

    public bool IsMoving()
    {
        return m_isMoving;
    }

    public Vector2 GetMovementDirection()
    {
        return m_direction;
    }
}
