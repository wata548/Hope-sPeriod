//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SpreadInfo {
    
    
    [System.SerializableAttribute()]
    public class SkillDBData : DefaultDataType {
        
        [UnityEngine.SerializeField()]
        private int m_Code;
        
        [UnityEngine.SerializeField()]
        private string m_Name;
        
        [UnityEngine.SerializeField()]
        private string m_Description;
        
        [UnityEngine.SerializeField()]
        private int m_UseHP;
        
        [UnityEngine.SerializeField()]
        private int m_UseMP;
        
        [UnityEngine.SerializeField()]
        private int m_ATK;
        
        [UnityEngine.SerializeField()]
        private int m_ATKCon;
        
        [UnityEngine.SerializeField()]
        private int m_DEFPower;
        
        [UnityEngine.SerializeField()]
        private SpreadInfo.DefenceType m_DEFType;
        
        [UnityEngine.SerializeField()]
        private bool m_DEFReflect;
        
        [UnityEngine.SerializeField()]
        private float m_ATR;
        
        [UnityEngine.SerializeField()]
        private int m_ATRCon;
        
        [UnityEngine.SerializeField()]
        private int m_HP;
        
        [UnityEngine.SerializeField()]
        private int m_HPCon;
        
        [UnityEngine.SerializeField()]
        private bool m_HPAll;
        
        [UnityEngine.SerializeField()]
        private int m_MP;
        
        [UnityEngine.SerializeField()]
        private int m_MPCon;
        
        [UnityEngine.SerializeField()]
        private bool m_MPAll;
        
        [UnityEngine.SerializeField()]
        private SpreadInfo.EffectType m_Effect;
        
        [UnityEngine.SerializeField()]
        private float m_EffectPower;
        
        [UnityEngine.SerializeField()]
        private int m_EffectCon;
        
        [UnityEngine.SerializeField()]
        private SpreadInfo.EffectTargetType m_EffectTarget;
        
        public int Code {
            get {
                return this.m_Code;
            }
        }
        
        public string Name {
            get {
                return this.m_Name;
            }
        }
        
        public string Description {
            get {
                return this.m_Description;
            }
        }
        
        public int UseHP {
            get {
                return this.m_UseHP;
            }
        }
        
        public int UseMP {
            get {
                return this.m_UseMP;
            }
        }
        
        public int ATK {
            get {
                return this.m_ATK;
            }
        }
        
        public int ATKCon {
            get {
                return this.m_ATKCon;
            }
        }
        
        public int DEFPower {
            get {
                return this.m_DEFPower;
            }
        }
        
        public SpreadInfo.DefenceType DEFType {
            get {
                return this.m_DEFType;
            }
        }
        
        public bool DEFReflect {
            get {
                return this.m_DEFReflect;
            }
        }
        
        public float ATR {
            get {
                return this.m_ATR;
            }
        }
        
        public int ATRCon {
            get {
                return this.m_ATRCon;
            }
        }
        
        public int HP {
            get {
                return this.m_HP;
            }
        }
        
        public int HPCon {
            get {
                return this.m_HPCon;
            }
        }
        
        public bool HPAll {
            get {
                return this.m_HPAll;
            }
        }
        
        public int MP {
            get {
                return this.m_MP;
            }
        }
        
        public int MPCon {
            get {
                return this.m_MPCon;
            }
        }
        
        public bool MPAll {
            get {
                return this.m_MPAll;
            }
        }
        
        public SpreadInfo.EffectType Effect {
            get {
                return this.m_Effect;
            }
        }
        
        public float EffectPower {
            get {
                return this.m_EffectPower;
            }
        }
        
        public int EffectCon {
            get {
                return this.m_EffectCon;
            }
        }
        
        public SpreadInfo.EffectTargetType EffectTarget {
            get {
                return this.m_EffectTarget;
            }
        }
    }
    
    public class SkillDBDataTable : DefaultDataTable<SpreadInfo.SkillDBData> {
    }
}
