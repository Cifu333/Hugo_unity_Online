using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletScript : MonoBehaviourPun
{
    [SerializeField] private float speed = 20f;

    private Rigidbody2D rb;
    private PhotonView pv;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pv = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pv.RPC("NetworkDestory", RpcTarget.All);

        collision.gameObject.GetComponent<Character>().Damage();
    }

    [PunRPC]
    public void NetworkDestory()
    {
        Destroy(this.gameObject);
    }

    public void SetVelocity(float dir)
    {
        rb.velocity = new Vector2(speed * dir, 0);
    }
}
