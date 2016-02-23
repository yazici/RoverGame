﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class Intro : MonoBehaviour
    {
        delegate void CompleteObjective();
        CompleteObjective onCompleteObjective;

        private PlayerStats playerStats = null;

        public MouseLook mouseLook;
        public CameraDriver cameraDriver;
        public CharacterDriver characterDriver;

        public Objective pressAnyKeyObjective;
        public Objective lookObjective;
        public Objective moveObjective;
        public Objective escapePodObjective;
        public Objective constructExplosiveObjective;
        public Objective clearLandslideObjective;

        public float displaySpeed = 0.01f;

        public List<Objective> introObjectives = new List<Objective>();
        

        public PlayerStats CachedPlayerStats
        {
            get { return (playerStats != null) ? playerStats : playerStats = GameObject.FindObjectOfType<PlayerStats>(); }
        }

        private IEnumerator RunTutorial()
        {
            FadeMenu fadeMenu = UIManager.GetMenu<FadeMenu>();
            ObjectiveTracker objectiveTracker = UIManager.GetMenu<ObjectiveTracker>();
            ObjectiveDisplay od;
            CachedPlayerStats.DisableMovement();

            fadeMenu.Fade(0f, Color.clear, Color.black);
            bool anyKeyPressed = false;
            od = objectiveTracker.AddObjective(pressAnyKeyObjective, displaySpeed);
            while (!anyKeyPressed || od.Isfilling)
            {
                anyKeyPressed = Input.anyKey;
                yield return null;
            }

            yield return new WaitForSeconds(1f);

            for(int i = 0; i < introObjectives.Count; i++)
            {
                yield return new WaitForSeconds(Random.Range(0.1f, 0.4f));
                od = objectiveTracker.AddObjective(introObjectives[i], displaySpeed);
                while(od.Isfilling)
                {
                    yield return null;
                }
            }

            fadeMenu.Fade(1f, Color.black, Color.clear);
            yield return new WaitForSeconds(1f);
            fadeMenu.Close();

            od = objectiveTracker.AddObjective(lookObjective, displaySpeed);
            while(od.Isfilling)
            {
                yield return null;
            }
            playerStats.EnableMovement();

            float prevMoveSpeedMultiplier = characterDriver.movementSpeedMultiplier;
            characterDriver.movementSpeedMultiplier = 0f;

            while(Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0)
            {
                yield return null;
            }

            yield return new WaitForSeconds(1f);

            od = objectiveTracker.AddObjective(moveObjective, displaySpeed);
            while (od.Isfilling)
            {
                yield return null;
            }
            characterDriver.movementSpeedMultiplier = prevMoveSpeedMultiplier;

            while (Input.GetAxis("RoverMove") == 0 && Input.GetAxis("RoverTurn") == 0)
            {
                yield return null;
            }

            yield return new WaitForSeconds(1f);

            objectiveTracker.AddObjective(escapePodObjective, displaySpeed);
        }


        public void NextObjective()
        {
            ObjectiveTracker objectiveTracker = UIManager.GetMenu<ObjectiveTracker>();
            ObjectiveDisplay od = objectiveTracker.AddObjective(constructExplosiveObjective, displaySpeed);
        }


        private void Awake()
        {
            onCompleteObjective += NextObjective;
            StartCoroutine(RunTutorial());
        }
    }
}
