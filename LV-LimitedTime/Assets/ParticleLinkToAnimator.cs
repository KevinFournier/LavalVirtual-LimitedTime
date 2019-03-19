using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLinkToAnimator : MonoBehaviour
{

	public Animator anim;
	public string boolToWait = "isWalking";

	public List<ParticuleStaticObject> listParticle;

	List<int> startValues;

	private void Start() {
		startValues = new List<int>();
		foreach (ParticuleStaticObject p in listParticle) {
			startValues.Add(p.RateOverTime);
		}
	}

	// Update is called once per frame
	void Update()
    {
		if (anim.GetBool(boolToWait)) {
			int counter = 0;


			foreach (ParticuleStaticObject p in listParticle) {

				p.RateOverTime = startValues[counter];
				counter++;
			}
			
		}
		else {
			foreach (ParticuleStaticObject p in listParticle) {
				p.RateOverTime = 0;
			}
		}

	}
}
