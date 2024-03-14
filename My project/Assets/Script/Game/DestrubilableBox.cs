using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestrubilableBox : MonoBehaviour
{
    // Start is called before the first frame update
    public void Destruct() { 
        Destroy(gameObject);
    
    }
}
