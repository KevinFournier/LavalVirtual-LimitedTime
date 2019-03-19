using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    public string nameParameters;
    public bool status;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetBool(nameParameters, status);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
