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
		//		readonly DustInfoFilter dif;
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
//			dif = tray.value as DustInfoFilter;
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

		public void visitEntity(object readerParent)
		{
			var e = session.resolveEntity(callbackTray.entity);
			
			if (null != e) {
				Exception procEx = null;

				try {
					bool found = visitedEntities.ContainsKey(e);
					bool nochk = callbackTray.cmd.HasFlag(VisitCommand.recNoCheck);
					if (visitedEntities.ContainsKey(e) && !callbackTray.cmd.HasFlag(VisitCommand.recNoCheck)) {
						callbackTray.readerObject = visitedEntities[e];
						sendVisitEvent(VisitEvent.entityRevisit);
					} else {
						callbackTray.entity = e;
						callbackTray.readerParent = readerParent;
						callbackTray.readerObject = VISITED;
					
						if (sendVisitEvent(VisitEvent.entityStartOpt)) {
							Object rh = callbackTray.readerObject;
							visitedEntities[e] = rh ?? VISITED;				
			
							foreach (var ec in e.content.Keys) {
								callbackTray.key = ec;
								var val = e.content[ec];
				
								if (null != val) {
									callbackTray.readerObject = rh;
									if (sendVisitEvent(VisitEvent.keyStartOpt)) {
										var r = val as DustDataReference;
					
										if ((null == r) && callbackTray.cmd.HasFlag(VisitCommand.visitAtts)) {
											callbackTray.value = val;
											callbackTray.readerObject = rh;
											dip.processInfo(callbackTray);
									
											callbackTray.key = ec;
											callbackTray.readerObject = rh;
											callbackTray.readerParent = readerParent;
											sendVisitEvent(VisitEvent.keyEnd);
										} else if ((null != r) && callbackTray.cmd.HasFlag(VisitCommand.visitRefs)) {
											visitRef(e, ec, rh, false);
										}
									}
								}
							}
						
							callbackTray.readerObject = rh;
							callbackTray.readerParent = readerParent;
							sendVisitEvent(VisitEvent.entityEnd);
							visitedEntities[e] = callbackTray.readerObject ?? VISITED;					
						}
					}
				} catch (Exception ex) {
					callbackTray.readerObject = procEx = ex;
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
		
		public bool visitRef(DustDataEntity ei, DustDataEntity eKey, object readerParent, bool close)
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
								callbackTray.readerParent = readerParent;
								callbackTray.entity = ddr.eTarget;

								if (!sendVisitEvent(VisitEvent.keyStartOpt)) {
									callbackTray.entity = ei;
									continue;
								}
							}
							
							callbackTray.entity = ddr.eTarget;
							visitEntity(readerParent);
							
							if (null != mapId) {
								callbackTray.key = mapId;
								callbackTray.readerParent = readerParent;
								callbackTray.entity = ddr.eTarget;
								sendVisitEvent(VisitEvent.keyEnd);
							}
																
							callbackTray.key = eKey;
							callbackTray.entity = ddr.eTarget;
							callbackTray.readerParent = readerParent;
							sendVisitEvent(VisitEvent.keyEnd);
						}
					}
								
					if (!first && close) {
						sendVisitEvent(VisitEvent.visitEnd);
					} 
				} catch (Exception ex) {
					callbackTray.readerObject = procEx = ex;
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
