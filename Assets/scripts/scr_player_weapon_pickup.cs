using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_player_weapon_pickup : MonoBehaviour
{
    [SerializeField]
    private Equipment.EquipEnum _EquipmentType;
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<scr_player>().AddEquipment(_EquipmentType))
            GameObject.Destroy(this.gameObject);
    }

}
