using System;

namespace CodeBase.Data
{
    [Serializable]
    public class HeroState
    {
        public float currenHp;
        public float maxHp;

        public void ResetHP() => 
            currenHp = maxHp;
    }
}