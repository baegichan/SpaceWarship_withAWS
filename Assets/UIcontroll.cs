using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class UIcontroll : MonoBehaviour
{
    /// <summary>
    /// 0번 로그인 성공
    /// 1번 로그인 실패
    /// 2번 회원가입 성공
    /// 3번 회원가입 실패
    /// 4번 버전 오류
    /// </summary>
    public UnityEvent[] UIevents;
    public void obj_off(GameObject obj)
    {
        obj.SetActive(false);
    }
    public void obj_on(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }
    
    public IEnumerator UI_alphadown(Image obj)
    {
         
        yield return new WaitForSeconds(Time.deltaTime);
        if(obj.color.a<=0)
        {
            obj.gameObject.SetActive(false);
        }
        else
        {
            obj.color = new Color(obj.color.r, obj.color.g, obj.color.b, obj.color.a-0.01f);
            StartCoroutine(UI_alphadown(obj));
        }
    }
    
}
