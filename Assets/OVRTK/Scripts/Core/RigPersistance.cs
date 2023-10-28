using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// The Persistence Rig is designed to enable smooth transitions between scenes without the need to reinitialize the OVRTK from scratch.
///</summary>
public class RigPersistance : MonoBehaviour
{
    [SerializeField]
    private GameObject OVRTK_Rig;

    public bool EnablePersistance = false;
        
    private void Start()
    {
        if (EnablePersistance)
        {
            DontDestroyOnLoad(OVRTK_Rig);
        }
    }
}