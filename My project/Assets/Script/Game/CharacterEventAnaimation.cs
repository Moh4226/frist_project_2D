using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEventAnaimation : MonoBehaviour
{
    private Character m_character;

    private void Awake()
    {
        m_character = GetComponentInParent<Character>();
    }

    private void OnShoot()
    { 
        m_character.Shoot();
    }
}
