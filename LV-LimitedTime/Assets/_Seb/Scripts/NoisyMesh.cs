using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoisyMesh : MonoBehaviour {

	[Range(0, 0.1f)]
	public float Speed = 0.005f;

	public Vector2 NoisePosition;
	public float NoiseScale = 0.15f;

	public bool StartExpand = false;

	public bool ExpandAtStart = false;
	public GameObject StartPosition;
	public float SpeedToExpand = 0.5f;

	bool isExpanding = false;
	bool isImpanding = false;

	float OriginalSpeed;
	float OriginalNoiseScale;

	float StartSpeed = 0.5f;
	float StartNoiseScale = 1.5f;

	Transform meshToMove;
	MeshFilter mf;
	SkinnedMeshRenderer smr;
	Mesh mesh;
	Vector3[] OriginalVertices;
	Vector3[] NewVertices;

	float newX, newY, newZ;

	// Use this for initialization
	void Awake() {

		OriginalSpeed = Speed;
		OriginalNoiseScale = NoiseScale;

		meshToMove = transform;
		mf = meshToMove.GetComponent<MeshFilter>();
		if (mf != null) {
			OriginalVertices = mf.mesh.vertices;
		}
		else{
			smr = meshToMove.GetComponent<SkinnedMeshRenderer>();
			if(smr != null) {
				OriginalVertices = smr.sharedMesh.vertices;
			}
		}


		NewVertices = new Vector3[OriginalVertices.Length];

		if (ExpandAtStart) {
			isExpanding = true;

			for (int i = 0 ; i < NewVertices.Length ; i++) {
				NewVertices[i] = StartPosition.transform.localPosition - gameObject.transform.localPosition;
			}
			mf.mesh.vertices = NewVertices;
		}
	}

	// Update is called once per frame
	void FixedUpdate() {

		if (StartExpand) {
			if (isExpanding) {

				NoisePosition.x += Speed;
				NoisePosition.y += Speed;

				Speed = Mathf.Lerp(StartSpeed, OriginalSpeed, SpeedToExpand);
				NoiseScale = Mathf.Lerp(StartNoiseScale, OriginalNoiseScale, SpeedToExpand);

				StartSpeed = Speed;
				StartNoiseScale = NoiseScale;

				for (int i = 0 ; i < NewVertices.Length ; i++) {

					newX = Mathf.Lerp(NewVertices[i].x, OriginalVertices[i].x + Mathf.PerlinNoise((OriginalVertices[i].x + NoisePosition.x), (OriginalVertices[i].y + NoisePosition.y)) * NoiseScale - (0.5f * NoiseScale), SpeedToExpand);
					newY = Mathf.Lerp(NewVertices[i].y, OriginalVertices[i].y + Mathf.PerlinNoise((OriginalVertices[i].y + NoisePosition.x), (OriginalVertices[i].z + NoisePosition.y)) * NoiseScale - (0.5f * NoiseScale), SpeedToExpand);
					newZ = Mathf.Lerp(NewVertices[i].z, OriginalVertices[i].z + Mathf.PerlinNoise((OriginalVertices[i].z + NoisePosition.x), (OriginalVertices[i].x + NoisePosition.y)) * NoiseScale - (0.5f * NoiseScale), SpeedToExpand);

					NewVertices[i].x = newX;
					NewVertices[i].y = newY;
					NewVertices[i].z = newZ;

				}

			}


			if (mf != null) {
				mf.mesh.vertices = NewVertices;
			}else if(smr != null) {
				
				smr.sharedMesh.vertices = NewVertices;
			}
			
		}

	}
}
