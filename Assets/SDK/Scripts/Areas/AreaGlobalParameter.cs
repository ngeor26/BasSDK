namespace ThunderRoad
{
    using Newtonsoft.Json;
    using System;
    using UnityEngine;

    public abstract class AreaGlobalParameter
    {
        [JsonMergeKey, JsonProperty("$type"), JsonIgnore]
        public Type type
        {
            get { return GetType(); }
            set { }
        }

        public bool visibleFromGate;

        [JsonIgnore]
        [HideInInspector]
        public SpawnableArea spawnableArea;

        public AreaGlobalParameter()
        {

        }

        public AreaGlobalParameter(bool visibleFromGate)
        {
            this.visibleFromGate = visibleFromGate;
        }

        public virtual bool IsCompatible(AreaGlobalParameter other)
        {
            return !(visibleFromGate && other.visibleFromGate);
        }

        public abstract void Apply(bool isInArea, bool inGateWay, SpawnableArea connectedArea);

        public abstract AreaGlobalParameter GetParameterFor(SpawnableArea spawnableArea);
    }
}