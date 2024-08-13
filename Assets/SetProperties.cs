using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SetProperties : MonoBehaviour
{
    private VisualEffect vfx;
    // Start is called before the first frame update
    void Start()
    {
        vfx = GetComponent<VisualEffect>();
        vfx.enabled = false;
    }


    [ContextMenu("Stop")]
    void Stop()
    {
        vfx.enabled = false;
    }

    [ContextMenu("Play")]
    void Play()
    {
        vfx.enabled = true;
        vfx.Play();
    }
}
