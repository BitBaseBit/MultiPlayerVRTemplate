using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginUIManager : MonoBehaviour
{

    public GameObject connectOptionsPanelGameObj;
    public GameObject connectWithNamePanelGameObj;

    // Start is called before the first frame update
    void Start()
    {
        connectOptionsPanelGameObj.SetActive(true);
        connectWithNamePanelGameObj.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
