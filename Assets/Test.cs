using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private TextMeshProUGUI playerName;
    [Header("Stats")]
    [SerializeField] private float speed = 0f;
    [SerializeField] private float jumpForce = 0;
    private float health;
    private float fireSpeed;
    private float damage;

    private Rigidbody2D rb;
    private float desiredMovementAxis = 0f;


    private PhotonView pv;
    private Vector3 enemyPosition = Vector3.zero;

    private whitelist race;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pv = GetComponent<PhotonView>();

        playerName.text = Photon_Manager.instance.PlayerName();

        PhotonNetwork.SendRate = 20;
        PhotonNetwork.SerializationRate = 20;


        for (int i = 0; i < Network_Manager._NETWORK_MANAGER.GetRacesList().Races.Count; i++)
        {
            if (Network_Manager._NETWORK_MANAGER.GetRacesList().Races[i].idRace == Photon_Manager.instance.Raceid())
            {
                speed = Network_Manager._NETWORK_MANAGER.GetRacesList().Races[i].velocity;
                health = Network_Manager._NETWORK_MANAGER.GetRacesList().Races[i].life;
                fireSpeed = Network_Manager._NETWORK_MANAGER.GetRacesList().Races[i].ratefire;
                jumpForce = Network_Manager._NETWORK_MANAGER.GetRacesList().Races[i].jump;
                damage = Network_Manager._NETWORK_MANAGER.GetRacesList().Races[i].damage;

            }
        }
    }
    private void Update()
    {
        Debug.Log("Speed = " + speed + " Jump = " + jumpForce);

        if (pv.IsMine)
        {
            CheckInputs();
        }
        else
        {
            SmoothReplicate();
        }

        if (InputManagers.inputManagerInstance.GetLeftAxisUpdate().x < 0)
        {
            sr.flipX = true;
        }
        else if (InputManagers.inputManagerInstance.GetLeftAxisUpdate().x > 0)
        {
            sr.flipX = false;
        }
    }

    private void SmoothReplicate()
    {
        transform.position = Vector3.Lerp(transform.position, enemyPosition, Time.deltaTime * 20);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(desiredMovementAxis * Time.fixedDeltaTime * speed, rb.velocity.y);
    }

    private void CheckInputs()
    {

        desiredMovementAxis = InputManagers.inputManagerInstance.GetLeftAxisUpdate().x;

        if (InputManagers.inputManagerInstance.GetJump() == 0)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
        }

        if (InputManagers.inputManagerInstance.GetShoot() == 0)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        PhotonNetwork.Instantiate("Circle", transform.position + new Vector3(1, 0, 0), Quaternion.identity);
    }


    public void Damage()
    {
        pv.RPC("NetworkDamage", RpcTarget.All);
    }

    [PunRPC]
    public void NetworkDamage()
    {
        Destroy(this.gameObject);

        //To doo hacer que te quite vida en vez de matar si la vida llega a 0 o menor que zero destruir
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else if (stream.IsReading)
        {
            enemyPosition = (Vector3)stream.ReceiveNext();
        }
    }
}