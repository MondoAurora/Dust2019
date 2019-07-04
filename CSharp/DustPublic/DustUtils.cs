/*
 * Created by SharpDevelop.
 * User: Montru / Lorand Kedves
 * Date: 19/06/04
 * Time: 10:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Dust
{
	public static class DustUtils
	{
		public static bool isEnumRef(DustValType valType)
		{
			return valType < DustValType.LinkDefSingle;
		}
				
		public static int indexOf(DustEntity entity, params DustKey[] keys)
		{
			int idx = 0;
			
			foreach (DustKey key in keys) {
				if ( entity == Dust.resolveKey(key) ) {
					return idx;
				}
				++ idx;
			}
			
			return -1;
		}
				
		public static VType getValue<VType>(DustEntity entity, VType defVal, params DustKey[] keys)
		{
			var tray = new DustInfoTray(entity, null);
			DustEntity e = null;
			
			foreach (DustKey key in keys) {
				tray.key = key;
				if (null != e) {
					tray.entity = e;
				}
				Dust.access(DustAccessCommand.read, tray);
				
				e = tray.value as DustEntity;
			}
			
			return (VType) (tray.value ?? defVal);
		}

	}
}
