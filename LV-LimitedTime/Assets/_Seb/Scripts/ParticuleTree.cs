using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticuleTree : MonoBehaviour
{
	[Header("Particules")]
	public GameObject ObjetMesh;
	public Material ParticuleMaterial;
	public ParticleSystem Particules;

	[Header("Parametres:")]
	public int LimitMax = 10000;
	public int RateOverTIme = 10000;
	public float startSpeed = 0f;
	public float startSize = 0.01f;
	public Vector2 startLifetime = new Vector2(0.5f, 1);
	public float WindZoneMultiplier = 0.1f;

	Mesh MeshToCopy;
	MeshRenderer MeshRendererToCopy;

	ParticleSystem.MainModule ModuleMain;
	ParticleSystem.EmissionModule ModuleEmission;
	ParticleSystem.ShapeModule ModuleShape;
	ParticleSystem.ExternalForcesModule ModuleExternalForce;

	ParticleSystemRenderer ParticleSystemRenderer;

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
		ModuleMain.simulationSpace = ParticleSystemSimulationSpace.World;
		//ModuleMain.customSimulationSpace = transform;
		ModuleMain.startSize = startSize;

		//
		// EMISSION MODULE
		ModuleEmission = Particules.emission;
		ModuleEmission.rateOverTime = RateOverTIme;
		//
		// SHAPE MODULE
		ModuleShape = Particules.shape;
		ModuleShape.shapeType = ParticleSystemShapeType.MeshRenderer;
		ModuleShape.meshShapeType = ParticleSystemMeshShapeType.Vertex;
		ModuleShape.useMeshColors = false;
		ModuleShape.meshRenderer = MeshRendererToCopy;

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


    void Start()
    {
		Setup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
