using UnityEngine;
using System.Collections;

public class CollisionPlayerTwo : MonoBehaviour
{
    public float speed = 100000;

    // Use this for initialization
    void OnCollisionEnter(Collision collison)
    {
        if (collison.rigidbody && collison.gameObject.tag == "puk")
        {
            collison.rigidbody.AddForce(Vector3.forward * (-1) * speed * Time.deltaTime, ForceMode.VelocityChange);
        }
    }
}
