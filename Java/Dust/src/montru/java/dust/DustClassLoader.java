package montru.java.dust;

import java.io.File;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLClassLoader;
import java.util.ArrayList;
import java.util.Collection;

import montru.java.utils.DustUtilsFactory;

public class DustClassLoader extends URLClassLoader {
    private static final ClassLoader CL_DUST = Dust.class.getClassLoader();
    private static File MODULE_ROOT;
    
    private static DustUtilsFactory<String, DustClassLoader> MODULE_LOADERS = new DustUtilsFactory<String, DustClassLoader>(true) {
        @Override
        protected DustClassLoader create(String key, Object... hints) {
            DustClassLoader ret = null;
            
            File modDir = new File(MODULE_ROOT, key);
            ArrayList<URL> urls = new ArrayList<>();
            
            if ( modDir.isDirectory() ) {
                for ( File f : modDir.listFiles() ) {
                    if ( f.getName().toLowerCase().endsWith(".jar") ) {
                        try {
                            urls.add(f.toURI().toURL());
                        } catch (MalformedURLException e) {
                            Dust.optWrapException(e, "JAR url creation error", key, f.getAbsolutePath());
                        }
                    }
                }
                
                ret = new DustClassLoader(urls);
            }
            
            return ret;
        }
    };
    
    public static void setModuleRoot(String modRoot) {
        MODULE_ROOT = new File(modRoot);
    }
    
    public static Class<?> getClass(String module, String className) throws Exception {
        return ( null == MODULE_ROOT ) ? Class.forName(className) : MODULE_LOADERS.get(module).loadClass(className);
    }
    
//    private static URL[] toURLs(String module) throws Exception {
//        File modDir = new File(MODULE_ROOT, module);
//        
//        
//        
//        URL[] ret = new URL[s.size()];
//        int idx = 0;
//        
//        for ( String su : s ) {
//            File f = new File(su);
//            ret[idx++] = f.toURI().toURL();
//        }
//        
//        return ret;
//    }

    private DustClassLoader(Collection<URL> urls) {
        super(urls.toArray(new URL[urls.size()]), CL_DUST);
    }
}