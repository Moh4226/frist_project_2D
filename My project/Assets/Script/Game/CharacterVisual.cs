using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterVisual : MonoBehaviour
{
    [Serializable]
    public struct Directions
    {
        public GameObject Up;
        public GameObject Down;
        public GameObject Side;
    }

    private enum Direction { Left, Right, Up, Down };

    [SerializeField] private Directions m_rifle;
    [SerializeField] private Directions m_rifleWalk;
    [SerializeField] private Directions m_scytheIdle;
    [SerializeField] private Directions m_scytheAttack;
    [SerializeField] private GameObject[] m_muzzleFlashes;
    [SerializeField] private Directions m_riflieShootingPoint;
    [SerializeField] private Directions m_riflieWalkShootingPoint;
    [SerializeField] private LineRenderer m_lineRenderer;


    private Character m_character;

    private void Awake()
    {
        m_character = GetComponentInParent<Character>();
        m_lineRenderer.gameObject.SetActive(false);
    }

    private static void SetScaleX(Transform t, float x)
    {
        Vector3 scale = t.localScale;
        scale.x = x;
        t.localScale = scale;
    }

    private Direction GetDirection(Vector2 dir)
    {
        if (dir.x < -0.5f)
        {
            return Direction.Left;
        }
        else if (dir.x > 0.5f)
        {
            return Direction.Right;
        }
        else if (dir.y < 0)
        {
            return Direction.Down;
        }
        else
        {
            return Direction.Up;
        }

    }

    private GameObject GetDirectionalSprite(Directions sprites, Vector2 dir)
    {
      
        switch (GetDirection(dir)) { 
            case Direction.Left:
                 SetScaleX(sprites.Side.transform, 1.0f);
                 return sprites.Side;

            case Direction.Right:
                SetScaleX(sprites.Side.transform, -1.0f);
                return sprites.Side;

            case Direction.Down:
                return sprites.Down;

            case Direction.Up:
            default:
                return sprites.Up;
        }
    }

    public Transform GetShootingPiont()
    {
        Vector2 dir = m_character.GetMovementDirection();
        bool isMoving = m_character.IsMoving();
        Directions pionts;
        if (isMoving)
        {
            pionts = m_riflieWalkShootingPoint;
        }
        else
            pionts = m_riflieShootingPoint;

        switch (GetDirection(dir))
        {
            case Direction.Left:
            case Direction.Right:
                return pionts.Side.transform;

            case Direction.Down:
                return pionts.Down.transform;

            case Direction.Up:
            default:
                return pionts.Up.transform;
        }

    }

    private void EnableDirection(Directions directions, GameObject visible)
    {
        directions.Up.SetActive(directions.Up == visible);
        directions.Down.SetActive(directions.Down == visible);
        directions.Side.SetActive(directions.Side == visible);
    }
    public void DrawShootingLine(Vector2 start, Vector2 end)
    {
        m_lineRenderer.SetPosition(0,start);
        m_lineRenderer.SetPosition(1,end);
        m_lineRenderer.gameObject.SetActive(true);
        StartCoroutine(HideShooting());

    }

    private IEnumerator HideShooting() {
        
        yield return new WaitForSeconds(0.1f);
        m_lineRenderer.gameObject.SetActive(false);


    }
    private void Update()
    {
        Vector2 dir = m_character.GetMovementDirection();
        bool isMoving = m_character.IsMoving();

        Directions directions;
        switch (m_character.GetWeapon())
        {
            case CharacterWeapon.Scythe:
                directions = m_scytheIdle;
                break;

            case CharacterWeapon.Rifle:
                if (isMoving)
                    directions = m_rifleWalk;
                else
                    directions = m_rifle;
                break;

            default:
                directions = m_rifle;
                break;
        }

        GameObject sprite = GetDirectionalSprite(directions, dir);
        EnableDirection(m_rifle, sprite);
        EnableDirection(m_rifleWalk, sprite);
        EnableDirection(m_scytheIdle, sprite);
        EnableDirection(m_scytheAttack, sprite);

        bool isShooting = m_character.IsShooting();
        foreach (GameObject obj in m_muzzleFlashes)
            obj.SetActive(isShooting);
    }
}
