using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticuleTree_v2 : MonoBehaviour
{
	[Header("Particules")]
	public GameObject ObjetMesh;
	public Material ParticuleMaterial;
	public ParticleSystem Particules;

	[Header("Parametres:")]
	public int LimitMax = 10000;
	public float startSize = 0.01f;
	public Vector2 startLifetime = new Vector2(0.5f, 1);


	Mesh MeshToCopy;
	MeshRenderer MeshRendererToCopy;

	ParticleSystem.MainModule ModuleMain;
	ParticleSystem.EmissionModule ModuleEmission;
	ParticleSystem.ShapeModule ModuleShape;
	ParticleSystem.ExternalForcesModule ModuleExternalForce;

	ParticleSystemRenderer ParticleSystemRenderer;


    // Start is called before the first frame update
	[ContextMenu("Setup")]
	void Setup() {

		MeshToCopy = ObjetMesh.GetComponent<MeshFilter>().sharedMesh;
		MeshRendererToCopy = ObjetMesh.GetComponent<MeshRenderer>();
		ParticleSystemRenderer = GetComponent<ParticleSystemRenderer>();

		Particules = GetComponent<ParticleSystem>();
		ParticleSystem.MinMaxCurve minMax = new ParticleSystem.MinMaxCurve(startLifetime.x, startLifetime.y);

		//
		//MAIN MODULE
		ModuleMain = Particules.main;
		ModuleMain.startLifetime = minMax;
		ModuleMain.maxParticles = LimitMax;
		ModuleMain.simulationSpace = ParticleSystemSimulationSpace.World;
		//ModuleMain.customSimulationSpace = transform;
		ModuleMain.startSize = startSize;

		//
		// EMISSION MODULE
		ModuleEmission = Particules.emission;
		ModuleShape = Particules.shape;

		//
		// SHAPE MODULE
		ModuleShape.meshShapeType = ParticleSystemMeshShapeType.Vertex;
		ModuleShape.shapeType = ParticleSystemShapeType.MeshRenderer;
		ModuleShape.meshRenderer = MeshRendererToCopy;

		//
		// RENDERER MODULE
		ParticleSystemRenderer.material = ParticuleMaterial;

		//
		// EXTERNAL FORCE MODULE
		ModuleExternalForce = Particules.externalForces;
		ModuleExternalForce.enabled = true;
		ModuleExternalForce.influenceFilter = ParticleSystemGameObjectFilter.LayerMask;


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
