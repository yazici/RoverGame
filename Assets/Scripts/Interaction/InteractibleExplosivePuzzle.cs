﻿using UnityEngine;
using System.Collections;

namespace Sol{
	public class InteractibleExplosivePuzzle : InteractiblePuzzle
    {
		public GameObject explosiveDevice;
		public Ingredient desiredIngredient;
		public Collider scaleTriggerColl;
		public bool triggered;
		public string failString = "You will need an {0} to clear this landslide";
		private bool checkScaled = true;

		public override void Interact(){
			Debug.Log("Interacting");
			Inventory inventory = UIManager.GetMenu<Inventory>();
			MessageMenu messageMenu = UIManager.GetMenu<MessageMenu>();

			if (inventory.GetIngredientAmount (desiredIngredient) > 0) {
				Debug.Log ("wakka wakka");
				triggered = true;
				inventory.RemoveInventoryItem (desiredIngredient, 1);
				explosiveDevice.SetActive(true);
				scaleTriggerColl.enabled = true;
			} else if (!triggered) {
				interactible = false;
				failString = string.Format (failString, desiredIngredient);
				messageMenu.Open (failString, 4, 2.0f);

			} else if (interactible){
				base.Interact ();
			}
			checkScaled = true;
		}

		void Update() {
			if (checkScaled) {
				if (puzzleScaled) {
					base.Interact ();
					checkScaled = false;
				}
			}
		}
		
		public override bool Complete {
			get {
				return base.Complete;
			}
			set {
				base.Complete = value;
				if (complete) {
					gameObject.GetComponent<CraterExplosion>().Detonate ();
					myPuzzleCanvas.GetComponent<Animator> ().SetTrigger ("FadeBackward");
				}
			}
		}
	}
}
