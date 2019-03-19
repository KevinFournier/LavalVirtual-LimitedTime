using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	public float WindZoneMultiplier = 0.1f;


	Vector3[] ValidPoints;
	int countValidPoint = 0;
	Mesh Mesh;

	GameObject ParticleHolder;

	ParticleSystem.MainModule ModuleMain;
	ParticleSystem.EmissionModule ModuleEmission;
	ParticleSystem.ShapeModule ModuleShape;
	ParticleSystem.ExternalForcesModule ModuleExternalForce;
	ParticleSystem.SizeOverLifetimeModule ModuleSizeOverLifetime;

	ParticleSystemRenderer ParticleSystemRenderer;

	// Start is called before the first frame update
	[ContextMenu("Setup")]
	void Start() {
		Mesh = new Mesh();

		ParticleHolder = new GameObject("ParticleHolder");
		ParticleHolder.transform.SetParent(MySkinnedMeshRenderer.transform.parent);
		ParticleHolder.transform.rotation = Quaternion.Euler(-90, 0, 0);
		Particules = ParticleHolder.AddComponent<ParticleSystem>();
		ParticleSystemRenderer = ParticleHolder.GetComponent<ParticleSystemRenderer>();

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
		// SHAPE MODULE
		ModuleShape = Particules.shape;
		ModuleShape.enabled = true;
		ModuleShape.shapeType = ParticleSystemShapeType.Mesh;
		ModuleShape.meshShapeType = ParticleSystemMeshShapeType.Vertex;
		ModuleShape.useMeshColors = false;
		ModuleShape.mesh = Mesh;
		ModuleShape.meshShapeType = MeshShapeType;
		//
		// RENDERER MODULE
		ParticleSystemRenderer.material = ParticuleMaterial;

		//
		// EXTERNAL FORCE MODULE
		ModuleExternalForce = Particules.externalForces;
		ModuleExternalForce.enabled = true;
		ModuleExternalForce.influenceFilter = ParticleSystemGameObjectFilter.LayerMask;
		ModuleExternalForce.multiplier = WindZoneMultiplier;

		countParticle();
	}

	List<Vector3> PointsInZone = new List<Vector3>();
	List<int> PointsInZoneIndex = new List<int>();
	ParticleSystem.Particle[] particlesArray;

	void countParticle() {

		countValidPoint = 0;
		for (int i = 0 ; i < Mesh.vertexCount; i++) {
			var v = Mesh.vertices[i];

			if (Collider.bounds.Contains(v)) {

				countValidPoint++;
				PointsInZone.Add(v);
				PointsInZoneIndex.Add(i);
				/*
				GameObject g = GameObject.CreatePrimitive(PrimitiveType.Cube);
				g.transform.SetParent(ParticleHolder.transform);
				g.transform.localScale = Vector3.one * 0.01f;
				g.transform.localPosition = v;
				*/
			}
		}
		LimitMax = countValidPoint;
		ModuleMain.maxParticles = LimitMax;

		particlesArray = new ParticleSystem.Particle[LimitMax];
	}


	// Update is called once per frame
	void FixedUpdate() {

		MySkinnedMeshRenderer.BakeMesh(Mesh);

		Particules.GetParticles(particlesArray);

		foreach (var item in PointsInZoneIndex) {
			particlesArray[item].position = PointsInZone[item];
		}

		Particules.SetParticles(particlesArray);


			
		//print(countValidPOint);
	}
}
