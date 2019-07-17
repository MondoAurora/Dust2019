package montru.java.dust.kernel;

import montru.java.dust.Dust;
import montru.java.dust.Dust.DustRuntime;
import montru.java.dust.DustClassLoader;
import montru.java.gen.dust.kernel.DustMetaComponents;
import montru.java.utils.DustUtilsConfig;
import montru.java.utils.DustUtilsDev;;

public class DustSystem extends Dust implements DustRuntime {

    @Override
    public <RetType> RetType accessImpl(DustAccessCommand cmd, DustInfoTray tray) {
        // TODO Auto-generated method stub
        return null;
    }

    @Override
    public void visitImpl(DustVisitCommand cmd, DustVisitTray tray) {
        // TODO Auto-generated method stub
        
    }

    @Override
    public void initWithConfig(DustUtilsConfig config) throws Exception {
        DustUtilsDev.dump("initWithConfig called.", DustMetaComponents.DustMetaTypes.Type);
        
//        Set<String> modNames = config.get(DustLaunchConfig.BootModuleNames);
    }

    @Override
    public void launch() throws Exception {
        
        Class<?> cc = DustClassLoader.getClass("json", "montru.java.dust.modules.json.DustJsonTest");
        cc.newInstance();
        DustUtilsDev.dump("launch called.");
    }

}
