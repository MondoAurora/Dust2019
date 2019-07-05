/*
 * Created by SharpDevelop.
 * User: Montru / Lorand Kedves
 * Date: 19/07/05
 * Time: 12:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Dust
{
	public class DustUtilKeyFinder
	{
		readonly DustKey[] keys;
		readonly DustEntity[] entities;
		
		public DustUtilKeyFinder(params DustKey[] keys)
		{
			this.keys = keys;
			this.entities = new DustEntity[keys.Length];
			
			for (int i = keys.Length; i-- > 0;) {
				entities[i] = Dust.resolveKey(keys[i]);
			}
		}
		
		public int indexOf(DustEntity e)
		{
			return Array.IndexOf(entities, e);
		}
		
		public DustKey keyOf(DustEntity e, int defIdx)
		{
			int idx = indexOf(e);
			return keyOf((-1 < idx) ? idx : defIdx);
		}
		
		public DustKey keyOf(int idx)
		{
			return (-1 < idx) ? keys[idx] : null;
		}
	}
}
