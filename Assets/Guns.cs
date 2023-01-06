using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class Guns : MonoBehaviour
{
    public PhotonView PV;
    public GameObject bullet;

    public GameObject[] Shot;
    public GameObject[] Roket;

    public void SetShot(GameObject[] Guns)
    {
        Clear_AR(Shot);
        Shot = new GameObject[Guns.Length];
        for (int i = 0; i < Guns.Length; i++)
        {
            Shot[i] = Guns[i];
        }


    }
    public void SetRoket(GameObject[] Launchers)
    {
        Clear_AR(Roket);
        Roket = new GameObject[Launchers.Length];
        for (int i = 0; i < Launchers.Length; i++)
        {
            Roket[i] = Launchers[i];
        }


    }
    public void Clear_AR(GameObject[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = null;
        }
    }



    public float cooldown;
    public float cooldown2;

    public float cooldown3;

    public float cooltime3 = 5.0f;
    public float cooltime2 = 1.0f;
    public float cooltime = 0.2f;
    public Vector3 Shotter;
    public GameObject bullet2;
    public int misilecount;
    public enum Weapon_mode
    {
        Mode1,
        Mode2,
        Mode3

    }
    public Weapon_mode Player_Weapon_Mode = Weapon_mode.Mode1;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }

    public void cooldowns()
    {
        cooldown = Mathf.Clamp(cooldown -= Time.deltaTime, 0, cooltime);
        cooldown2 = Mathf.Clamp(cooldown2 -= Time.deltaTime, 0, cooltime2);
        cooldown3 = Mathf.Clamp(cooldown3 -= Time.deltaTime, 0, cooltime3);
    }
    private float time = 10;
    private void Update()
    {
        cooldowns();
        if (PV.IsMine)
        {

            if (Input.GetMouseButtonDown(0))
            {

                switch (Player_Weapon_Mode)
                {
                    case Weapon_mode.Mode1:
                        if (cooldown == 0)
                        {


                            PV.RPC("Spawn", RpcTarget.All);

                            cooldown = cooltime;
                        }
                        break;
                    case Weapon_mode.Mode2:
                        if (cooldown3 == 0)
                        {


                            PV.RPC("Spawn", RpcTarget.All);
                            if (misilecount == Roket.Length)
                            {
                                cooldown3 = cooltime3;
                            }
                            if (misilecount == Roket.Length)
                            {
                                misilecount = 0;
                            }
                        }
                        break;
                    case Weapon_mode.Mode3:
                        if (cooldown2 == 0)
                        {


                            PV.RPC("Spawn", RpcTarget.All);

                            cooldown2 = cooltime2;
                        }
                        break;
                }



            }
            if (Input.GetMouseButton(0))
            {
                switch (Player_Weapon_Mode)
                {
                    case Weapon_mode.Mode1:
                        if (cooldown == 0)
                        {


                            PV.RPC("Spawn", RpcTarget.All);

                            cooldown = cooltime;
                        }
                        break;
                    case Weapon_mode.Mode2:
                        /*
                        if (cooldown3 == 0)
                        {

                           
                            PV.RPC("Spawn", RpcTarget.All);
                            if (misilecount == Shot.Length)
                            {
                                cooldown3 = cooltime3;
                            }
                            if (misilecount == Shot.Length)
                            {
                                misilecount = 0;
                            }

                        }*/
                        break;
                    case Weapon_mode.Mode3:
                        if (cooldown2 == 0)
                        {


                            PV.RPC("Spawn", RpcTarget.All);

                            cooldown2 = cooltime2;
                        }
                        break;
                }
            }
         
        }
    }
    IEnumerator SpawnBullet()
    {
        yield return new WaitForSeconds(3);
        Spawn();
    }




    [PunRPC]
    public void Spawn()
    {
        for (int i = 0; i < Shot.Length; i++)
        {
            Shotter = Shot[i].transform.position;
            if (Player_Weapon_Mode == Weapon_mode.Mode1)
            {
                Debug.Log(transform.rotation + "로테이션");
                Debug.Log(this.transform.localRotation.x + "  " + this.transform.localRotation.y + "  " + this.transform.localRotation.z + this.transform.localRotation.w + "테스트 2");
                Instantiate(bullet, Shotter, transform.rotation);
            }


        }
        if (Player_Weapon_Mode == Weapon_mode.Mode2)
        {
            if (Roket.Length != 0)
            {
                Shotter = Roket[misilecount++].transform.position;

                Instantiate(bullet2, Shotter, transform.rotation);
            }
        }
        if (Player_Weapon_Mode == Weapon_mode.Mode3)
        {
            for (int i = 0; i < Shot.Length; i++)
            {
                Shotter = Shot[i].transform.position;
                if (Player_Weapon_Mode == Weapon_mode.Mode3)
                {
                    for (int j = 0; j < 5; j++)
                    {

                        GameObject test_bullet = Instantiate(bullet, Shotter, Quaternion.identity);
                        Quaternion test = Quaternion.identity;
                        test.Set(this.transform.rotation.x + Random.Range(-0.02f, 0.02f), this.transform.rotation.y + Random.Range(-0.02f, 0.02f), this.transform.rotation.z + Random.Range(-0.02f, 0.02f), this.transform.rotation.w + Random.Range(-0.02f, 0.02f));

                        test_bullet.transform.Rotate(test.eulerAngles);
                        Debug.Log(this.transform.localRotation.x + "  " + this.transform.localRotation.y + "  " + this.transform.localRotation.z + " " + this.transform.localRotation.w);

                    }

                }


            }
        }

        /*
         mouseposition.x = Input.mousePosition.x;
         mouseposition.y = Input.mousePosition.y;
         DistanceSM.x = (mouseposition.x - CenterVec.x) / CenterVec.x;
         DistanceSM.y = (mouseposition.y - CenterVec.y) / CenterVec.y;*/

        //Instantiate(bullet, Shotter.position,transform.rotation);
        //Debug.Log("SHIP   x  :" + transform.rotation.x + "    y  :" + transform.rotation.y + "    z  :" + transform.rotation.z);
        // Debug.Log("BULLET   x  :" + Spawned_bullet.transform.rotation.x + "    y  :" + Spawned_bullet.transform.rotation.y + "    z  :" + Spawned_bullet.transform.rotation.z);

        //Spawned_bullet.
    }

    // Start is called before the first frame update

}
