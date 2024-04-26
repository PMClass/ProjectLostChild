using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointData : MonoBehaviour
{
    #region Serialized Private Variables
    // used by GameManager to indicate which checkpoint to use for instantiation transform
    // (checkpoint with largest number is used, only one of the checkpoints need to be set)
    [SerializeField] private int CheckpointPriority;
    #endregion

    #region Data Accessors
    public int GetPriority() { return CheckpointPriority; }
    #endregion
}
