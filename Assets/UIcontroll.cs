using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class UIcontroll : MonoBehaviour
{
    /// <summary>
    /// 0�� �α��� ����
    /// 1�� �α��� ����
    /// 2�� ȸ������ ����
    /// 3�� ȸ������ ����
    /// 4�� ���� ����
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
