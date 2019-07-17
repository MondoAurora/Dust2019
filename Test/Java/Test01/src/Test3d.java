
import java.awt.BorderLayout;
import java.awt.Font;
import java.awt.GraphicsConfiguration;
import java.awt.Shape;

import javax.media.j3d.Alpha;
import javax.media.j3d.AmbientLight;
import javax.media.j3d.Appearance;
import javax.media.j3d.BoundingSphere;
import javax.media.j3d.BranchGroup;
import javax.media.j3d.Canvas3D;
import javax.media.j3d.ColoringAttributes;
import javax.media.j3d.DirectionalLight;
import javax.media.j3d.Font3D;
import javax.media.j3d.FontExtrusion;
import javax.media.j3d.Material;
import javax.media.j3d.RotationInterpolator;
import javax.media.j3d.Shape3D;
import javax.media.j3d.Text3D;
import javax.media.j3d.Transform3D;
import javax.media.j3d.TransformGroup;
import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.vecmath.Color3f;
import javax.vecmath.Vector3f;

import com.sun.j3d.utils.universe.SimpleUniverse;

/*
 * Text3DApp renders a single, rotating Text3D Object. The Text3D object has
 * material properties specified along with lights so that the Text3D object is
 * shaded.
 */

public class Test3d extends JPanel {
    private static final long serialVersionUID = 1L;

    public BranchGroup createSceneGraph() {
        // Create the root of the branch graph
        BranchGroup objRoot = new BranchGroup();

        Transform3D t3D = new Transform3D();
        t3D.setTranslation(new Vector3f(0.0f, 0.0f, -3.0f));
        TransformGroup objMove = new TransformGroup(t3D);
        objRoot.addChild(objMove);

        // Create the transform group node and initialize it to the
        // identity. Add it to the root of the subgraph.
        TransformGroup objSpin = new TransformGroup();
        objSpin.setCapability(TransformGroup.ALLOW_TRANSFORM_WRITE);
        objMove.addChild(objSpin);

        Appearance textAppear = new Appearance();
        ColoringAttributes textColor = new ColoringAttributes();
        textColor.setColor(1.0f, 0.0f, 0.0f);
        textAppear.setColoringAttributes(textColor);
        textAppear.setMaterial(new Material());

        double X1 = 0;
        double Y1 = 0;
        double X2 = 0.1;
        double Y2 = 0;
        Shape extrusionShape = new java.awt.geom.Line2D.Double(X1, Y1, X2, Y2);
     
        FontExtrusion fontEx = new FontExtrusion(extrusionShape);
     
        Font3D font3D = new Font3D(new Font("Helvetica", Font.PLAIN, 1), fontEx);
        Text3D textGeom = new Text3D(font3D, new String("3DText"));
        textGeom.setAlignment(Text3D.ALIGN_CENTER);
        Shape3D textShape = new Shape3D();
        textShape.setGeometry(textGeom);
        textShape.setAppearance(textAppear);
        objSpin.addChild(textShape);

        // Create a new Behavior object that will perform the desired
        // operation on the specified transform object and add it into
        // the scene graph.
        Alpha rotationAlpha = new Alpha(-1, 10000);

        RotationInterpolator rotator = new RotationInterpolator(rotationAlpha, objSpin);

        // a bounding sphere specifies a region a behavior is active
        // create a sphere centered at the origin with radius of 100
        BoundingSphere bounds = new BoundingSphere();
        rotator.setSchedulingBounds(bounds);
        objSpin.addChild(rotator);

        DirectionalLight lightD = new DirectionalLight();
        lightD.setInfluencingBounds(bounds);
        lightD.setDirection(new Vector3f(0.0f, 0.0f, -1.0f));
        lightD.setColor(new Color3f(1.0f, 0.0f, 1.0f));
        objMove.addChild(lightD);

        AmbientLight lightA = new AmbientLight();
        lightA.setInfluencingBounds(bounds);
        objMove.addChild(lightA);

        return objRoot;
    } // end of CreateSceneGraph method

    public Test3d() {
        setLayout(new BorderLayout());
    }

    void init() {
        GraphicsConfiguration config = SimpleUniverse.getPreferredConfiguration();
        Canvas3D canvas3D = new Canvas3D(config);
        canvas3D.setStereoEnable(false);
        add("Center", canvas3D);

        BranchGroup scene = createSceneGraph();

        // SimpleUniverse is a Convenience Utility class
        SimpleUniverse simpleU = new SimpleUniverse(canvas3D);

        // This will move the ViewPlatform back a bit so the
        // objects in the scene can be viewed.
        simpleU.getViewingPlatform().setNominalViewingTransform();

        simpleU.addBranchGraph(scene);
    } // end of Text3DApp (constructor)

    public static void main(String[] args) {
        JFrame frame = new JFrame("test");
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

        Test3d test3d = new Test3d();
        frame.getContentPane().add(test3d);

        test3d.init();

        frame.pack();
        frame.setVisible(true);

    } // end of main (method of Text3DApp)

} // end of class Text3DApp
