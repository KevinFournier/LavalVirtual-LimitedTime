using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bourasque : MonoBehaviour
{
    public GameObject[] trees;
    public AudioSource rafale;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("RandomWindFlow");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator RandomWindFlow()
    {
        yield return new WaitForSeconds(Random.Range(10f, 20f));
        Debug.Log("Bourasque!");

        rafale.Play();
        for(int i = 0; i < trees.Length; i++)
        {
            trees[i].GetComponentInChildren<NoisyMesh>().Speed = 0.010f;
            trees[i].GetComponentInChildren<ParticuleTree>().WindZoneMultiplier = 2;
        }

        yield return new WaitForSeconds(5.5f);
        for (int i = 0; i < trees.Length; i++)
        {
            trees[i].GetComponentInChildren<NoisyMesh>().Speed = 0.005f;
            trees[i].GetComponentInChildren<ParticuleTree>().WindZoneMultiplier = 0.1f;
        }

        StartCoroutine("RandomWindFlow");
    }
}
