package montru.java.dust.modules.json;

import java.io.FileReader;
import java.io.Reader;

import org.json.simple.parser.JSONParser;

import montru.java.gen.dust.kernel.DustMetaComponents;
import montru.java.utils.DustUtilsDev;

public class DustJsonTest {

    public DustJsonTest() throws Exception {
        DustUtilsDev.dump("DustJsonTest", DustMetaComponents.DustMetaTypes.Type);

        Reader r = new FileReader("GeoTest02.23.json");
        Object o = new JSONParser().parse(r);
        
        DustUtilsDev.dump(o);
    }
}
