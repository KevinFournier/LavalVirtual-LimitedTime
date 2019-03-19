using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmprunteColor : MonoBehaviour
{
	public ParticleSystem Particle01;
	public ParticleSystem Particle02;

	public MeshRenderer Emprunte;

	public Color Couleur = Color.yellow;

	Color oldCoulor;

    // Start is called before the first frame update
    void Start()
    {
		oldCoulor = Couleur;

		ParticleSystem.MainModule MainModule1 = Particle01.main;
		MainModule1.startColor = Couleur;

		ParticleSystem.MainModule MainModule2 = Particle02.main;
		MainModule2.startColor = Couleur;

		Emprunte.material.SetColor("_EmissionColor", Couleur);

	}

	// Update is called once per frame
	void Update()
    {
        if(oldCoulor != Couleur) {
			Start();
		}

    }
}
