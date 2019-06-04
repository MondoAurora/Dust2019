/*
 * Created by SharpDevelop.
 * User: Montru / Lorand Kedves
 * Date: 19/06/04
 * Time: 09:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Reflection;
using System.Linq;

namespace Dust
{
	public class DustKey
	{
		public readonly string module;
		public readonly string key;
		
		protected DustKey(string module, int key)
		{
			this.module = module;
			this.key = key.ToString();
		}
		
		public static IEnumerable GetAll<T>() where T : DustKey
		{
			var fields = typeof(T).GetFields(BindingFlags.Public |
			                  BindingFlags.Static |
			                  BindingFlags.DeclaredOnly); 

			return fields.Select(f => f.GetValue(null)).Cast<T>();
		}
	}

	public abstract partial class Dust : DustKernel
	{
		private static readonly object sysLock = new object();

		protected static void init(DustKernel system)
		{ 
			lock (sysLock) { 
				if (null == Dust.dustImpl) {
					Dust.dustImpl = system;
				}
			}
		}
	}
}
