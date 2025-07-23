using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;


namespace Cloth
{

    public enum ClothType
    {
        SPEED,
        STRONG,

    }

    public class ClothManager : Singleton<ClothManager>
    {
        public List<ClothSetup> clothSetups;

        public ClothSetup GetSetupByType(ClothType clothtype)
        {
            return clothSetups.Find(i => i.clothtype == clothtype);
        }
    }

    [System.Serializable]
    public class ClothSetup
    {
        public ClothType clothtype;
        public Texture2D texture;
    }
}