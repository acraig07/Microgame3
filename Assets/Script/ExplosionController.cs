using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public AnimationClip _clip;

    void OnEnable()
    {
        Invoke("Disable", _clip.length);
    }
    
    private void Disable()
    {
        Destroy(gameObject);
    }
}
