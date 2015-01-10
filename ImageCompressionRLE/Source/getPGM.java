
//////////////////////////////////////////////////////////////////////////////
//
//                                PGM READER

//
//
// <> Reads in a PGM File (P2) and stores it in an 2D array of shorts.
// <> Methods: getImage(), getRows(), getCols(), and getMaxValue.
// <> Constructor is passed a String, that is a file name
//    of a (P2) PGM picture file which will be imported.
//    PGM FILE FORMAT:
//          _____________________________
//          |P2                         |
//          |# Comments...              |
//          |COLUMNS ROWS               |
//          |MAX_VALUE                  |
//          |VALUES_IN_ROW_ORDER        |
//          |... 
//////////////////////////////////////////////////////////////////////////////



import java.util.Scanner;
import java.io.File;
import java.io.FileNotFoundException;

public class getPGM {
    
    private short[][] image;
    private int cols, rows, maxVal;
    
    public getPGM(String filename) {

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
        this.rows = rows; this.cols = cols; this.maxVal = maxVal;
        image = new short[rows][cols];
        for(int i =0;i < rows;++i){
            for(int j =0; j < cols;++j){
                image[i][j] = scan.nextShort();
            }
        }
    }
    
    public short[][] getImage(){
        return image;
    }
    public int getRows(){
        return rows;
    }
    public int getCols(){
        return cols;
    }
    public int getMaxValue(){
        return maxVal;
    }
    public short getPixel(int i, int j){
        return image[i][j];
    } 
}
