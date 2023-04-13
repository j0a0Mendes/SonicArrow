using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresspassDetector : MonoBehaviour
{
    
    public void Update()
    {
        /**RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            GameObject otherObject = hit.collider.gameObject;
            // do something with the other object
            
            int layer = otherObject.layer;
            Debug.Log(LayerMask.LayerToName(layer));
        }**/
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;
        Debug.Log("Collided with " + collidedObject.layer);

        // Do something else in response to the collision
    }

}
