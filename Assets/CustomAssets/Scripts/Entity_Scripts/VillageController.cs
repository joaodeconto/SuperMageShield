using UnityEngine;
using TMPro;

namespace SuperMageShield
{
    public class VillageController : EntityController
    {
        private VillageSO _villageData;        

        private void Start()
        {
            _villageData =  _entityData as VillageSO;
        }
    }
}
