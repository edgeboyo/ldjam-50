using System;
using Data;
using Extensions;
using Misc;
using UnityEngine;
using Zenject;
using Profiles;

namespace Game
{
    public class PropHealth : CharacterHealth
    {
        public EnemyProfile PropProfile;

        private void Start()
        {
            LifeData = PropProfile.LifeData;
            bloodBox = LifeData.BloodBox;

            //Debug.Log(bloodBox);
        }
    }
}