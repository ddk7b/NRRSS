using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace NRRSS
{
    public class Config : IConfig
    {

        [Description("Whether or not the debug mode is enabled.")]
        public bool Debug { get; set; }

        [Description("Whether or not the plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Room list for schematics.")]
        public Dictionary<string, TargetRoomConfig> RoomSpawns { get; set; } = new Dictionary<string, TargetRoomConfig>()
        {
            {
                "ListItem1",
                new TargetRoomConfig()
            }
        };
    }
}
