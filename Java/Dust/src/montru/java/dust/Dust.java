package montru.java.dust;

import java.util.Arrays;

import montru.java.utils.DustUtilsConfig;
import montru.java.utils.DustUtilsDev;
import montru.java.utils.DustUtilsJava;

public class Dust implements DustComponents {

    private static DustRuntime RUNTIME;

    public static <RetType> RetType access(DustAccessCommand cmd, DustInfoTray tray) {
        return RUNTIME.accessImpl(cmd, tray);
    }

    public static void visit(DustVisitCommand cmd, DustVisitTray tray) {
        RUNTIME.visitImpl(cmd, tray);
    }

    public static void optWrapException(Throwable src, String msg, Object... params) {
        if (src instanceof DustException) {
            throw (DustException) src;
        } else {
            src.printStackTrace();
            throw new DustException(src, msg, params);
        }
    }
    

    public static void main(String[] args) throws Exception {
        DustUtilsDev.dump("Dust.main() calls init with parameters:", Arrays.asList(args));
        init(new DustUtilsConfig.DustConfigConsole(args));
    }
    
    protected enum DustLaunchConfig {
        ModuleRoot, RuntimeClass, RuntimeModule, BootModuleNames
    }

    public interface DustContext {
        <RetType> RetType accessImpl(DustAccessCommand cmd, DustInfoTray tray);
        void visitImpl(DustVisitCommand cmd, DustVisitTray tray);
    }

    public interface DustRuntime extends DustContext, DustUtilsConfig.Configurable {
        void launch() throws Exception;
    }

    protected static void init(DustUtilsConfig cfg) throws Exception {
        String modRoot = cfg.get(DustLaunchConfig.ModuleRoot);
        
        if ( !DustUtilsJava.isEmpty(modRoot) ) {
            DustClassLoader.setModuleRoot(modRoot);
        }
        
        String rtMod = cfg.get(DustLaunchConfig.RuntimeModule);
        String rtClassName = cfg.get(DustLaunchConfig.RuntimeClass);

        DustUtilsDev.dump("Found runtime class:", rtClassName);

        Class<?> c = DustClassLoader.getClass(rtMod, rtClassName);
        RUNTIME = (DustRuntime) c.newInstance();

        DustUtilsDev.dump("Initializing runtime...");

        RUNTIME.initWithConfig(cfg);

        DustUtilsDev.dump("Launching...");

        RUNTIME.launch();

        DustUtilsDev.dump("Exited normally.");
    }
}
