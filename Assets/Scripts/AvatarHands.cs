using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarHands : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;

    public static AvatarHands Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
