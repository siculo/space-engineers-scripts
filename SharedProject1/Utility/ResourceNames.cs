using System;
using System.Collections.Generic;
using System.Text;

namespace IngameScript
{
  partial class Program
  {
    class ResourceNames
    {
      private static Dictionary<string, string> typeIdToShortName = new Dictionary<string, string>()
      {
        ["myobjectbuilder_ammomagazine"] = "Ammo",
        ["myobjectbuilder_component"] = "Comp.",
        ["myobjectbuilder_ingot"] = "Ingot",
        ["myobjectbuilder_ore"] = "Ore",
        ["myobjectbuilder_physicalcubeblockobject"] = "Block",
        ["myobjectbuilder_physicalgunobject"] = "gun"
      };

      private static Dictionary<string, string> nameToTypeId = new Dictionary<string, string>()
      {
        ["ammo"] = "MyObjectBuilder_AmmoMagazine",
        ["component"] = "MyObjectBuilder_Component",
        ["ingot"] = "MyObjectBuilder_Ingot",
        ["ore"] = "MyObjectBuilder_Ore",
        ["block"] = "MyObjectBuilder_PhysicalCubeBlockObject",
        ["gun"] = "MyObjectBuilder_PhysicalGunObject"
      };

      private static Dictionary<string, string> subtypeIdToShortName = new Dictionary<string, string>();

      public static string TypeIdToShortName(string typeId)
      {
        return TransformName(typeId, typeIdToShortName);
      }

      public static string NameToTypeId(string name)
      {
        return TransformName(name, nameToTypeId);
      }

      public static string SubtypeIdToShortName(string subtypeId)
      {
        return TransformName(subtypeId, subtypeIdToShortName);
      }

      private static string TransformName(string name, Dictionary<string, string> transformMap)
      {
        if (string.IsNullOrWhiteSpace(name))
        {
          return null;
        }
        string key = name.ToLower();
        if (transformMap.ContainsKey(key))
        {
          return transformMap[key];
        }
        return name;
      }
    }
  }
}
