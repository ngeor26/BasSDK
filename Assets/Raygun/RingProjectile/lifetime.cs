using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifetime : MonoBehaviour
{
    public float time = 6.0f;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
