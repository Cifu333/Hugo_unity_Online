using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{


    [SerializeField]
    private float speed = 10f;
    private Rigidbody2D rb;

    private PhotonView pv;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pv = GetComponent<PhotonView>();
    }

    private void Update()
    {
        rb.velocity =new Vector3(speed,0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pv.RPC("NetworkDestroy", RpcTarget.All);
        collision.gameObject.GetComponent<Character>().Damage();
    }

    [PunRPC]
    public void NetworkDestroy()
    {
        Destroy(this.gameObject);
    }
}
