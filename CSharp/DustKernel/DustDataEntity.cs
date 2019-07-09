/*
 * Created by SharpDevelop.
 * User: Montru / Lorand Kedves
 * Date: 19/06/04
 * Time: 15:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

using Dust;
using Dust.Units.Data;
using Dust.Units.Meta;

namespace Dust.Kernel
{

	public class DustDataEntity : DustEntity
	{
		public DustValType optValType;

		public readonly Dictionary<DustDataEntity, Object> content = new Dictionary<DustDataEntity, Object>();
		
		public Object setValue(DustDataEntity key, Object val, Object hint)
		{
			Object newVal = null;
			
			Object oldVal;
			if (!content.TryGetValue(key, out oldVal)) {
				oldVal = null;
			}
			
			switch (key.optValType) {
				case DustValType.AttDefBool:
				case DustValType.AttDefIdentifier:
				case DustValType.AttDefDouble:
				case DustValType.AttDefLong:
				case DustValType.AttDefRaw:
					newVal = val;
					break;
				case DustValType.LinkDefSingle:
					newVal = new DustDataReference(key, this, (DustDataEntity)val, hint);
					break;
				case DustValType.LinkDefSet:
				case DustValType.LinkDefArray:
				case DustValType.LinkDefMap:
					newVal = (null == oldVal) ?	new DustDataReference(key, this, (DustDataEntity)val, hint) 
						: ((DustDataReference)oldVal).addItem((DustDataEntity)val, hint);
					break;
			}
			
			content[key] = newVal;
			
			var oldRef = oldVal as DustDataReference;
			
			return ((null == oldRef) ? oldVal : oldRef.eTarget);
		}
		
		public DustDataReference getRef(DustDataEntity key)
		{
			Object ret;
			return (content.TryGetValue(key, out ret)) ? ret as DustDataReference : null;
		}
		
		public Object getValue(DustDataEntity key, Object defVal, object hint)
		{
			Object ret = defVal;
			DustDataReference er = null;
			
			if (content.TryGetValue(key, out ret)) {
				er = ret as DustDataReference;
				ret = (null == er) ? ret : er.getAt(hint);
			}
			
			if ((null == ret) && (DustValType.LinkDefMap == key.optValType)) {
				Object refPT;
				if (content.TryGetValue(DustSystem.getEntity(MetaLinks.LinkDefItemTypePrimary), out refPT)) {
					DustDataEntity ePT = ((DustDataReference) refPT).eTarget;
					
					var eNew = new DustDataEntity();
					
					eNew.setValue(DustSystem.getEntity(DataLinks.EntityPrimaryType), ePT, null);
					ret = eNew;
				}
			}
			
			return ret ?? defVal;
		}
		
		override
		public String ToString() {
			return "Entity " + GetHashCode();
		}
	}
}
