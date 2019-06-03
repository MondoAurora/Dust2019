/*
 * Created by SharpDevelop.
 * User: Montru / Lorand Kedves
 * Date: 19/05/27
 * Time: 12:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Dust.Utils.CSharp
{
	//	public abstract class DustUtilConstructor<TKey, TValue>
	//	{
	//		public abstract TValue create(TKey key, params Object[] hints);
	//	}

	//	public class Default<TKey, TValue> : DustUtilConstructor<TKey, TValue>
	//	{
	//		public TValue create(TKey key, params Object[] hints) {
	//			return (TValue) Activator.CreateInstance((Type) TValue);
	//		}
	//	}

	//	private class LeftProvider : DustUtilFactory<String, String>
	//		{
	//			readonly UnitJsonWrapper jw;
	//
	//			public LeftProvider(UnitJsonWrapper jw)
	//				: base(typeof(String))
	//			{
	//				this.jw = jw;
	//			}
	//
	//			protected override String create(String key)
	//			{
	//				JObject refOb = (JObject)jw.data[key];
	//				String ret = ((JValue)refOb[LT]).ToString();
	//				return ret;
	//			}
	//		};

	
	public class DustUtilFactory<TKey, TValue> : Dictionary<TKey, TValue>
	{
		Type valType;
		
		public DustUtilFactory(Type valType)
		{
			this.valType = valType;
		}
		
		protected virtual TValue create(TKey key)
		{
			return (TValue)Activator.CreateInstance(valType);
		}
		
		public virtual new TValue this[TKey key] {
			get {
				if (!ContainsKey(key)) {
					var val = create(key);
					base[key] = val;
					return val;
				} else {
					return base[key];
				}
			}
			private set {
				base[key] = value;
			}
		}
	}
	
}
