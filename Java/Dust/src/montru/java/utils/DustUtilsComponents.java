package montru.java.utils;

import java.nio.charset.StandardCharsets;

public interface DustUtilsComponents {
    String DUST_CHARSET_UTF8 = StandardCharsets.UTF_8.name();

    String DUST_ID_SEP = ":";
    String DUST_DEFAULT_SEPARATOR = ",";
    String DUST_MULTI_FLAG = "*";

    interface DumpFormatter {

    }

    public interface Settings {
        boolean isSet(Object key);
        boolean isSet(Object key, String value);
        <ValType> ValType get(Object key);
    }

    public enum CollectionAction {
        contains, add, remove, clear
    }

}
