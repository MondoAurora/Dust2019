package montru.java.dust;

import montru.java.utils.DustUtilsComponents;

public interface DustComponents extends DustUtilsComponents {

    public interface DustEntity {
    }

    public interface DustEntityKey extends DustEntity {
    }

    public interface DustEntityMapped extends DustEntityKey {
        String getPersistentId();
    }

    enum ContextRef implements DustEntity {
        msg, self, session
    }

    public class DustInfoTray
    {
        public DustEntity entity;
        public Object key;        
        public Object value;
                
        public DustInfoTray()
        {
        }
        
        public DustInfoTray(DustEntity owner)
        {
        }
        
        public DustInfoTray(DustEntity owner, Object value, Object... keys)
        {
            this.entity = owner;
            this.key = keys;
            this.value = value;
        }

        public DustInfoTray(DustInfoTray src)
        {
            loadSrc(src);
        }
        
        public void loadSrc(DustInfoTray src)
        {
            this.entity = src.entity;
            this.key = src.key;
            this.value = src.value;
        }
    }
    
    public enum VisitEvent
    {
        visitStart,
        processValue,
        keyStartOpt,
        refSep,
        entityStartOpt,
        entityEnd,
        entityRevisit,
        keyEnd,
        visitEnd,
        
        visitAborted,
        visitInternalError
    }
    
    public enum DustVisitCommand
    {
        none,
        
        visitAtts,
        visitRefs,
        visitMeta,
        
        recEntityOnce,
        recPathOnce, 
        recNoCheck,
    }
    
    public enum VisitResponse
    {
        success,
        skip,
        abort,
    }
    
    public class DustVisitTray extends DustInfoTray
    {
        public DustVisitor visitor;
        
        public Object readerObject;
        public Object readerParent;
        
        public Object result;
        public VisitResponse resp;
        
        public DustVisitTray(DustEntity eRoot, DustVisitor visitor)
        {
            super(eRoot);
            this.visitor = visitor;
        }
        
        public DustVisitTray(DustInfoTray src, DustVisitor visitor)
        {
            super(src);
            this.visitor = visitor;
        }
        
        public DustVisitTray(DustVisitTray src)
        {
            loadSrc(src);
        }
        
        public void loadSrc(DustVisitTray src)
        {
            super.loadSrc(src);
            
            this.visitor = src.visitor;
            this.result = src.result;
        }
    }

    public enum DustAccessCommand
    {
        infoGet,
        infoSet,
        infoDel,
        
        entityGet,
        entityClone,
        entityDel,
    }
    
    public interface DustInfoProcessor
    {
        void processInfo(DustInfoTray tray);
    }
    
    public interface DustVisitor extends DustInfoProcessor
    {
        void processVisitEvent(VisitEvent visitEvent, DustVisitTray tray);
    }

    public enum DustValType
    {
        NotSet,
        AttDefIdentifier,
        AttDefBool,
        AttDefLong,
        AttDefDouble,
        AttDefRaw,
        LinkDefSingle,
        LinkDefSet,
        LinkDefArray,
        LinkDefMap
    }

    public class DustException extends RuntimeException {
        private static final long serialVersionUID = 1L;

        public DustException(Throwable cause, String message, Object... params) {
            super(DustUtils.optFormat(message, params), cause);
        }

        public DustException(String message, Object... params) {
            super(DustUtils.optFormat(message, params));
        }
    }
}
