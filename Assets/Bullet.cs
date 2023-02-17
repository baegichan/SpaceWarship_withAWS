using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update\
    public int damage = 1000;
    public GameObject Effect;
    public GameObject Effect2;
    public int Speed;
    public float DeleteTime = 5;
    void Start()
    {
        
        StartCoroutine(Destroy());
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(DeleteTime);
        Destroy(this.gameObject);
    }
    //나아가는 기능
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector3(0, 0, 1) * Speed * Time.deltaTime, Space.Self);
        if (damage>500)
        {
            damage -= 10;
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        //부딪힐때 데미지
        if(collision.gameObject.GetComponent<Player>())
        {
            Player i_player = collision.gameObject.GetComponent<Player>();
            if(i_player.PV.IsMine==true)
            {
                Debug.Log("damaged " + damage);
                i_player.Damaged(damage);
                Destroy(gameObject);
               
            }
            
        }
    }
}
