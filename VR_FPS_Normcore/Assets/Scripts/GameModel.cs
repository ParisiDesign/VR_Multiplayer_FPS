using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class GameModel
{
    [RealtimeProperty(1, true, true)]
    private int _spot1Player;
    [RealtimeProperty(2, true, true)]
    private int _spot2Player;
}
/* ----- Begin Normal Autogenerated Code ----- */
public partial class GameModel : IModel {
    // Properties
    public int spot1Player {
        get { return _cache.LookForValueInCache(_spot1Player, entry => entry.spot1PlayerSet, entry => entry.spot1Player); }
        set { if (value == spot1Player) return; _cache.UpdateLocalCache(entry => { entry.spot1PlayerSet = true; entry.spot1Player = value; return entry; }); FireSpot1PlayerDidChange(value); }
    }
    public int spot2Player {
        get { return _cache.LookForValueInCache(_spot2Player, entry => entry.spot2PlayerSet, entry => entry.spot2Player); }
        set { if (value == spot2Player) return; _cache.UpdateLocalCache(entry => { entry.spot2PlayerSet = true; entry.spot2Player = value; return entry; }); FireSpot2PlayerDidChange(value); }
    }
    
    // Events
    public delegate void Spot1PlayerDidChange(GameModel model, int value);
    public event         Spot1PlayerDidChange spot1PlayerDidChange;
    public delegate void Spot2PlayerDidChange(GameModel model, int value);
    public event         Spot2PlayerDidChange spot2PlayerDidChange;
    
    // Delta updates
    private struct LocalCacheEntry {
        public bool spot1PlayerSet;
        public int  spot1Player;
        public bool spot2PlayerSet;
        public int  spot2Player;
    }
    
    private LocalChangeCache<LocalCacheEntry> _cache;
    
    public GameModel() {
        _cache = new LocalChangeCache<LocalCacheEntry>();
    }
    
    // Events
    public void FireSpot1PlayerDidChange(int value) {
        try {
            if (spot1PlayerDidChange != null)
                spot1PlayerDidChange(this, value);
        } catch (System.Exception exception) {
            Debug.LogException(exception);
        }
    }
    public void FireSpot2PlayerDidChange(int value) {
        try {
            if (spot2PlayerDidChange != null)
                spot2PlayerDidChange(this, value);
        } catch (System.Exception exception) {
            Debug.LogException(exception);
        }
    }
    
    // Serialization
    enum PropertyID {
        Spot1Player = 1,
        Spot2Player = 2,
    }
    
    public int WriteLength(StreamContext context) {
        int length = 0;
        
        if (context.fullModel) {
            // Mark unreliable properties as clean and flatten the in-flight cache.
            // TODO: Move this out of WriteLength() once we have a prepareToWrite method.
            _spot1Player = spot1Player;
            _spot2Player = spot2Player;
            _cache.Clear();
            
            // Write all properties
            length += WriteStream.WriteVarint32Length((uint)PropertyID.Spot1Player, (uint)_spot1Player);
            length += WriteStream.WriteVarint32Length((uint)PropertyID.Spot2Player, (uint)_spot2Player);
        } else {
            // Reliable properties
            if (context.reliableChannel) {
                LocalCacheEntry entry = _cache.localCache;
                if (entry.spot1PlayerSet)
                    length += WriteStream.WriteVarint32Length((uint)PropertyID.Spot1Player, (uint)entry.spot1Player);
                if (entry.spot2PlayerSet)
                    length += WriteStream.WriteVarint32Length((uint)PropertyID.Spot2Player, (uint)entry.spot2Player);
            }
        }
        
        return length;
    }
    
    public void Write(WriteStream stream, StreamContext context) {
        if (context.fullModel) {
            // Write all properties
            stream.WriteVarint32((uint)PropertyID.Spot1Player, (uint)_spot1Player);
            stream.WriteVarint32((uint)PropertyID.Spot2Player, (uint)_spot2Player);
        } else {
            // Reliable properties
            if (context.reliableChannel) {
                LocalCacheEntry entry = _cache.localCache;
                if (entry.spot1PlayerSet || entry.spot2PlayerSet)
                    _cache.PushLocalCacheToInflight(context.updateID);
                
                if (entry.spot1PlayerSet)
                    stream.WriteVarint32((uint)PropertyID.Spot1Player, (uint)entry.spot1Player);
                if (entry.spot2PlayerSet)
                    stream.WriteVarint32((uint)PropertyID.Spot2Player, (uint)entry.spot2Player);
            }
        }
    }
    
    public void Read(ReadStream stream, StreamContext context) {
        bool spot1PlayerExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.spot1PlayerSet);
        bool spot2PlayerExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.spot2PlayerSet);
        
        // Remove from in-flight
        if (context.deltaUpdatesOnly && context.reliableChannel)
            _cache.RemoveUpdateFromInflight(context.updateID);
        
        // Loop through each property and deserialize
        uint propertyID;
        while (stream.ReadNextPropertyID(out propertyID)) {
            switch (propertyID) {
                case (uint)PropertyID.Spot1Player: {
                    int previousValue = _spot1Player;
                    
                    _spot1Player = (int)stream.ReadVarint32();
                    
                    if (!spot1PlayerExistsInChangeCache && _spot1Player != previousValue)
                        FireSpot1PlayerDidChange(_spot1Player);
                    break;
                }
                case (uint)PropertyID.Spot2Player: {
                    int previousValue = _spot2Player;
                    
                    _spot2Player = (int)stream.ReadVarint32();
                    
                    if (!spot2PlayerExistsInChangeCache && _spot2Player != previousValue)
                        FireSpot2PlayerDidChange(_spot2Player);
                    break;
                }
                default:
                    stream.SkipProperty();
                    break;
            }
        }
    }
}
/* ----- End Normal Autogenerated Code ----- */
