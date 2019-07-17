package montru.java.utils;

import java.util.Collection;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;

public abstract class DustUtilsConfig implements DustUtilsComponents.Settings {
	String CFG_KEYVALUESEP = "=";
	String CFG_LISTFLAG = "*";
	
	public interface Configurable {
		void initWithConfig(DustUtilsConfig config) throws Exception;
	}
	
	@Override
	public <ValType> ValType get(Object key) {
	    ValType ret = null;
	    
	    if ( key instanceof String ) {
	        ret = getCfg((String) key);
	    } else if ( key instanceof Enum ){
	        Enum<?> e = (Enum<?>) key;
	        
	        String sk = e.name();
	        ret = getCfg(sk);
	        
	        if ( null == ret ) {
	            sk = e.getClass().getSimpleName() + "." + sk;
	            ret = getCfg(sk);
	        }
	    } else {
	        ret = getCfg(DustUtilsJava.toString(key));
	    }
	    
	    return ret;
	}
	
	@Override
	public boolean isSet(Object key) {
	    return Boolean.TRUE.equals(get(key));
	}
	
	@Override
	public boolean isSet(Object key, String value) {
	    Object v = get(key);
	    return (v instanceof String) ? DustUtilsJava.isEqual(v, value) : (v instanceof Collection) ? ((Collection<?>)v).contains(value) : false;
	}

	protected abstract <ValType> ValType getCfg(String key);
	
	protected void loadCfgString(String str, Map<String, Object> target) {
		int idx = str.indexOf(CFG_KEYVALUESEP);
		if (-1 == idx) {
			target.put(str, Boolean.TRUE.toString());
		} else {
			String key = str.substring(0, idx);
			String val = str.substring(idx + 1);
			if (key.endsWith(CFG_LISTFLAG)) {
				key = key.substring(0, idx - 1);
				String ls = val.substring(0, 1);
				
				Set<String> s = new HashSet<>();
				for ( String v : val.substring(1).split(ls)) {
				    s.add(v.trim());
				}
				target.put(key, s);
			} else {
				target.put(key, val);
			}
		}
	}

	public static class Std extends DustUtilsConfig {
		@Override
		@SuppressWarnings("unchecked")
		public <ValType> ValType getCfg(String key) {
			String ret = System.getProperty(key);
			return (ValType) ((null == ret) ? System.getenv(key) : ret);
		}
	}

	public static class DustConfigConsole extends Std {
		Map<String, Object> cmdLineCfg = new HashMap<>();

		public DustConfigConsole(String[] cmdLine) {
			for (String par : cmdLine) {
				loadCfgString(par, cmdLineCfg);
			}
		}

		@Override
		@SuppressWarnings("unchecked")
		public <ValType> ValType getCfg(String key) {
			ValType ret = (ValType) cmdLineCfg.get(key);

			return (null == ret) ? super.getCfg(key) : ret;
		}
	}
}
