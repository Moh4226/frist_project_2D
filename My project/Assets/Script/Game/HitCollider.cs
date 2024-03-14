using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitCollider : MonoBehaviour
{
    [SerializeField] private UnityEvent m_OnHit;
    // Start is called before the first frame update
    public void HandleHit() { 
        m_OnHit.Invoke();

    
    
    }
}
