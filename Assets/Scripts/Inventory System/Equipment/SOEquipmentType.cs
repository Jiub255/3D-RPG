using UnityEngine;

// Treat these like equipment type enums, use the SO's name as the enum. 
[CreateAssetMenu(menuName = "Equipment/Equipment Type", fileName = "New Equipment Type")]
public class SOEquipmentType : ScriptableObject
{
    // Could put references to which stats this piece of equipment will affect. 
    // Would this work?
    // Could do a whole different set of SO's for stat bonuses.
    //     Just have each stat have a separate SO with a int field for modification value. Maybe. 
/*    [SerializeField]
    private List<SOStat> _affectedStats;*/
}