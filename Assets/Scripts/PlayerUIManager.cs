using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public GameObject VRMenuGameObj;
    public GameObject GoHomeButtonGameObj;

    // Start is called before the first frame update
    void Start()
    {
        VRMenuGameObj.SetActive(false);
        GoHomeButtonGameObj.GetComponent<Button>().onClick.AddListener(VirtualWorldManager.Instance.LeaveRoomAndLoadHomeScene);
    }

}
