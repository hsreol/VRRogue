using System.Collections.Generic;
using UnityEngine;

namespace Invector
{
    [vClassHeader("OBJECT DAMAGE", iconName = "DamageIcon")]
    public class vvObjectDamage : vMonoBehaviour
    {
        [System.Serializable]
        public class OnHitEvent : UnityEngine.Events.UnityEvent<Collider> { }
        public vDamage damage;
        [Tooltip("Assign this to set other damage sender")]
        public Transform overrideDamageSender;
        [Tooltip("List of tags that can be hit")]
        public List<string> tags;
        [Tooltip("Check to use the damage Frequence")]
        public bool continuousDamage;
        [Tooltip("Apply damage to each end of the frequency in seconds ")]
        public float damageFrequency = 0.5f;
        private List<Collider> targets;
        private List<Collider> disabledTarget;
        private float currentTime;
        public OnHitEvent onHit;
        //������ ��񿡵��� ���ݷ� ����ġ
        public int Leather_Resist;
        public int Light_Resist;
        public int Heavy_Resist;

        public Weapon weapon;

        int damageCount = 1;

        public enum CollisionMethod
        {
            OnTriggerEnter,
            OnColliderEnter,
            OnParticleCollision
        }

        public CollisionMethod collisionMethod = CollisionMethod.OnTriggerEnter;

        public ParticleSystem part;
        public bool limitParticleCollisionEvent = false;
        public int maxParticleCollisionEvent = 1;
        public List<ParticleCollisionEvent> collisionEvents;

        public GameObject playerStat;
        public PlayerStatManager playerStatManager;
        
        float tagBalance = 0; 
        int playerStatDamage = 0;
        float prof = 0;

        protected virtual void Start()
        {
            weapon = gameObject.GetComponent<Weapon>();
            Leather_Resist = 1;
            Light_Resist = 1;
            Heavy_Resist = 1;
            targets = new List<Collider>();
            disabledTarget = new List<Collider>();
            if (collisionMethod == CollisionMethod.OnParticleCollision)
            {
                part = GetComponent<ParticleSystem>();
                collisionEvents = new List<ParticleCollisionEvent>();
            }
            playerStat = GameObject.Find("PlayerStat");
            playerStatManager = playerStat.GetComponent<PlayerStatManager>();
        }

        protected virtual void Update()
        {
            if (!this.enabled) return;
            if (continuousDamage && targets != null && targets.Count > 0)
            {
                if (currentTime > 0)
                {
                    currentTime -= Time.deltaTime;
                }
                else
                {
                    currentTime = damageFrequency;
                    foreach (Collider collider in targets)
                        if (collider != null)
                        {
                            if (collider.enabled)
                            {

                                ApplyDamage(collider, transform.position); // apply damage to enabled collider

                            }
                            else
                                disabledTarget.Add(collider);// add disabled collider to list of disabled
                        }
                    //remove all disabled colliders of target list
                    if (disabledTarget.Count > 0)
                    {
                        for (int i = disabledTarget.Count; i >= 0; i--)
                        {
                            if (disabledTarget.Count == 0) break;
                            try
                            {
                                if (targets.Contains(disabledTarget[i]))
                                    targets.Remove(disabledTarget[i]);
                            }
                            catch
                            {
                                break;
                            }
                        }
                    }

                    if (disabledTarget.Count > 0) disabledTarget.Clear();
                }
            }
        }

        protected virtual void OnCollisionEnter(Collision hit)
        {
            if (!this.enabled) return;
            if (collisionMethod != CollisionMethod.OnColliderEnter || continuousDamage) return;

            if ((tags.Count == 0 || tags.Contains(hit.transform.tag)))
            {
                ApplyDamage(hit.collider, hit.contacts[0].point);

            }
        }

        protected virtual void OnTriggerEnter(Collider hit)
        {
            if (!this.enabled) return;
            if (collisionMethod != CollisionMethod.OnTriggerEnter) return;
            if (continuousDamage && (tags.Count == 0 || tags.Contains(hit.transform.tag)) && !targets.Contains(hit))
            {
                targets.Add(hit);
            }
            else if (tags.Count == 0 || tags.Contains(hit.gameObject.tag))
            {
                ApplyDamage(hit, transform.position);
            }
        }

