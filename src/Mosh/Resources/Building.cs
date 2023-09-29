using Moryx.AbstractionLayer.Resources;
using Moryx.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mosh.Resources
{
    [Description("Resource used to model the structure of a building")]
    public class BuildingStructure : Resource
    {
        [DataMember, EntrySerialize]
        public BuildingElementType ElementType  { get; set; }
    }

    public enum BuildingElementType
    {
        Room,
        Floor,
        Building,
        Plot
    }
}
