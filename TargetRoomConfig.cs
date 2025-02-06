using Exiled.API.Enums;
using System.ComponentModel;
using UnityEngine;

namespace NRRSS
{
    public class TargetRoomConfig
    {
        [Description("The roomtype of the intended room.")]
        public RoomType TargetRoom { get; set; } = RoomType.Unknown;

        [Description("The name of the schematic.")]
        public string SchematicName { get; set; } = "DefaultSchematic";

        [Description("The relative position of the schematic to the room.")]
        public Vector3 Position { get; set; } = Vector3.zero;

        [Description("The relative rotation of the schematic to the room.")]
        public Vector3 Rotation { get; set; } = Vector3.zero;
    }
}
