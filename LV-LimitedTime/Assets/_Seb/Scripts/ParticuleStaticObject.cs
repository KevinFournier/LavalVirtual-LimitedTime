using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[ExecuteInEditMode]
public class ParticuleStaticObject : MonoBehaviour {
	[Header("Particules")]
	public GameObject ObjetMesh;
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

	Mesh MeshToCopy;
	MeshRenderer MeshRendererToCopy;

	ParticleSystem.MainModule ModuleMain;
	ParticleSystem.EmissionModule ModuleEmission;
	ParticleSystem.ShapeModule ModuleShape;
	ParticleSystem.ExternalForcesModule ModuleExternalForce;
	ParticleSystem.SizeOverLifetimeModule ModuleSizeOverLifetime;

	ParticleSystemRenderer ParticleSystemRenderer;

	#region ParametresUpdate
	float oldStartSize;
	float oldRateOverTime;
	float oldWindZoneMultiplier;
	int oldLimiteMax;
	Color oldColor1;
	Color oldColor2;
	Vector2 oldstartLifetime;
	ParticleSystemMeshShapeType OldMeshShapeType;

	#endregion


	private void OnEnable() {
		Setup();
	}

	// Start is called before the first frame update
	[ContextMenu("Setup")]
	void Setup() {

		if (ObjetMesh == null)
			ObjetMesh = transform.parent.gameObject;

		MeshToCopy = ObjetMesh.GetComponent<MeshFilter>().sharedMesh;
		MeshRendererToCopy = ObjetMesh.GetComponent<MeshRenderer>();
		ParticleSystemRenderer = GetComponent<ParticleSystemRenderer>();

		Particules = GetComponent<ParticleSystem>();
		ParticleSystem.MinMaxCurve minMax = new ParticleSystem.MinMaxCurve(startLifetime.x, startLifetime.y);

		//
		//MAIN MODULE
		ModuleMain = Particules.main;
		ModuleMain.startSpeed = startSpeed;
		ModuleMain.startLifetime = minMax;
		ModuleMain.maxParticles = LimitMax;
		ParticleSystem.MinMaxGradient minMaxGradient = new ParticleSystem.MinMaxGradient(Couleur1, Couleur2);
		ModuleMain.startColor = minMaxGradient;
		ModuleMain.simulationSpace = ParticleSystemSimulationSpace.World;
		//ModuleMain.customSimulationSpace = transform;
		ModuleMain.startSize = startSize;

		//
		// EMISSION MODULE
		ModuleEmission = Particules.emission;
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
		ModuleShape.shapeType = ParticleSystemShapeType.MeshRenderer;
		ModuleShape.meshShapeType = ParticleSystemMeshShapeType.Vertex;
		ModuleShape.useMeshColors = false;
		ModuleShape.meshRenderer = MeshRendererToCopy;
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


	}


	void Start() {
		oldStartSize = startSize;

		Setup();
	}

	// Update is called once per frame
	void Update() {
		if (Particules == null) { Setup(); }

		if (startSize != oldStartSize) {
			ModuleMain.startSize = startSize;
			oldStartSize = startSize;
		}

		if (RateOverTIme != oldRateOverTime) {
			ModuleEmission.rateOverTime = RateOverTIme;
			oldRateOverTime = RateOverTIme;
		}


		if (Couleur1 != oldColor1 || Couleur2 != oldColor2) {
			ParticleSystem.MinMaxGradient minMaxGradient = new ParticleSystem.MinMaxGradient(Couleur1, Couleur2);
			ModuleMain.startColor = minMaxGradient;
			oldColor1 = Couleur1;
			oldColor2 = Couleur2;
		}

		if (MeshShapeType != OldMeshShapeType) {
			OldMeshShapeType = MeshShapeType;
			ModuleShape.meshShapeType = MeshShapeType;
		}
		if (oldWindZoneMultiplier != WindZoneMultiplier) {
			ModuleExternalForce.multiplier = WindZoneMultiplier;
			oldWindZoneMultiplier = WindZoneMultiplier;
		}

		if (oldLimiteMax != LimitMax) {
			oldLimiteMax = LimitMax;
			ModuleMain.maxParticles = LimitMax;
			//Setup();
		}

		if (oldstartLifetime != startLifetime) {
			ParticleSystem.MinMaxCurve minMax = new ParticleSystem.MinMaxCurve(startLifetime.x, startLifetime.y);
			ModuleMain.startLifetime = minMax;
			oldstartLifetime = startLifetime;
		}
	}
}
