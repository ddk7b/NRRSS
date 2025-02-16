using Exiled.API.Features;
using Handlers = Exiled.Events.Handlers;
using MapEditorReborn.API.Features;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Exiled.API.Enums;
using MapEditorReborn.API.Features.Objects;

namespace NRRSS
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance { get; private set; }

        public virtual string Name { get; } = "NRRSS";

        public virtual string Author { get; } = "Temi";

        public virtual string Prefix { get; } = "NRRSS";

        public virtual Version Version { get; } = new Version(1, 0, 2);

        public override void OnEnabled()
        {
            Log.Info("NRRSS plugin is being enabled.");
            Instance = this;
            ValidateConfig();
            Handlers.Server.RoundStarted += OnRoundStart;
            base.OnEnabled();

        }

        public override void OnDisabled()
        {
            Log.Info("NRRSS plugin is being disabled.");
            Handlers.Server.RoundStarted -= OnRoundStart;
            base.OnDisabled();
        }

        private void ValidateConfig()
        {
            if (Instance.Config.RoomSpawns == null || Instance.Config.RoomSpawns.Count == 0)
            {
                Log.Warn("No RoomSpawns are configured in the plugin's configuration. The plugin may not function correctly.");
            }
        }

        private void OnRoundStart()
        {
            foreach (Room room in Room.List)
            {
                foreach (TargetRoomConfig configRoom in Config.RoomSpawns.Values)
                {
                    if (room.Type == configRoom.TargetRoom)
                    {
                        int spawnChance = configRoom.Chance;
                        int randomRoll = UnityEngine.Random.Range(0, 100);
                        if (randomRoll < spawnChance)
                        {
                            Quaternion roomRot = room.Rotation;
                            Vector3 rotatedPositionOffset = roomRot * configRoom.Position;
                            Vector3 worldPosition = room.Position + rotatedPositionOffset;
                            Quaternion configRotation = Quaternion.Euler(configRoom.Rotation);
                            Quaternion worldRotation = roomRot * configRotation;
                            var schematic = ObjectSpawner.SpawnSchematic(configRoom.SchematicName, worldPosition, worldRotation, Vector3.one, null, true);
                            if (schematic == null)
                            {
                                Log.Debug($"Schematic was unable to spawn.");
                            }
                            else
                            {
                                Log.Debug($"Schematic was successfully spawned.");
                            }
                        }
                        else
                        {
                            Log.Debug($"{configRoom.SchematicName} failed chance check. Rolled {randomRoll} (needed < {spawnChance}).");
                        }
                    }
                }
            }
        }
    }
}
