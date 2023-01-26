﻿namespace BlueShadowMon
{
    /// <summary>
    /// A consumable that can be used by a player.
    /// </summary>
    public class Consumable : Effect
    {
        protected Action<Pet> _effect;
        protected Action<Pet[]> _team;
        public Consumable(string name, EffectType type, EffectTarget target, Action<Pet> effect ) : base(name, type, target)
        {
            if (HasTargetable(EffectTarget.Self))
            {
                throw new ArgumentException("A consumable can't have 'Self' as a target!");
            }
            _effect = effect;
        }

        public Consumable(string name, EffectType type, EffectTarget target, Action<Pet[]> team) : base(name, type, target)
        {
            if (HasTargetable(EffectTarget.Self))
            {
                throw new ArgumentException("A consumable can't have 'Self' as a target!");
            }
            _team = team;
        }

        /// <summary>
        /// Use the consumable on a single target.
        /// </summary>
        /// <param name="target"></param>
        public void UseOn(Pet target)
        {
            _effect(target);
        }

        /// <summary>
        /// Use the consumable on multiple targets
        /// </summary>
        /// <param name="targets"></param>
        public void UseOn(Pet[] targets)
        {
            if (!HasTargetable(EffectTarget.Multiple))
                throw new Exception("This consumable cannot be used on multiple targets.");
            foreach (Pet t in targets)
            {
                _effect(t);
            }
        }
    }
}