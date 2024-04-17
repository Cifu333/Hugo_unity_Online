using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Character : MonoBehaviour, IPunObservable
{
    [Header("stats")]
    [SerializeField]
    private float speed = 600f;

    [SerializeField]
    private float jumpforce = 400f;


private Rigidbody2D rb;
    private float desiredMovementAxix = 0f;

    private PhotonView pv;
    private Vector3 enemyPosition = Vector3.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pv = GetComponent<PhotonView>();

        PhotonNetwork.SerializationRate = 20;
    }

    private void Update()
    {
        if(pv.IsMine)
        {
            CheckInputs();
        }
        else
        {
            SmootReplicate();
        }

       
    }

    private void SmootReplicate()
    {
        transform.position = Vector3.Lerp(transform.position, enemyPosition,Time.deltaTime * 20);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(desiredMovementAxix * Time.deltaTime * speed,rb.velocity.y);
    }

    private void CheckInputs()
    {
        desiredMovementAxix = InputManagers.inputManagerInstance.GetLeftAxisUpdate().x;

        if (InputManagers.inputManagerInstance.GetJump()==0 && Mathf.Approximately(rb.velocity.y, 0f))
        {
            rb.AddForce(new Vector2(0f, jumpforce));
        }

        if(InputManagers.inputManagerInstance.GetShoot()==0) 
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        PhotonNetwork.Instantiate("Bullet", transform.position + new Vector3(1f, 0f,0f), Quaternion.identity);
    }

    public void Damage()
    {
        pv.RPC("NetworkDamage", RpcTarget.All);
    }

    [PunRPC]
    private void NetworkDamage()
    {
        Destroy(this.gameObject);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else if(stream.IsReading) 
        {
            enemyPosition = (Vector3)stream.ReceiveNext();
        }
    }
}
