using UnityEngine;

public class CamController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset; //kamera ve oyuncu arasýndaki alana offset denir
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()  //daha düz bir hareket için lateupdate
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }


    }
}

