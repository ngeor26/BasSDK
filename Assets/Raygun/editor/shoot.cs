using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public GameObject projectile;

    private ParticleSystem shootEffect;

    private AudioSource shootSound;

    // Start is called before the first frame update
    void Start()
    {
        shootEffect = GetComponent<ParticleSystem>();
        shootSound = GetComponent<AudioSource>();
    }

    [ContextMenu("Shoot")]
    public void Shoot()
    {
        GameObject clone;
        clone = Instantiate(projectile, transform.position, Quaternion.identity);
        clone.SetActive(true);
        clone.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * 10);
        shootSound.Play();
        if(!shootEffect.isPlaying){
            shootEffect.Play();
        }
    }
}
