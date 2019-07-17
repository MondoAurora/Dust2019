package montru.java.utils;

import java.text.MessageFormat;
import java.util.Collection;
import java.util.EnumSet;
import java.util.Map;

public class DustUtilsJava implements DustUtilsComponents {
    
    public static String optFormat(String message, Object... params) {
        return (0==params.length) ? message : MessageFormat.format(message, params);
    }

	public static <FlagType extends Enum<FlagType>> void optSetFlag(EnumSet<FlagType> target, FlagType flag,
			boolean condition) {
		if (condition) {
			target.add(flag);
		}
	}

	public static String toString(Object ob) {
		return (null == ob) ? "" : ob.toString();
	}

	public static String toLocalId(Enum<?> e) {
		return new StringBuilder(e.getClass().getSimpleName()).append(DUST_ID_SEP).append(e.name()).toString();
	}

	public static String toEnumId(Enum<?> e) {
		return new StringBuilder(e.getClass().getName()).append(DUST_ID_SEP).append(e.name()).toString();
	}

	@SuppressWarnings("unchecked")
	public static <RetType extends Enum<RetType>> RetType fromEnumId(String s) throws ClassNotFoundException {
		String[] ee = s.split(DUST_ID_SEP);
		return parseEnum(ee[1], (Class<RetType>) Class.forName(ee[0]));
	}

	public static <RetType extends Enum<RetType>> RetType parseEnum(String name, Class<RetType> ec) {
		for (RetType e : ec.getEnumConstants()) {
			if (e.name().equals(name)) {
				return e;
			}
		}

		return null;
	}

	public static boolean isEmpty(String str) {
		return (null == str) ? true : str.isEmpty();
	}

	public static StringBuilder sbAppend(StringBuilder sb, Object sep, boolean strict, Object... objects) {
		for (Object ob : objects) {
			String str = toString(ob);

			if (strict || (0 < str.length())) {
				if (null == sb) {
					sb = new StringBuilder(str);
				} else {
					if (0 < sb.length()) {
						sb.append(sep);
					}
					sb.append(str);
				}
			}
		}
		return sb;
	}

	@SuppressWarnings("unchecked")
	public static <Content> Content safeGet(int idx, Object... arr) {
		return ((null != arr) && (0 < idx) && (idx < arr.length)) ? (Content) arr[idx] : null;
	}

	public static boolean isEqual(Object o1, Object o2) {
		return (null == o1) ? (null == o2) : (null == o2) ? false : (o1 == o2) ? true : o1.equals(o2);
	}

	public static boolean isEqualLenient(Object ob, Object opt) {
		return (null == opt) || ((null != ob) && opt.equals(ob));
	}

	public static <RetType extends Enum<RetType>> RetType shiftEnum(RetType e, boolean up, boolean rotate) {
		int ord = e.ordinal();
		@SuppressWarnings("unchecked")
		RetType[] values = (RetType[]) e.getClass().getEnumConstants();
		ord = up ? ++ord : --ord;

		if ((0 <= ord) && (ord < values.length)) {
			return values[ord];
		} else {
			if (rotate) {
				return ((0 > ord) ? values[values.length - 1] : values[0]);
			} else {
				return null;
			}
		}
	}

	@SuppressWarnings("rawtypes")
	public static StringBuilder toStringBuilder(StringBuilder target, Iterable<?> content, boolean map, String name) {
		if (null == content) {
			return null;
		}

		if (!isEmpty(name)) {
			target = DustUtilsJava.sbAppend(target, "", false, " \"", name, "\": ");
			target = DustUtilsJava.sbAppend(target, "", false, map ? "{ " : "[ ");
		}

		boolean empty = true;
		for (Object r : content) {
			if (empty) {
				empty = false;
			} else {
				target.append(", ");
			}
			if (r instanceof Map.Entry) {
				Map.Entry e = (Map.Entry) r;
				Object val = e.getValue();
				if (!(val instanceof DumpFormatter)) {
					val = sbAppend(null, "", false, "\"", val, "\"");
				}
				sbAppend(target, "", false, " \"", e.getKey(), "\": ", val);
			} else {
				target = DustUtilsJava.sbAppend(target, "", false, r);
			}
		}
		if (!isEmpty(name)) {
			target.append(map ? " }" : " ]");
		}
		return target;
	}

	@SuppressWarnings({ "rawtypes", "unchecked" })
	public static <RetType> RetType getByPath(Object from, Object... path) {
		Object ret = from;
		for (Object k : path) {
			if (k instanceof Enum) {
				k = ((Enum) k).name();
			}
			ret = ((Map<String, Object>) ret).get(k);
		}
		return (RetType) ret;
	}

	public static void biDiPut(Map<Object, Object> target, Object o1, Object o2) {
		target.put(o1, o2);
		target.put(o2, o1);
	}

	public static int indexOf(Object val, Object... items) {
		if ( null != items ) {
			int idx = 0;
			for ( Object it : items ) {
				if ( val == it ) {
					return idx;
				} else {
					++idx;
				}
			}
		}
		
		return -1;
	}

	@SuppressWarnings({ "rawtypes", "unchecked" })
	public static boolean manageCollection(CollectionAction action, Collection coll, Object obj) {
		switch (action) {
		case add:
			if ( coll.contains(obj) ) {
				return false;
			} else {
				return coll.add(obj);
			}
		case contains:
			return coll.contains(obj);
		case remove:
			return coll.remove(obj);
		case clear:
			if (coll.isEmpty()) {
				return false;
			} else {
				coll.clear();
				return true;
			}
		}

		throw new RuntimeException("Should not get here");
	}

}
