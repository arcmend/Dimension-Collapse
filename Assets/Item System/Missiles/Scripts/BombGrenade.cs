﻿using UnityEngine;

namespace DimensionCollapse
{
    public class BombGrenade : AbstractMissile
    {
        public float effectiveRadius = 20f;
        public float centerDamage = 50;
        public float impactForce = 500;
        public override void ExplodeCore()
        {
            Vector3 explosionPos = transform.position;
            Collider[] victims = Physics.OverlapSphere(explosionPos, effectiveRadius, LayerMask.GetMask("Default", "Player"));
            foreach (var victim in victims)
            {
                if (ItemUtils.IsPlayer(victim.gameObject))
                {
                    PlayerManager playerManager = victim.GetComponent<PlayerManager>();
                    playerManager.OnAttacked(Mathf.Lerp(centerDamage, 0, Vector3.Distance(victim.transform.position, explosionPos) / effectiveRadius));
                    playerManager.AddImpact(victim.transform.position - explosionPos, impactForce);
                }
                else
                {
                    victim.GetComponent<Rigidbody>()?.AddExplosionForce(impactForce, explosionPos, effectiveRadius, 2);
                }
            }
        }
    }
}
