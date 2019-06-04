/*
 * Created by SharpDevelop.
 * User: loran
 * Date: 19/05/27
 * Time: 12:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Dust;

namespace Dust.Kernel
{
	public class JsonHeader
	{
		public string unitCommitId { get; set; }
		public string ThisUnit { get; set; }
		public string CommitId { get; set; }
		public string EntityUnit { get; set; }
		public string EntityId { get; set; }
		public string LocalId { get; set; }
		public string PrimaryType { get; set; }
		public string NativeId { get; set; }
	}

	public class JsonUnitWrapper
	{
		public JsonHeader header { get; set; }
		public JObject data { get; set; }
		public JObject refUnits { get; set; }
		public Dictionary<String, String> keyTypes { get; set; }
		
		static bool isLocal(String key)
		{
			return !key.StartsWith("-");
		}
		
		DustValType getValType(String key)
		{
			return (DustValType)Enum.Parse(typeof(DustValType), keyTypes[key]);
		}
		
		DustDataEntity resolveRef(DustSession system, DustEntityStore store, String key)
		{
			if (isLocal(key)) {
				return store[key];
			} else {
				var keyModName = (String)((JObject)data[key])[header.EntityUnit];
				var keyId = (String)((JObject)data[key])[header.EntityId];
								
				return system.modules[keyModName][keyId];
			}
		}
		
		public void populate(Dictionary<string, JsonUnitWrapper> js, DustSession system, String name)
		{
			DustEntityStore store = system.modules[name];
			
			foreach (var c in data.Children()) {		
				var p = (JProperty)c;
				
				var id = p.Name;
				
				if (isLocal(id)) {
					var eTarget = store[id];
					
					var o = (JObject)p.Value;
			
					foreach (var cc in o.Children()) {
						var pp = (JProperty)cc;
				
						var key = pp.Name;
						DustDataEntity eKey = resolveRef(system, store, key);
						eKey.optValType = getValType(key);
						Object val = null;
						
						switch (eKey.optValType) {
							case DustValType.AttDefBool:
							case DustValType.AttDefIdentifier:
							case DustValType.AttDefDouble:
							case DustValType.AttDefLong:
							case DustValType.AttDefRaw:
								val = ((JValue)pp.Value).Value;
								break;
							case DustValType.LinkDefSingle:
								val = ((JValue)pp.Value).Value;
								val = resolveRef(system, store, (String)val);
								break;
							case DustValType.LinkDefSet:
							case DustValType.LinkDefArray:
								var a = (JArray)pp.Value;
								int idx = 0;
								
								foreach (var r in a.Children()) {
									eTarget.setValue(eKey, resolveRef(system, store, (String)r), idx++);
								}
								
								break;
							case DustValType.LinkDefMap:
								break;
						}
							
						if (null != val) {
							eTarget.setValue(eKey, val, null);
						}
					}
				}
			}
		}
	}
	
	public static class DustCommSerializerJson
	{
		public static DustDataEntity loadSingleFromText(string jsonText, string mod, string id)
		{
			DustSession session = DustSystem.getSession();
			
			Dictionary<string, JsonUnitWrapper> js;
			js = JsonConvert.DeserializeObject<Dictionary<string, JsonUnitWrapper>>(jsonText);
			
			foreach (string k in js.Keys) {
				JsonUnitWrapper jw = js[k];
				
				Console.WriteLine("Unit read " + k);
				
				jw.populate(js, session, k);
			}

			DustDataEntity eRoot = session.modules[mod][id];
			
			session.ctx[DustContext.SELF] = eRoot;
			
			return eRoot;
		}

	}
}
