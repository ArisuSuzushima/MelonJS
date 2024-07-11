using System;
using System.IO;
using HarmonyLib;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using MelonLoader;

namespace MelonJS.Engine
{
    public class ScriptEngineManager
    {
        private V8ScriptEngine _engine;
        private MelonOverlay _melonOverlay;
        private HarmonyOverlay _harmonyOverlay;
        private Api _api;

        public ScriptEngineManager()
        {
            _engine = new V8ScriptEngine();
            _api = new Api();
            _melonOverlay = new MelonOverlay();
            _harmonyOverlay = new HarmonyOverlay();

            // Export Melon API to Engine
            _melonOverlay.ExportToEngine(_engine);

            // Export Harmony API to Engine
            _harmonyOverlay.ExportToEngine(_engine);

            // Export custom API to Engine
            _engine.AddHostObject("api", _api);
        }

        public void Execute(string script)
        {
            try
            {
                _engine.Execute(script);
            }
            catch (ScriptEngineException ex)
            {
                Console.WriteLine("Script error: " + ex.Message);
            }
        }

        public void ExecuteFile(string filePath)
        {
            try
            {
                string script = File.ReadAllText(filePath);
                _engine.Execute(script);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing script file {filePath}: {ex.Message}");
            }
        }

        public void ExecuteDirectory(string directoryPath)
        {
            try
            {
                var jsFiles = Directory.GetFiles(directoryPath, "*.js");
                foreach (var file in jsFiles)
                {
                    Console.WriteLine($"Initializing script file {file}");
                    ExecuteFile(file);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing script directory {directoryPath}: {ex.Message}");
            }
        }
    }

    public class MelonOverlay
    {
        public void ExportToEngine(V8ScriptEngine engine)
        {
            engine.AddHostType("MelonLogger", typeof(MelonLogger));
            engine.AddHostType("MelonPreferences", typeof(MelonPreferences));
            engine.AddHostType("MelonCoroutines", typeof(MelonCoroutines));
            engine.AddHostType("MelonDebug", typeof(MelonDebug));
            engine.AddHostType("MelonMod", typeof(MelonMod));
            engine.AddHostType("MelonUtils", typeof(MelonUtils));
        }
    }

    public class HarmonyOverlay
    {
        public void ExportToEngine(V8ScriptEngine engine)
        {
            engine.AddHostType("Harmony", typeof(HarmonyLib.Harmony));
            engine.AddHostType("HarmonyPatch", typeof(HarmonyLib.HarmonyPatch));
            engine.AddHostType("HarmonyPriority", typeof(HarmonyLib.HarmonyPriority));
            engine.AddHostType("HarmonyArgument", typeof(HarmonyLib.HarmonyArgument));
            engine.AddHostType("HarmonyMethod", typeof(HarmonyLib.HarmonyMethod));
        }
    }

    public class Api
    {
        public void ShowMessage(string message)
        {
            MelonLogger.Msg(message);
        }

        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}
