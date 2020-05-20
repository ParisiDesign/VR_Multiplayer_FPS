using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class ColorSyncModel {
    [RealtimeProperty(1, true, true)]
    private Color _color;
}
/* ----- Begin Normal Autogenerated Code ----- */
public partial class ColorSyncModel : IModel {
    // Properties
    public UnityEngine.Color color {
        get { return _cache.LookForValueInCache(_color, entry => entry.colorSet, entry => entry.color); }
        set { if (value == color) return; _cache.UpdateLocalCache(entry => { entry.colorSet = true; entry.color = value; return entry; }); FireColorDidChange(value); }
    }
    
    // Events
    public delegate void ColorDidChange(ColorSyncModel model, UnityEngine.Color value);
    public event         ColorDidChange colorDidChange;
    
    // Delta updates
    private struct LocalCacheEntry {
        public bool              colorSet;
        public UnityEngine.Color color;
    }
    
    private LocalChangeCache<LocalCacheEntry> _cache;
    
    public ColorSyncModel() {
        _cache = new LocalChangeCache<LocalCacheEntry>();
    }
    
    // Events
    public void FireColorDidChange(UnityEngine.Color value) {
        try {
            if (colorDidChange != null)
                colorDidChange(this, value);
        } catch (System.Exception exception) {
            Debug.LogException(exception);
        }
    }
    
    // Serialization
    enum PropertyID {
        Color = 1,
    }
    
    public int WriteLength(StreamContext context) {
        int length = 0;
        
        if (context.fullModel) {
            // Mark unreliable properties as clean and flatten the in-flight cache.
            // TODO: Move this out of WriteLength() once we have a prepareToWrite method.
            _color = color;
            _cache.Clear();
            
            // Write all properties
            length += WriteStream.WriteBytesLength((uint)PropertyID.Color, WriteStream.ColorToBytesLength());
        } else {
            // Reliable properties
            if (context.reliableChannel) {
                LocalCacheEntry entry = _cache.localCache;
                if (entry.colorSet)
                    length += WriteStream.WriteBytesLength((uint)PropertyID.Color, WriteStream.ColorToBytesLength());
            }
        }
        
        return length;
    }
    
    public void Write(WriteStream stream, StreamContext context) {
        if (context.fullModel) {
            // Write all properties
            stream.WriteBytes((uint)PropertyID.Color, WriteStream.ColorToBytes(_color));
        } else {
            // Reliable properties
            if (context.reliableChannel) {
                LocalCacheEntry entry = _cache.localCache;
                if (entry.colorSet)
                    _cache.PushLocalCacheToInflight(context.updateID);
                
                if (entry.colorSet)
                    stream.WriteBytes((uint)PropertyID.Color, WriteStream.ColorToBytes(entry.color));
            }
        }
    }
    
    public void Read(ReadStream stream, StreamContext context) {
        bool colorExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.colorSet);
        
        // Remove from in-flight
        if (context.deltaUpdatesOnly && context.reliableChannel)
            _cache.RemoveUpdateFromInflight(context.updateID);
        
        // Loop through each property and deserialize
        uint propertyID;
        while (stream.ReadNextPropertyID(out propertyID)) {
            switch (propertyID) {
                case (uint)PropertyID.Color: {
                    UnityEngine.Color previousValue = _color;
                    
                    _color = ReadStream.ColorFromBytes(stream.ReadBytes());
                    
                    if (!colorExistsInChangeCache && _color != previousValue)
                        FireColorDidChange(_color);
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
