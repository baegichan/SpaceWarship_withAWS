using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon;
public class Player : MonoBehaviour
{
    [SerializeField]
    float ShipSpeed;
    public Rigidbody rigid;
    public PhotonView PV;
    private Vector2 mouseposition;
    private Vector2 DistanceSM;
    private Vector2 CenterVec;
    private float Speed_1, Speed_2, Speed_3;
    Quaternion Quate = Quaternion.identity;
    public GameObject Target;
    public Transform[] guns;
    float roll;
    public GameObject[] LightOBJ;
    public bool Light = false;
    int HP = 10000;
    public int _HP
    {
        get 
        { 
        return HP; 
        }
        set {
            if (value <= 0)
            { 
                HP = 0;
                Destroy(gameObject);
                SpawnManager.s_instance.DiePlayer();
            }
            else{ HP = value; }
        }
    }

    public float HeadingSpeed = 2.0f;
    [PunRPC]
    public void Damaged(int damage)
    {
        _HP -= damage;
    }
    public enum State
    {

        None,
        Move,
        Stop
    }
    public State test = State.Move;
    [PunRPC]
    public void Update_location()
    {
       
    }
    public void MovementShip()
    {
        if (PV.IsMine)
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                Light = Light == true ? false : true;

                LIGHTON();


            }

            float X = Input.GetAxis("Horizontal");
            float Y = Input.GetAxis("Vertical");



            mouseposition.x = Input.mousePosition.x;
            mouseposition.y = Input.mousePosition.y;
            DistanceSM.x = (mouseposition.x - CenterVec.x) / CenterVec.x;
            DistanceSM.y = (mouseposition.y - CenterVec.y) / CenterVec.y;

