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

using Dust;
using Dust.Utils.CSharp;

namespace Dust.Kernel
{
	public class DustEntityRef : DustEntity
	{
		public readonly DustEntityInstance eLinkDef;
		
		public readonly DustEntityInstance eSrc;
		public readonly DustEntityInstance eTarget;
		
		private Object coll;
		private Object id;
		
		public DustEntityRef(DustEntityInstance eLinkDef, DustEntityInstance eSrc, DustEntityInstance eTarget)
		{
			this.eLinkDef = eLinkDef;
			this.eSrc = eSrc;
			this.eTarget = eTarget;
		}
		
		public DustEntityRef addItem(DustEntityInstance target, Object hint)
		{
			DustEntityRef refAdd = null;
			
			switch (eLinkDef.optValType) {
				case DustValType.LinkDefSet:
					HashSet<DustEntityRef> hs;
					if (null == coll) {
						if (this.eTarget == target) {
							return this;
						}
						coll = hs = new HashSet<DustEntityRef>();
						hs.Add(this);
					} else {
						hs = (HashSet<DustEntityRef>)coll;
						foreach (var rr in hs) {
							if (rr.eTarget == target) {
								return rr;
							}
						}
					}
					
					refAdd = new DustEntityRef(eLinkDef, eSrc, target);
					refAdd.coll = coll;
					
					hs.Add(refAdd);
					break;
				case DustValType.LinkDefArray:
					List<DustEntityRef> lst;
					if (null == coll) {
						coll = lst = new List<DustEntityRef>();
						lst.Add(this);
						this.id = 0;
					} else {
						lst = (List<DustEntityRef>)coll;
					}
					
					refAdd = new DustEntityRef(eLinkDef, eSrc, target);
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
					break;
			}
			
			return refAdd;
		}
	}
	
	public class DustEntityInstance : DustEntity
	{
		public DustValType optValType;

		readonly Dictionary<DustEntityInstance, Object> content = new Dictionary<DustEntityInstance, Object>();
		
		public void setValue(DustEntityInstance key, Object val, Object hint)
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
					newVal = new DustEntityRef(key, this, (DustEntityInstance)val);
					break;
				case DustValType.LinkDefSet:
				case DustValType.LinkDefArray:
				case DustValType.LinkDefMap:
					newVal = (null == oldVal) ?	new DustEntityRef(key, this, (DustEntityInstance)val) 
						: ((DustEntityRef)oldVal).addItem((DustEntityInstance)val, hint);
					break;
			}
			
			content[key] = newVal;
		}
		
		public TVal getValue<TVal>(DustEntityInstance key, TVal defVal)
		{
			Object ret;
			return content.TryGetValue(key, out ret) ? (TVal)ret : defVal;
		}
	}
	
	public class DustEntityStore : DustUtilFactory<String, DustEntityInstance>
	{
		public DustEntityStore()
			: base(typeof(DustEntityInstance))
		{
		}

	}
	
	public class DustSession
	{
		public readonly DustSession parent;
		
		public readonly DustUtilFactory<String, DustEntityStore> modules = new DustUtilFactory<String, DustEntityStore>(typeof(DustEntityStore));
		public readonly Dictionary<DustContext, DustEntityInstance> ctx = new Dictionary<DustContext, DustEntityInstance>();

		public readonly HashSet<DustEntityInstance> managedInstances = new HashSet<DustEntityInstance>();
		public readonly HashSet<DustEntityRef> managedReferences = new HashSet<DustEntityRef>();
		
		public DustSession()
			: this(null)
		{
		}
		
		public DustSession(DustSession p)
		{
			this.parent = p;
		}
		
		public DustEntityInstance getEntity(String module, String entity)
		{
			return modules[module][entity];
		}
		
	}
	
	public class DustSystem : Dust
	{
		DustSession rootSession = new DustSession();

		override public double getValDoubleImpl(DustEntity entity, DustKey key, double defVal)
		{
			DustEntityInstance eKey = rootSession.modules[key.module][key.key];
			
			DustEntityInstance ei = rootSession.ctx[((DustContext)entity)];
			return ei.getValue(eKey, defVal);
		}
		
		public static DustSystem getSystem() {
			return (DustSystem) Dust.dustImpl;
		}
		
		public static DustSession getSession() {
			return getSystem().rootSession;
		}
		
		public static void initKernel(string[] args) {
			Dust.init(new DustSystem());
		}
	}
}
