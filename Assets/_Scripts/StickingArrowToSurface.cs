using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickingArrowToSurface : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private SphereCollider myCollider;

    [SerializeField]
    private GameObject stickingArrow;

    [SerializeField]
    private AudioSource windNavigatingSound;

    //private bool notFlying = false;

    private void Start() 
    { 
        windNavigatingSound.Play();  
    }

    //private void Update() 
    //{ 
    //    if (notFlying)
    //
    //        windNavigatingSound.Stop();
    //    }   
    //}

    private void OnCollisionEnter(Collision collision)
    {
        windNavigatingSound.Stop();

        rb.isKinematic = true;
        myCollider.isTrigger = true;

        GameObject arrow = Instantiate(stickingArrow);
        arrow.transform.position = transform.position;
        arrow.transform.forward = transform.forward;

        if (collision.collider.attachedRigidbody != null)
        {
            arrow.transform.parent = collision.collider.attachedRigidbody.transform;
        }

        collision.collider.GetComponent<IHittable>()?.GetHit();

        Destroy(gameObject);

    }
}
