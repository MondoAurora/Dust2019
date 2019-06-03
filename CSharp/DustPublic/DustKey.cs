/*
 * Created by SharpDevelop.
 * User: loran
 * Date: 19/05/27
 * Time: 12:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Reflection;
using System.Linq;

namespace Dust
{
	/// <summary>
	/// Description of DustKey.
	/// </summary>
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
}
