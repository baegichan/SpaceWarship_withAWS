using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetManager : MonoBehaviour
{
    public List<Targetinfo> targets;

    public static TargetManager s_targetmanager;
    private void Awake()
    {
        if(s_targetmanager == null)
        {
            s_targetmanager = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void CreateTargetImg(GameObject targetobj)
    {
        GameObject img = (GameObject)Instantiate(Resources.Load("TargetImage"),this.transform);
        Targetinfo info = img.GetComponent<Targetinfo>();
        targets.Add(info);
        targetobj.GetComponent<Player>().deadevnet += info.Delete_targetui;
        info.Set_Targetinfo(Player.mainplayer.gameObject,targetobj,img.GetComponent<Image>());
    }
  
    public void Update_MainplayerUI()
    {
      foreach (Targetinfo i in targets)
      {
            i.Update_MainPlayer(Player.mainplayer.gameObject);
      }
    }
    public void Destroy_TargetUI(Targetinfo target)
    {
        targets.Remove(target);
    }
}
