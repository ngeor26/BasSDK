using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThunderRoad;

public class evaporate : MonoBehaviour
{
   public void OnTriggerEnter(Collider other)
   {
    var creature = other.GetComponentInParent<Creature>();
        if (creature != null)
        {
            Destroy(creature);
        }
    }
}
