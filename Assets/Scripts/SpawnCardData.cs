using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/SpawnCard")]
public class SpawnCardData : CardData
{
    [System.Serializable]
    public class SpawnData
    {
        public GameObject m_prefab;
        public Vector3 m_offset;
    }
    
    public List<SpawnData> m_spawnList;

    public override void Resolve(Vector3 cursorPosition) 
    {
        foreach(SpawnData spwData in m_spawnList)
        {
            Instantiate(spwData.m_prefab, cursorPosition + spwData.m_offset, Quaternion.identity);
        }
    }
}
