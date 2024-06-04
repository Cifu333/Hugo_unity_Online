using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Runtime.CompilerServices;
using Photon.Pun.Demo.PunBasics;
using UnityEngine.UI;
using TMPro;

public class Character : MonoBehaviour, IPunObservable
{
    [Header("stats")]

    [SerializeField]
    private SpriteRenderer rend;


    [SerializeField]
    private float speed;

    [SerializeField]
    private float jumpforce;

    [SerializeField]
    private float bulletSped;

    [SerializeField]
    private float health;

    [SerializeField]
    private float fireSpeed;

    [SerializeField]
    private float damage;



    [SerializeField]
    private TextMeshProUGUI playerName;

 
    private string myName;
    private int myRace;
    private bool isGrounded;

    private PhotonView pv;

    private Rigidbody2D rb;
    private float desiredMovementAxix = 0f;

    //private Vector3 enemyPosition = Vector3.zero;
    //private string enemyName;
   // private int enemyRace;
   // private float enemyDirection;

    public Transform BulletSpawnPoint;
    //
    private string otherPlayerName;
    private int otherRaceId;
    private float otherDirX;
    private Vector3 enemyPosition = Vector3.zero;
    private bool isDamageOther;
    private float otherHealth;
  


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pv = GetComponent<PhotonView>();

        PhotonNetwork.SerializationRate = 20;
    }

    private void Update()
   {
      

        Debug.Log(myRace);

        if (pv.IsMine)
        {
            
            playerName.text = myName;

            if (myRace == 1)
            {
                rend.color = Color.gray;
            }
            else
            {
                rend.color= Color.red;
            }

            //signar los valores de las razas
            for (int i = 0; i < Network_Manager._NETWORK_MANAGER.GetRacesList().Races.Count; i++)
            {
                if (Network_Manager._NETWORK_MANAGER.GetRacesList().Races[i].idRace == myRace)
                {
                    health = Network_Manager._NETWORK_MANAGER.GetRacesList().Races[i].life;
                    fireSpeed = Network_Manager._NETWORK_MANAGER.GetRacesList().Races[i].ratefire;
                    damage = Network_Manager._NETWORK_MANAGER.GetRacesList().Races[i].damage;
                    jumpforce = Network_Manager._NETWORK_MANAGER.GetRacesList().Races[i].jump;
                    speed = Network_Manager._NETWORK_MANAGER.GetRacesList().Races[i].velocity;
                }
            }

            PlayerAnimations();
            CheckInputs();
        }
        else
        {
           

           // playerName.text = enemyName;


            if (otherRaceId == 1)
            {
                rend.color = Color.grey;
            }
            else
            {
                rend.color = Color.red;
            }

            //Asignar los valores de las razas
            for (int i = 0; i < Network_Manager._NETWORK_MANAGER.GetRacesList().Races.Count; i++)
            {
                if (Network_Manager._NETWORK_MANAGER.GetRacesList().Races[i].idRace == otherRaceId)
                {
                    health = Network_Manager._NETWORK_MANAGER.GetRacesList().Races[i].life;
                    fireSpeed = Network_Manager._NETWORK_MANAGER.GetRacesList().Races[i].ratefire;
                    damage = Network_Manager._NETWORK_MANAGER.GetRacesList().Races[i].damage;
                    jumpforce = Network_Manager._NETWORK_MANAGER.GetRacesList().Races[i].jump;
                    speed = Network_Manager._NETWORK_MANAGER.GetRacesList().Races[i].velocity;
                }
            }
            SmootReplicate();
        }
       
    }
    private void SmootReplicate()
    {
        transform.position = Vector3.Lerp(transform.position, enemyPosition, Time.deltaTime * 20);

        if (otherDirX < 0)
        {
            rend.flipX = true;
        }
        else if (otherDirX > 0)
        {
            rend.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(desiredMovementAxix * Time.deltaTime * speed,rb.velocity.y);
    }

    private void CheckInputs()
    {
        desiredMovementAxix = InputManagers.inputManagerInstance.GetLeftAxisUpdate().x;
        Debug.Log(desiredMovementAxix);

        if (InputManagers.inputManagerInstance.GetJump()==0 && Mathf.Approximately(rb.velocity.y, 0f))
        {
            rb.AddForce(new Vector2(0f, jumpforce));
        }

        if(InputManagers.inputManagerInstance.GetShoot()==0) 
        {
            Debug.Log("DISPARO11");
            Shoot();
            Debug.Log("DISPARO");
        }
        if (InputManagers.inputManagerInstance.GetLeftAxisUpdate().x < 0)
        {
            rend.flipX = true;
        }
        else if (InputManagers.inputManagerInstance.GetLeftAxisUpdate().x > 0)
        {
            rend.flipX = false;
        }
    }

    private void Shoot()
    {
      

        if (rend.flipX == false)
        {
           GameObject temp=  PhotonNetwork.Instantiate("Bullet", transform.position + new Vector3(1f, 0f, 0f), Quaternion.identity);
            BulletScript bulletTemp = temp.GetComponent<BulletScript>();

            bulletTemp.SetVelocity(1);
          



        }
        else if (rend.flipX == true)
        {
            GameObject temp = PhotonNetwork.Instantiate("Bullet", transform.position + new Vector3(-1f, 0f, 0f), Quaternion.identity);
            BulletScript bulletTemp = temp.GetComponent<BulletScript>();

            bulletTemp.SetVelocity(-1);
           

        }

    }

    public void Damage()
    {
        pv.RPC("NetworkDamage", RpcTarget.All);
        otherHealth -= 10;
    }

   

    [PunRPC]
    private void NetworkDamage()
    {
        Destroy(this.gameObject);
    }

    public void PlayerAnimations()
    {
        //if (InputManagers.inputManagerInstance.GetJump() == 0)
        //{
        //    isJumping = true;
        //}
        //else
        //{
        //    isJumping = false;
        //}


        //if (InputManagers.inputManagerInstance.GetShoot() == 0)
        //{
        //    isShooting = true;
        //}
        //else
        //{
        //    isShooting = false;
        //}

        //m_Animator.SetBool("IsMoving", InputManagers.inputManagerInstance.GetLeftAxisPressed());

        //m_Animator.SetBool("IsJumping", isJumping);

        //m_Animator.SetBool("IsShooting", isShooting);

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(myName);
            stream.SendNext(myRace);
            stream.SendNext(InputManagers.inputManagerInstance.GetLeftAxisUpdate().x);
            //stream.SendNext(isDamage);
            stream.SendNext(health);
        }
        else if (stream.IsReading)
        {
            enemyPosition = (Vector3)stream.ReceiveNext();
            otherPlayerName = (string)stream.ReceiveNext();
            otherRaceId = (int)stream.ReceiveNext();
            otherDirX = (float)stream.ReceiveNext();
            isDamageOther = (bool)stream.ReceiveNext();
            otherHealth = (float)stream.ReceiveNext();
           
        }
    }
    

}
