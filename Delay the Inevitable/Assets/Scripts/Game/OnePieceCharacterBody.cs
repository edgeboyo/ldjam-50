using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Game
{
    public class OnePieceCharacterBody : CharacterBody
    {
        [SerializeField] private List<SpriteRenderer> flippableSpriteRenderers;

        protected override void SetFlip(Direction direction)
        {
            base.SetFlip(direction);
            
            if (direction == Direction.Left)
            {
                flippableSpriteRenderers.ForEach(sr => sr.flipX = true);
            }
            else
            {
                flippableSpriteRenderers.ForEach(sr => sr.flipX = false);
            }
        }
    }
}