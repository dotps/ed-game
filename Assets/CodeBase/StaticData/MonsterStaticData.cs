using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeId monsterTypeId;
        
        public int hp = 5;
        
        public float moveSpeed = 5f;
        
        public float damage = 10f;
        public float attackCooldown = 3f;
        [Range(0.5f, 1f)] public float attackRadius;
        [Range(0.5f, 1f)] public float attackDistance;

        public int minLoot = 0;
        public int maxLoot = 10;
        
        public GameObject prefab;
    }
}