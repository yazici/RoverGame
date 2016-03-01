﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class QuestManager : MonoBehaviour
    {
        
        public List<Quest> quests = new List<Quest>();

        protected int currentQuest = 0;

        public Quest CurrentQuest
        {
            get { return quests[currentQuest]; }
        }


        public virtual void CompleteQuest()
        {

        }


        public virtual void BeginQuest()
        {

        }
    }
}