            DistanceSM = Vector2.ClampMagnitude(DistanceSM, 1f);
            roll = Mathf.Lerp(roll, Input.GetAxisRaw("Roll"), 3.5f * Time.deltaTime);
            transform.Rotate(-DistanceSM.y * 90f * Time.deltaTime, DistanceSM.x * 90.0f * Time.deltaTime, roll * 60.0f * Time.deltaTime, Space.Self);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                Speed_1 = Mathf.Lerp(Speed_1, Input.GetAxisRaw("Vertical") * ShipSpeed * 3f, 2.5f * Time.deltaTime);
                Speed_2 = Mathf.Lerp(Speed_2, Input.GetAxisRaw("Horizontal") * ShipSpeed * 3f, 2.5f * Time.deltaTime);
                Speed_3 = Mathf.Lerp(Speed_3, Input.GetAxisRaw("Hover") * ShipSpeed * 3f, 2.5f * Time.deltaTime);
            }
            else
            {
                Speed_1 = Mathf.Lerp(Speed_1, Input.GetAxisRaw("Vertical") * ShipSpeed, 2.5f * Time.deltaTime);
                Speed_2 = Mathf.Lerp(Speed_2, Input.GetAxisRaw("Horizontal") * ShipSpeed, 2.5f * Time.deltaTime);
                Speed_3 = Mathf.Lerp(Speed_3, Input.GetAxisRaw("Hover") * ShipSpeed, 2.5f * Time.deltaTime);
            }
            
            Vector3 i_forward = transform.position += transform.forward * Speed_1 * Time.deltaTime;
            Vector3 i_right = transform.position += transform.right * Speed_2 * Time.deltaTime + transform.up * Speed_3 * Time.deltaTime;
            rigid.MovePosition(new Vector3(Mathf.Clamp(i_forward.x, 0, Mapdata.s_instance.end_point), Mathf.Clamp(i_right.y, 0, Mapdata.s_instance.end_point), Mathf.Clamp(i_right.z, 0, Mapdata.s_instance.end_point)));
            //transform.position = new Vector3(Mathf.Clamp(i_forward.x, 0, Mapdata.s_instance.end_point), Mathf.Clamp(i_right.y, 0, Mapdata.s_instance.end_point), Mathf.Clamp(i_right.z, 0, Mapdata.s_instance.end_point));

            //transform.position = new Vector3(Mathf.Clamp(i_right.x, 0, Mapdata.s_instance.end_point), Mathf.Clamp(i_right.y, 0, Mapdata.s_instance.end_point), Mathf.Clamp(i_right.z, 0, Mapdata.s_instance.end_point));
            //transform.position += transform.forward * Speed_1 * Time.deltaTime;
            //transform.position += transform.right * Speed_2 * Time.deltaTime + transform.up * Speed_3 * Time.deltaTime;

        }



    }

    public void LIGHTON()
    {
        if (Light == true)
        {
            for (int i = 0; i < LightOBJ.Length; i++)
            {
                LightOBJ[i].SetActive(true);
            }



        }
        else
        {
            for (int i = 0; i < LightOBJ.Length; i++)
            {
                LightOBJ[i].SetActive(false);
            }
        }
    }



    #region DeadBlock
    /*
    if (Input.GetAxis("Horizontal") == 0)
    {
    }
    else if (Input.GetAxis("Horizontal") > 0)
    {
        transform.Translate(Vector3.right * ShipSpeed * Time.deltaTime);
    }
    else
    {
        transform.Translate(Vector3.left * ShipSpeed * Time.deltaTime);
    }
    if (Input.GetAxis("Vertical") == 0)
    {
    }
    else if (Input.GetAxis("Vertical") > 0)
    {
        transform.Translate(Vector3.forward * ShipSpeed * Time.deltaTime);
    }
    else
    {
        transform.Translate(Vector3.back * ShipSpeed * Time.deltaTime);
    }
    if(Input.GetKey(KeyCode.Space))
    {
        transform.Translate(Vector3.up * ShipSpeed * Time.deltaTime);
    }
    if(Input.GetKey(KeyCode.LeftControl))
    {
        transform.Translate(Vector3.down * ShipSpeed * Time.deltaTime);
    }
    if(Input.GetKey(KeyCode.Q))
    {
        //transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 2);
        //lookat 문제임

        // transform.Rotate(transform.rotation.x, transform.rotation.y , transform.rotation.z, Space.World);
        // Quaternion tar = Quaternion.Euler()
        // transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }
    if (Input.GetKey(KeyCode.E))
    {
        // transform.rotation = Quaternion.Euler(transform.eulerAngles.x , transform.eulerAngles.y, transform.eulerAngles.z - 2);
       //lookat 문제임
       // transform.Rotate(transform.rotation.x, transform.rotation.y , transform.rotation.z , Space.World);
       // transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

    }


    //Quaternion target = Quaternion.Euler(0, 0, qe);
    //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime );
    Debug.Log(qe);

    if (Input.mousePosition.x>0)
    {

    }
    if (mouseposition.y != Input.mousePosition.y)
    {
        if(mouseposition.y>Input.mousePosition.y)
        {
            if (Target.transform.localPosition.y > -6) Target.transform.Translate(Vector3.down* Time.deltaTime * HeadingSpeed);
        }
        // transform.eulerAngles = new Vector3(transform.eulerAngles.x + 2, transform.eulerAngles.y, transform.eulerAngles.z);
        //transform.Rotate(new Vector3(transform.rotation.x+(Input.mousePosition.y - mouseposition.y)/10, transform.rotation.y , transform.rotation.z));
        //transform.localRotation=Quaternion.Euler(transform.rotation.x,transform.rotation.y+1,transform.rotation.z);
        else
        {
            if (Target.transform.localPosition.y < 6) Target.transform.Translate(Vector3.up* Time.deltaTime * HeadingSpeed);
            //transform.localRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y- 1, transform.rotation.z);
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x+ 2, transform.eulerAngles.y , transform.eulerAngles.z);
            //  transform.Rotate(new Vector3(transform.rotation.x - (Input.mousePosition.y - mouseposition.y)/10, transform.rotation.y , transform.rotation.z));
        }
    }


    if (mouseposition.x != Input.mousePosition.x)
    {
        if (mouseposition.x > Input.mousePosition.x)
        {
            if (Target.transform.localPosition.x > -6) Target.transform.Translate(Vector3.left*Time.deltaTime * HeadingSpeed);

        }
        else
        {
            if(Target.transform.localPosition.x < 6) Target.transform.Translate(Vector3.right* Time.deltaTime*HeadingSpeed) ;

            // transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y , transform.eulerAngles.z);
            // transform.Rotate(new Vector3(transform.rotation.x , transform.rotation.y + Mathf.Abs((Input.mousePosition.x - mouseposition.x)/10), transform.rotation.z));
        }
    }
    if(Target.transform.localPosition.x <0)
    {
        Target.transform.localPosition = new Vector3(Mathf.Clamp(Target.transform.localPosition.x + Time.deltaTime , -6, 0), Target.transform.localPosition.y, 10);
    }
    else if(Target.transform.localPosition.x > 0)
    {
        Target.transform.localPosition = new Vector3(Mathf.Clamp(Target.transform.localPosition.x - Time.deltaTime , 0, 6), Target.transform.localPosition.y, 10);
    }
    if (Target.transform.localPosition.y < 0)
    {
        Target.transform.localPosition = new Vector3(Target.transform.localPosition.x,Mathf.Clamp(Target.transform.localPosition.y + Time.deltaTime , -6, 0),10);
    }
    else if (Target.transform.localPosition.y > 0)
    {
        Target.transform.localPosition = new Vector3(Target.transform.localPosition.x,Mathf.Clamp(Target.transform.localPosition.y - Time.deltaTime , 0, 6), 10);
    }
    Debug.Log(Target.transform.position);
    var lookpos = Target.transform.position - transform.position;
    lookpos.z = transform.rotation.z + 2;
    var rotation = Quaternion.LookRotation(lookpos);
    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 2);
    //transform.LookAt(new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z));
    mouseposition = Input.mousePosition;
}
}*/
    #endregion
    private void Start()
    {
        Cursor.visible = false;
        CenterVec.x = Screen.width * 0.5f;
        CenterVec.y = Screen.height * 0.5f;
        CameraMove _cameraWork = this.gameObject.GetComponent<CameraMove>();
        if (_cameraWork != null)
        {
            if (PV.IsMine)
            {
                _cameraWork.OnStartFollowing();
            }
        }
        else
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
        }
    }
    private void FixedUpdate()
    {
        MovementShip();

    }

}
