//////////////////////////////////////////////////////////////////////////////
//
//                                {PGM IMAGE COMPRESSOR}
//
// 
// Rahul Shrestha
// Spring 2014
// Southeastern Louisiana University
//
//   <> Based upon RLE (Run Length Encoding Concept). Applies most
//		Opportune and low size; either horizontal or vertical	
//		Compression. 
//
//	 <> Use GIMP to convert images to .pgm files
//
///////////////////////////////////////////////////////////////////////////////


import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.util.Scanner;
import java.io.FileInputStream;
import java.util.InputMismatchException;

@SuppressWarnings("unused")
public class DriverCheck{

	
	@SuppressWarnings("static-access")
	public static void main(String[] args) {
		
		@SuppressWarnings("resource")
		Scanner myScan = new Scanner(System.in);
		
		
		// print info
		System.out.println("Based upon Run Length Encoding concept, this program does compression and decompression stuff.");
		System.out.println("Enter the name of image you want to compress:> ");
		
		// get image name
		String imageName = myScan.next();
		
		// store  file constants and remove strings
		String filePath = imageName;
		FileInputStream fileInputStream = null;
		try {
			fileInputStream = new FileInputStream(filePath);
			System.out.println("Image found");
			}
		catch (FileNotFoundException e) {
			System.out.println("File not found");
			e.printStackTrace();
		}
				
		@SuppressWarnings("resource")
		Scanner scan = new Scanner(fileInputStream);
				
		scan.nextLine(); // leave comment
		scan.nextLine(); // leave comment tag
		int originalImageWidth = scan.nextInt(); // stores width from pgm file
		int originalImageLength = scan.nextInt(); // stores length from pgm file
		int imageMaximumValue = scan.nextInt(); // stores maximum value from pgm file
		
		// instantiate getPGM class
		getPGM getpgm = new getPGM(imageName);	
		short[][] image = getpgm.getImage();
	
   		// instantiate CompressImageCheck class
		CompressImageCheck compressObject= new CompressImageCheck();
      
		// array to store compressed image
		short [] compressedImage = compressObject.compress(image,imageMaximumValue);
      
      	// ask user to provide compressed Image filename
   		System.out.println("Provide Filename to store compressed Image (No extension required; If possible name it same as that of the image name):> ");
   		String fileName = myScan.next();
   		
   	 		
   		// write 1d arrays to compressImage.txt
   		compressObject.write(compressedImage, fileName);
   		System.out.println(" ");
   		
   		// read compressImage.txt.compressed and store into compressedImageRead
   		short[] compressedImageRead = compressObject.read(fileName+ ".compressed");
   		
   		// decompress after reading .compressed file and store it in a 2D array
   		short[][] decompressedImage = compressObject.decompress(compressedImageRead);
   		
   		// print the real image
   		new showGrayImage(getpgm.getImage(), "Before COMPRESSION");
   		
   		// show image after decompression
   		new showGrayImage(decompressedImage,"After Decompression!");
   		
   		System.out.println("  "); 
   		System.out.println("loading....."); 
   		System.out.println("loading........"); 
		System.out.println("loading.............");
		System.out.println("  "); 
		System.out.println("Decompression Task Completed!!!");
		System.out.println("  "); 
		System.out.println("Warning: Not for COMMERCIAL USE!!!");

   }
}