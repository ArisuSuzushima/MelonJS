using System;
using MelonJS.Engine;
using MelonLoader;

namespace MelonJS
{
    public static class BuildInfo
    {
        public const string Name = "MelonJS";
        public const string Description = "JavaScript modding framework for MelonLoader.";
        public const string Author = "AkiChan";
        public const string Company = null;
        public const string Version = "1.0.0";
        public const string DownloadLink = null;
    }

    public class MelonJS : MelonMod
    {
        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("Initializing MelonJS...");
            ScriptEngineManager scriptEngineManager = new ScriptEngineManager();
            MelonLogger.Msg("Initialized " + BuildInfo.Name + " v" + BuildInfo.Version);
            
            string modsDirectory = "MelonJS-Mods";
            MelonLogger.Msg("Attempting to execute scripts in directory: " + modsDirectory);
            try
            {
                scriptEngineManager.ExecuteDirectory(modsDirectory);
                MelonLogger.Msg("Scripts executed successfully.");
            }
            catch (Exception ex)
            {
                MelonLogger.Error("Failed to execute scripts: " + ex.Message);
            }
        }
    }
}