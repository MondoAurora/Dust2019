/*
 * Created by SharpDevelop.
 * User: Montru / Lorand Kedves
 * Date: 19/06/04
 * Time: 15:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

using Dust;

namespace Dust.Kernel
{
	public class DustDataReference : DustEntity
	{
		public readonly DustDataEntity eLinkDef;
		
		public readonly DustDataEntity eSrc;
		public readonly DustDataEntity eTarget;
		
		private Object coll;
		private Object id;
		
		public DustDataReference(DustDataEntity eLinkDef, DustDataEntity eSrc, DustDataEntity eTarget, Object id)
		{
			this.eLinkDef = eLinkDef;
			this.eSrc = eSrc;
			this.eTarget = eTarget;
			this.id = id;
		}
		
		public Object getId()
		{
			return id;
		}
		
		public DustDataEntity getAt(Object hint)
		{
			switch (eLinkDef.optValType) {
				case DustValType.LinkDefSingle:
					return eTarget;
				case DustValType.LinkDefArray:
					var lst = coll as List<DustDataReference>;
					if ((null != lst) && (hint is int)) {
						var idx = (int)hint;
						if ((0 <= idx) && (idx < lst.Count)) {
							return lst[idx].eTarget;
						}
					}
					break;
				case DustValType.LinkDefMap:
					var map = coll as Dictionary<DustDataEntity, DustDataReference>;
					var key = hint as DustDataEntity;
					DustDataReference rr;
					if ((null != map) && (null != hint)) {
						if (map.TryGetValue(key, out rr)) {
							return rr.eTarget;
						}
					}
					break;
			}
			
			return null;
		}
		
		public DustDataReference addItem(DustDataEntity target, Object hint)
		{
			DustDataReference refAdd = null;
			
			switch (eLinkDef.optValType) {
				case DustValType.LinkDefSet:
					var hs = coll as HashSet<DustDataReference>;
					if (null == hs) {
						if (this.eTarget == target) {
							return this;
						}
						coll = hs = new HashSet<DustDataReference>();
						hs.Add(this);
					} else {
						foreach (var rr in hs) {
							if (rr.eTarget == target) {
								return rr;
							}
						}
					}
					
					refAdd = new DustDataReference(eLinkDef, eSrc, target, hint);
					refAdd.coll = coll;
					
					hs.Add(refAdd);
					break;
				case DustValType.LinkDefArray:
					var lst = coll as List<DustDataReference>;
					if (null == lst) {
						coll = lst = new List<DustDataReference>();
						lst.Add(this);
						this.id = 0;
					}
					
					refAdd = new DustDataReference(eLinkDef, eSrc, target, hint);
					refAdd.coll = coll;
					
					if (hint is int) {
						int idx = (int)hint;
						if ((-1 != idx) && (idx < lst.Count)) {
							lst[idx] = refAdd;
							refAdd.id = idx;
							return refAdd;
						}
					}
					
					refAdd.id = lst.Count;
					lst.Add(refAdd);
					
					break;
				case DustValType.LinkDefMap:
					var map = coll as Dictionary<DustDataEntity, DustDataReference>;
					var key = hint as DustDataEntity;
					
					if (key == null) {
						throw new System.ArgumentException("Invalid parameter", "hint");
					}
					
					refAdd = new DustDataReference(eLinkDef, eSrc, target, hint);
					refAdd.id = key;

					if (key != this.id) {
						if (null == map) {
							coll = map = new Dictionary<DustDataEntity, DustDataReference>();
							map[(DustDataEntity)this.id] = this;
						}
										
						refAdd.coll = coll;
						map[key] = refAdd;
					}
					
					break;
			}
			
			return refAdd;
		}
		
		public DustDataReference[] getMembers()
		{
			DustDataReference[] ret = null;
			
			if (null == coll) {
				ret = new DustDataReference[1];
				ret[0] = this;
			} else {
				switch (eLinkDef.optValType) {
					case DustValType.LinkDefSet:
						var st = coll as HashSet<DustDataReference>;
						ret = new DustDataReference[st.Count];
						st.CopyTo(ret);
						break;
					case DustValType.LinkDefArray:
						var lst = coll as List<DustDataReference>;
						ret = lst.ToArray();
						break;
					case DustValType.LinkDefMap:
						var mv = (coll as Dictionary<DustDataEntity, DustDataReference>).Values;
						ret = new DustDataReference[mv.Count];
						mv.CopyTo(ret, 0);
						break;
				}
			}
			
			return ret;
		}
	}
	
}