        protected virtual void OnTriggerExit(Collider hit)
        {
            if (!this.enabled) return;
            if (collisionMethod == CollisionMethod.OnColliderEnter && !continuousDamage) return;

            if ((tags.Count == 0 || tags.Contains(hit.transform.tag)) && targets.Contains(hit))
            {
                targets.Remove(hit);
            }
        }

        protected virtual void OnParticleCollision(GameObject hit)
        {
            if (!this.enabled) return;
            if (collisionMethod != CollisionMethod.OnParticleCollision) return;

            int numCollisionEvents = part.GetCollisionEvents(hit, collisionEvents);

            Collider collider = hit.GetComponent<Collider>();
            int i = 0;

            while ((!limitParticleCollisionEvent && i < numCollisionEvents) || (!limitParticleCollisionEvent && i < maxParticleCollisionEvent))
            {
                if (collider)
                {
                    if (continuousDamage && (tags.Count == 0 || tags.Contains(hit.transform.tag)) && !targets.Contains(collider))
                    {
                        targets.Add(collider);
                    }
                    else if ((tags.Count == 0 || tags.Contains(hit.transform.tag)))
                    {
                        ApplyDamage(collider, transform.position);
                    }
                }
                i++;
            }
        }

        public virtual void ClearTargets()
        {
            targets.Clear();
        }

        protected virtual void ApplyDamage(Collider target, Vector3 hitPoint)
        {
            damage.hitReaction = true;
            damage.sender = overrideDamageSender ? overrideDamageSender : transform;
            damage.hitPosition = hitPoint;
            damage.receiver = target.transform;
            calcSuperior(target);

            //damage.damageValue = int.Parse(weapon.weaponDamage);
            //damage.damageValue = (int)(float.Parse(weapon.weaponDamage) * tagBalance);
            //damage.damageValue = (int)(float.Parse(weapon.weaponDamage) * tagBalance * playerStatDamage);
            damage.damageValue = (int)(float.Parse(weapon.weaponDamage) * tagBalance * playerStatDamage * prof);
            playerStatManager.prof.ProfUp(weapon.weaponProfType);
            
            target.gameObject.ApplyDamage(new vDamage(damage));
            onHit.Invoke(target);


            if(gameObject.CompareTag("Projectile")){
                Destroy(gameObject, .5f);
            }
            else{
                ;
            }
        }
        protected virtual void calcSuperior(Collider target){
            if(target.transform.Find("Armor").gameObject.CompareTag("Armor_Leather")){
                switch(weapon.weaponType)
                {
                    case "Magic":
                        tagBalance = 0.5f;
                        break;
                    case "Sting":
                        tagBalance = 1.5f;
                        break;
                    case "Blow":
                        tagBalance = 1f;
                        break;
                    default:
                        tagBalance = 1f;
                        break;
                }
            }
            else if (target.transform.Find("Armor").gameObject.CompareTag("Armor_Light")){
                switch(weapon.weaponType)
                {
                    case "Magic":
                        tagBalance = 1f;
                        break;
                    case "Sting":
                        tagBalance = 1f;
                        break;
                    case "Blow":
                        tagBalance = 1.5f;
                        break;
                    default:
                        tagBalance = 1f;
                        break;
                }
            }
            else if (target.transform.Find("Armor").gameObject.CompareTag("Armor_Heavy")){
                switch(weapon.weaponType)
                {
                    case "Magic":
                        tagBalance = 1.5f;
                        break;
                    case "Sting":
                        tagBalance = 0.5f;
                        break;
                    case "Blow":
                        tagBalance = 1f;
                        break;
                    default:
                        tagBalance = 1f;
                        break;
                }
            }
            else{
                tagBalance = 1f;
            }

            if(gameObject.CompareTag("Projectile")){
                playerStatDamage = playerStatManager.rangeDamage;
            }
            else if(gameObject.CompareTag("Melee")){
                playerStatDamage = playerStatManager.meleeDamage;
            }
            
            prof = playerStatManager.prof.GetProf(weapon.weaponProfType);
        }
    }
}