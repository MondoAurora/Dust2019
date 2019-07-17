package montru.java.dust;

import java.util.HashMap;
import java.util.Map;

import montru.java.utils.DustUtilsJava;

public class DustEntityResolver extends DustUtilsJava implements DustComponents {
    private static Map<Object, DustEntity> keyToEntity = new HashMap<>();
    private static Map<DustEntity, Object> entityToKey = new HashMap<>();

    public static DustEntity register(String storeId, Object key) {
        DustInfoTray t = new DustInfoTray();
        t.key = storeId;
        DustEntity e = Dust.access(DustAccessCommand.entityGet, t);
        keyToEntity.put(key, e);
        entityToKey.put(e, key);
        return e;
    }

    public static DustEntity getEntity(Object key) {
        return keyToEntity.get(key);
    }

    @SuppressWarnings("unchecked")
    public static <RetType> RetType getKey(DustEntity e) {
        return (RetType) entityToKey.get(e);
    }
}
