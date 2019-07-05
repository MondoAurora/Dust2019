/*
 * Created by SharpDevelop.
 * User: Montru / Lorand Kedves
 * Date: 19/06/06
 * Time: 11:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

using Dust;

namespace Dust.Kernel
{
	class VisitContext
	{
		public DustVisitTray tray;
		
		public VisitContext(DustVisitTray tray)
		{
			this.tray = tray;
		}
	}
	
	public class DustProcVisitProcess
	{
		static Object VISITED = new Object();
		readonly DustSession session;
		readonly DustInfoTray infoTray;
		readonly DustVisitTray visitTray;
		
		DustVisitTray callbackTray;
		
		readonly DustInfoProcessor dip;
		readonly DustInfoFilter dif;
		readonly DustVisitor dvp;
		
		bool empty;

		Dictionary<DustDataEntity, Object> visitedEntities = new Dictionary<DustDataEntity, Object>();
		List<DustDataReference> callStack = new List<DustDataReference>();
		
		public DustProcVisitProcess(DustSession session, DustInfoTray tray)
		{
			this.session = session;
			this.infoTray = tray;
			this.visitTray = tray as DustVisitTray;
			callbackTray = (null == visitTray) ? new DustVisitTray(tray, null) : new DustVisitTray(visitTray);
			callbackTray.entity = session.resolveEntity(tray.entity);
			
			empty = true;
			
			dip = tray.value as DustInfoProcessor;
			dif = tray.value as DustInfoFilter;
			dvp = tray.value as DustVisitor;
		}
				
		private void optCloseVisit()
		{
			if (!empty) {
				sendVisitEvent(VisitEvent.visitEnd);
				callbackTray = null;
			}
		}
		
		public bool sendVisitEvent(VisitEvent ve)
		{
			callbackTray.resp = VisitResponse.success;
			
			if (empty) {
				empty = false;
				sendVisitEvent(VisitEvent.visitStart);
			}

			dvp.processVisitEvent(ve, callbackTray);
			
			if (VisitResponse.abort == callbackTray.resp) {
				throw new DustException();
			}
			
			return VisitResponse.success == callbackTray.resp;
		}

		public void visitEntity()
		{
			var e = session.resolveEntity(callbackTray.entity);
			
			if (null != e) {
				Exception procEx = null;

				try {
					bool found = visitedEntities.ContainsKey(e);
					bool nochk = callbackTray.cmd.HasFlag(VisitCommand.recNoCheck);
					if (visitedEntities.ContainsKey(e) && !callbackTray.cmd.HasFlag(VisitCommand.recNoCheck)) {
						callbackTray.rawHint = visitedEntities[e];
						sendVisitEvent(VisitEvent.entityRevisit);
					} else {
						callbackTray.entity = e;
						callbackTray.rawHint = VISITED;
					
						if (sendVisitEvent(VisitEvent.entityStartOpt)) {
							Object rh = callbackTray.rawHint;
							visitedEntities[e] = rh;				
			
							foreach (var ec in e.content.Keys) {
								callbackTray.key = ec;
								var val = e.content[ec];
				
								if (null != val) {
									if (sendVisitEvent(VisitEvent.keyStartOpt)) {
										var r = val as DustDataReference;
					
										if ((null == r) && callbackTray.cmd.HasFlag(VisitCommand.visitAtts)) {
											callbackTray.value = val;
											callbackTray.rawHint = rh;
											dip.processInfo(callbackTray);
										} else if ((null != r) && callbackTray.cmd.HasFlag(VisitCommand.visitRefs)) {
											visitRef(e, ec, false);
										}
									
										callbackTray.key = ec;
										sendVisitEvent(VisitEvent.keyEnd);
									}
								}
							}
						
							callbackTray.rawHint = rh;
							sendVisitEvent(VisitEvent.entityEnd);
							visitedEntities[e] = callbackTray.rawHint;					
						}
					}
				} catch (Exception ex) {
					callbackTray.rawHint = procEx = ex;
					sendVisitEvent((ex is DustException) ? VisitEvent.visitAborted : VisitEvent.visitInternalError);
				} finally {
					if (callbackTray.cmd.HasFlag(VisitCommand.recPathOnce)) {
						visitedEntities.Remove(e);
					}
				}
				
				if (null != procEx) {
					throw procEx;
				}
			}
		}
		
		public bool visitRef(DustDataEntity ei, DustDataEntity eKey, bool close)
		{
			bool first = true;
			DustProcCursor cursor = session.optSetCursor(ei, eKey);
			
			if (null != cursor) {
				Exception procEx = null;
				try {
					foreach (DustDataReference ddr in cursor) {
						callbackTray.entity = ei;
						callbackTray.value = ddr.eTarget;
						callbackTray.key = eKey;
									
						if (null != dvp) {
							if (first) {
								first = false;
							} else {
								sendVisitEvent(VisitEvent.refSep);
							}
						}
						
						if (null != dvp) {
							object mapId = (DustValType.LinkDefMap == ddr.eLinkDef.optValType) ? ddr.getId() : null;

							if (null != mapId) {
								callbackTray.key = mapId;
								if (!sendVisitEvent(VisitEvent.keyStartOpt)) {
									continue;
								}
							}
							
							callbackTray.entity = ddr.eTarget;
							visitEntity();
							
							if (null != mapId) {
								callbackTray.key = mapId;
								sendVisitEvent(VisitEvent.keyEnd);
								callbackTray.key = eKey;
							}
						}
					}
								
					if (!first && close) {
						sendVisitEvent(VisitEvent.visitEnd);
					} 
				} catch (Exception ex) {
					callbackTray.rawHint = procEx = ex;
					sendVisitEvent((ex is DustException) ? VisitEvent.visitAborted : VisitEvent.visitInternalError);
				} finally {
					session.cursors.Remove(cursor);						
				}
				
				if (null != procEx) {
					throw procEx;
				}
			}
			
			return !first;
		}
	}
}
