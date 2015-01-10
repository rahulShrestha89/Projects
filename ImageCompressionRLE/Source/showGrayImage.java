

//////////////////////////////////////////////////////////////////////////////
//
//                                GRAYSCALE IMAGE VIEWER
//
//
// <> Constructor should be passed a 2D array of shorts which 
//    represents a gray scale image.
//
//    OR
//  
// <> Constructor can be passed a String, that is a file name
//    of a (P2) PGM picture file which will be imported.
//    PGM FILE FORMAT:
//          _____________________________
//          |P2                         |
//          |# Comments...              |
//          |COLUMNS ROWS               |
//          |MAX_VALUE                  |
//          |VALUES_IN_ROW_ORDER        |
//          |... 
//
// <> Pixel values are normalized to the range: [0...255].
//
// <> The user can drag over a portion of the image with the mouse  
//    to print the corresponding array values to the console.
//
//////////////////////////////////////////////////////////////////////////////


import javax.swing.JFrame;
import javax.swing.JPanel;
import java.awt.*;
import java.awt.event.*;
import java.util.Scanner;
import java.io.File;
import java.io.FileNotFoundException;



public class showGrayImage extends JFrame{
    private showImagePanel thePanel;
    
    public showGrayImage(short[][] image, String title) {
        this.setTitle(title);
        this.setDefaultCloseOperation(this.EXIT_ON_CLOSE);
        this.setSize(image.length, image[0].length);
        thePanel = new showImagePanel(image);
        this.getContentPane().add(thePanel);
        this.pack();
        this.setVisible(true);
    }
    
    public showGrayImage(String filename, String title) {
        short[][] image;
        this.setTitle(title);
        this.setDefaultCloseOperation(this.EXIT_ON_CLOSE);
        Scanner scan = null;
        try{
            scan = new Scanner(new File(filename));
        }catch(FileNotFoundException e){
            System.out.println("The File was not found: " + filename);
            System.exit(1);
        }
        
        String temp = scan.next();
        // verify P2 file...
        if(!temp.equals("P2")){
            System.out.println("Image Value: " + temp);
            System.out.println("NOT A VALID PGM FILE... ONLY P2 FILES SUPPORTED HERE...");
            System.exit(1);
        }
        
        // remove comments...
        while(!scan.hasNextInt()){
            scan.nextLine();
        }
        int cols = scan.nextInt(); int rows = scan.nextInt(); int maxVal = scan.nextInt();
        image = new short[rows][cols];
        for(int i =0;i < rows;++i){
            for(int j =0; j < cols;++j){
                image[i][j] = scan.nextShort();
            }
        }
        this.setSize(cols,rows);
        thePanel = new showImagePanel(image);
        this.getContentPane().add(thePanel);
        this.pack();
        this.setVisible(true);
    }
        
    private class showImagePanel extends JPanel{

    private short[][] image; private int PIXEL_RANGE;
    private Point P1 = null, P2 = null;

    public showImagePanel(short[][] image) {
        this.image = image.clone();
        int max = 0;
        for(int i=0;i<image.length;++i){
            for(int j=0;j<image[0].length;++j){
                if(image[i][j] > max){
                    max=image[i][j];
                }
            }
        }
        PIXEL_RANGE = max;
        setPreferredSize (new Dimension(image[0].length, image.length));
        showImageListener sIL = new showImageListener();
        addMouseListener(sIL);
        
    }
    
    public void paintComponent(Graphics page){
        super.paintComponent(page);
        for(int i=0;i<image.length;++i){
            for(int j=0;j<image[0].length;++j){
                if(image[i][j] < 0){
                    System.out.println("Value out of range at row " + i + " column " + j + ": " + image[i][j] + " changing to 0...");
                    image[i][j] = 0;
                }
                if(image[i][j] > PIXEL_RANGE){
                    System.out.println("Value out of range at row " + i + " column " + j + ": " + image[i][j] + " changing to 255...");
                    image[i][j] = 255;
                }
                int colorVal = (int)(((double)image[i][j]/PIXEL_RANGE)*255.0);
                page.setColor(new Color(colorVal, colorVal, colorVal));
                page.drawLine(j,i,j,i);
            }
        }
    }
    
    private void displayValues(int x1, int y1, int x2, int y2){
        // Displays the array values of theselected area of 
        // the image to the console
        if(x1 < 0) x1 = 0;
        if(x2 < 0) x2 = 0;
        if(y1 < 0) y1 = 0;
        if(y2 < 0) y2 = 0;
        if(x1 > image[0].length) x1 = image[0].length;
        if(x2 > image[0].length) x2 = image[0].length;
        if(y1 > image.length) y1 = image.length;
        if(y2 > image.length) y2 = image.length;
                
        for(int i = Math.min(y1,y2);i<Math.max(y1,y2);++i){
         for(int j=Math.min(x1,x2); j < Math.max(x1,x2);++j){
             System.out.print(image[i][j]+ "\t");
         }
         System.out.println();
        }   
    }
    
    
private class showImageListener implements MouseListener{
   
    public void mousePressed (MouseEvent event) {
        P1 = event.getPoint();
    }
    
    public void mouseReleased (MouseEvent event) {
        P2 = event.getPoint();
        displayValues((int)P1.getX(), (int)P1.getY(), (int)P2.getX(), (int)P2.getY());
    }
    
    public void mouseClicked (MouseEvent event) {}
    public void mouseEntered (MouseEvent event) {}
    public void mouseExited (MouseEvent event) {}
    
}
    
}
}
