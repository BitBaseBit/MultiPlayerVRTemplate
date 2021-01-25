using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject UI_VRMenuGameObj;
    public GameObject UI_OpenWorldsGameObj;

    // Start is called before the first frame update
    void Start()
    {
        UI_VRMenuGameObj.SetActive(false);
        UI_OpenWorldsGameObj.SetActive(false);
    }

    public void OnWorldsButtonClicked()
    {
        if (UI_OpenWorldsGameObj != null)
        {
            UI_OpenWorldsGameObj.SetActive(true);
        }
    }

    public void OnGoHomeButtonClicked()
    {
        Debug.Log("Go Home Button Clicked");
    }

    public void OnChangeAvatarButtonClicked()
    {
        Debug.Log("Change avatar button clicked");
    }
}
