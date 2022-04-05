using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

namespace Game
{
    public class TwoPieceCharacterBody : CharacterBody
    {
        [SerializeField] public GameObject upperBody;
        [SerializeField] public GameObject lowerBody;

        public float bodyLookingRight;
        public float bodyLookingLeft;

        private Dictionary<bool, float> lowerBodyOffsets;

        public SpriteRenderer upperSR;
        public SpriteRenderer lowerSR;

        private SpriteRenderer[] renderers;

        // [HideInInspector] public Collider2D collider; //helpful for calculation. Leave hidden in case someone has a v bad idea

        private void Start()
        {
            upperSR = upperBody.GetComponent<SpriteRenderer>();
            lowerSR = lowerBody.GetComponent<SpriteRenderer>();

            renderers = new SpriteRenderer[2] { upperSR, lowerSR };

            lowerBodyOffsets = new Dictionary<bool, float>() { { false, bodyLookingRight }, { true, bodyLookingLeft } };
        }

        protected override void SetFlip(Direction direction)
        {
            base.SetFlip(direction);
            
            if (direction == Direction.Left)
            {
                SetFlip(true);
            }
            else
            {
                SetFlip(false);
            }
        }

        private void SetFlip(bool flipped)
        {
            foreach(var sr in renderers)
            {
                sr.flipX = flipped;
            }

            Vector3 lowerOffset = lowerBody.transform.localPosition;

            lowerOffset.x = lowerBodyOffsets[flipped];

            lowerBody.transform.localPosition = lowerOffset;
        }
    }
}
