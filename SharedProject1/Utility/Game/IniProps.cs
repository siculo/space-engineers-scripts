using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System;
using VRage.Collections;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Game;
using VRage;
using VRageMath;

namespace IngameScript
{
  partial class Program
  {
    public class IniProps
    {
      private MyIni _ini;

      private MyIniParseResult _parseResult;

      private bool _inited;

      public bool Inited { get { return _inited; } }
      public IniProps(string props)
      {
        _ini = new MyIni();
        _inited = _ini.TryParse(props, out _parseResult);
      }

      public string GetString(string section, string key, string defaultValue = null)
      {
        if (!_inited || !_ini.ContainsKey(section, key))
        {
          return defaultValue;
        }
        return _ini.Get(section, key).ToString();
      }

      public int GetInt(string section, string key, int defaultValue = 0)
      {
        if (!_inited || !_ini.ContainsKey(section, key))
        {
          return defaultValue;
        }
        return _ini.Get(section, key).ToInt16();
      }
    }
  }
}
