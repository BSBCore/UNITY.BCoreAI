using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationReplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("r")) {
            print("Animation replay");
            this.GetComponent<Animator>().Play("Construction",0,0f);
        }
    }
}
