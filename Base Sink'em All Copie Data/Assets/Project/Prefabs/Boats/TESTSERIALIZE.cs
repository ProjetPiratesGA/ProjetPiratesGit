using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using ProjetPirate.Data;
public class TESTSERIALIZE : NetworkBehaviour
{
    [SyncVar]
    public Data_TestSerialize data;

    // Use this for initialization
    void Start()
    {
        data = new Data_TestSerialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
            data.life += 10;
    }


    //public override bool OnSerialize(NetworkWriter writer, bool forceAll)
    //{
    //    if (forceAll)
    //    {
    //        // the first time an object is sent to a client, send all the data (and no dirty bits)
    //        writer.WritePackedUInt32((uint)this.data.life);
    //        return true;
    //    }
    //    bool wroteSyncVar = false;
    //    if ((base.syncVarDirtyBits & 1u) != 0u)
    //    {
    //        if (!wroteSyncVar)
    //        {
    //            // write dirty bits if this is the first SyncVar written
    //            writer.WritePackedUInt32((uint)this.data.life);
    //            wroteSyncVar = true;
    //        }
    //        writer.WritePackedUInt32((uint)this.data.life);
    //    }
    //    return wroteSyncVar;
    //}


    //public override void OnDeserialize(NetworkReader reader, bool initialState)
    //{
    //    if (initialState)
    //    {
    //        this.data.life = (int)reader.ReadPackedUInt32();
    //        return;
    //    }
    //    int num = (int)reader.ReadPackedUInt32();
    //    if ((num & 1) != 0)
    //    {
    //        this.data.life = (int)reader.ReadPackedUInt32();
    //    }
    //}
}
