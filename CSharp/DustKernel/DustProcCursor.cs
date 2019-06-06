/*
 * Created by SharpDevelop.
 * User: Montru / Lorand Kedves
 * Date: 19/06/06
 * Time: 11:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using Dust.Utils.CSharp;

namespace Dust.Kernel
{
	public class DustProcCursor : DustUtilIterator<DustDataReference>
	{
		public readonly DustDataEntity owner;
		public readonly DustDataEntity key;
		
		public static DustProcCursor optSet(DustDataEntity owner, DustDataEntity key)
		{
			DustDataReference ddr = owner.getRef(key);
			return (null == ddr) ? null : new DustProcCursor(ddr);
		}
		
		DustProcCursor(DustDataReference ddr)
			: base(ddr.getMembers())
		{
			this.owner = ddr.eSrc;
			this.key = ddr.eLinkDef;
		}
	}

}
