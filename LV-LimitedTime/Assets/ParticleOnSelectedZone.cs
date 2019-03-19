using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleOnSelectedZone : MonoBehaviour {

	public Collider Collider;
	public SkinnedMeshRenderer MySkinnedMeshRenderer;

	public Material ParticuleMaterial;
	public ParticleSystem Particules;


	[Header("Parametres:")]
	public int LimitMax = 10000;
	public int RateOverTIme = 10000;
	public Color Couleur1 = Color.magenta;
	public Color Couleur2 = Color.red;
	public ParticleSystemMeshShapeType MeshShapeType;
	[HideInInspector]
	public float startSpeed = 0f;
	[Range(0, 0.3f)]
	public float startSize = 0.01f;
	public Vector2 startLifetime = new Vector2(0.5f, 1);


	Vector3[] ValidPoints;
	int countValidPoint = 0;
	Mesh my_Mesh;

	ParticleSystem.MainModule ModuleMain;
	ParticleSystem.EmissionModule ModuleEmission;
	ParticleSystem.SizeOverLifetimeModule ModuleSizeOverLifetime;

	ParticleSystemRenderer ParticleSystemRenderer;

	// Start is called before the first frame update
	[ContextMenu("Setup")]
	void Start() {
		my_Mesh = new Mesh();

		Particules = GetComponent<ParticleSystem>();
		ParticleSystemRenderer = GetComponent<ParticleSystemRenderer>();

		conteneur = new GameObject("conteneur");
		conteneur.transform.SetParent(transform);

		Setup();
	}


	void Setup() {

		ParticleSystem.MinMaxCurve minMax = new ParticleSystem.MinMaxCurve(startLifetime.x, startLifetime.y);

		//
		//MAIN MODULE
		ModuleMain = Particules.main;
		ModuleMain.startSpeed = startSpeed;
		ModuleMain.startLifetime = minMax;
		ModuleMain.maxParticles = LimitMax;
		//ModuleMain.maxParticles = countValidPoint;
		ParticleSystem.MinMaxGradient minMaxGradient = new ParticleSystem.MinMaxGradient(Couleur1, Couleur2);
		ModuleMain.startColor = minMaxGradient;
		ModuleMain.simulationSpace = ParticleSystemSimulationSpace.World;
		//ModuleMain.customSimulationSpace = transform;
		ModuleMain.startSize = startSize;

		//
		// EMISSION MODULE
		ModuleEmission = Particules.emission;
		ModuleEmission.enabled = true;
		ModuleEmission.rateOverTime = RateOverTIme;

		//
		// SIZE OVER LIFETIME MODULE
		ModuleSizeOverLifetime = Particules.sizeOverLifetime;
		ModuleSizeOverLifetime.enabled = true;
		Keyframe[] KeysMin = { new Keyframe(0, 0), new Keyframe(0.2f, 0.3f), new Keyframe(0.8f, 0.3f), new Keyframe(1, 0) };
		Keyframe[] KeysMax = { new Keyframe(0, 0), new Keyframe(0.2f, 1), new Keyframe(0.8f, 1), new Keyframe(1, 0) };
		AnimationCurve min = new AnimationCurve(KeysMin);
		AnimationCurve max = new AnimationCurve(KeysMax);
		ParticleSystem.MinMaxCurve minMaxCurve = new ParticleSystem.MinMaxCurve(1, min, max);
		ModuleSizeOverLifetime.size = minMaxCurve;

		//
		// RENDERER MODULE
		ParticleSystemRenderer.material = ParticuleMaterial;

	}


	List<Vector3> PointsInZone = new List<Vector3>();
	List<int> PointsInZoneIndex = new List<int>();
	ParticleSystem.Particle[] particlesArray;

	GameObject conteneur;


	// Update is called once per frame
	void FixedUpdate() {
		countValidPoint = 0;

		//Mesh Mesh = new Mesh();
		MySkinnedMeshRenderer.BakeMesh(my_Mesh);


		Destroy(conteneur);


		int index = 0;

		conteneur = new GameObject("conteneur");
		conteneur.transform.SetParent(MySkinnedMeshRenderer.transform.parent);
		conteneur.transform.localPosition = Vector3.zero;
		conteneur.transform.localRotation = Quaternion.Euler(-90, 0, 0);

		foreach (var v in my_Mesh.vertices) {
			if (Collider.bounds.Contains(v)) {
				countValidPoint++;

				GameObject g = GameObject.CreatePrimitive(PrimitiveType.Cube);
				g.transform.SetParent(conteneur.transform);
				g.transform.localScale = Vector3.one * 0.01f;
				g.transform.localPosition = v;


			}
			index++;
		}



		print(countValidPoint);
		/*
		LimitMax = countValidPoint;
		ModuleMain.maxParticles = LimitMax;

		particlesArray = new ParticleSystem.Particle[LimitMax];




		Particules.GetParticles(particlesArray);

		foreach (var item in PointsInZoneIndex) {
			particlesArray[item].position = PointsInZone[item];
		}

		Particules.SetParticles(particlesArray);

	*/

	}
}
