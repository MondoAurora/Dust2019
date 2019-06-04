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
		
		public static double getValDouble(DustEntity entity, DustKey key, double defVal)
		{
			var tray = new DustInfoTray(entity, key, defVal);
			Dust.access(DustOperation.get, tray);
			return (double)tray.value;
		}
	}
}
