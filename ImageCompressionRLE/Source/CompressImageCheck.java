// Rahul Shrestha
// Spring 2014

import java.util.*;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.File;
import java.io.FileWriter;
import java.io.FileNotFoundException;


public  class CompressImageCheck {
	
	// compress image method
	public static short[] compress(short[][] image,int imageMaximumValue ){
		
		
		// get image dimensions
		int imageLength = image.length;	  // row length
		int imageWidth = image[0].length; // column length
		
		//print image pixels
		System.out.println("Image Resolution > " +  image.length +" X " + image[0].length);
		
		
		/*
		 *  For Horizontal Redundancy
		*/
		
		// array List to store for horizontal redundancy
		ArrayList<storeHorizontal> compressedImageHorizontalList  = new ArrayList<storeHorizontal>();
		
		
		short oldValueHorizontal;
		short counterHorizontal;

		// for each to access array of arrays
		for (short[] iterateHorizontal : image) { 
			oldValueHorizontal = iterateHorizontal[0];
			counterHorizontal = 0;
			
			for (int i = 0; i < iterateHorizontal.length; i++) {
				
				if ((i+1) == iterateHorizontal.length) {  
					
					if (iterateHorizontal[i] == oldValueHorizontal) {
						counterHorizontal++;
						compressedImageHorizontalList.add(new storeHorizontal(counterHorizontal, oldValueHorizontal));
					}
					else
					{
						compressedImageHorizontalList.add(new storeHorizontal(counterHorizontal, oldValueHorizontal));
						compressedImageHorizontalList.add(new storeHorizontal((short)1, iterateHorizontal[i]));						
					}
				}

				else if (iterateHorizontal[i] == oldValueHorizontal) {
					counterHorizontal++;
				}
				else
				{
					compressedImageHorizontalList.add(new storeHorizontal(counterHorizontal, oldValueHorizontal));
					counterHorizontal = 1 ;
					oldValueHorizontal = iterateHorizontal[i];
				}
			}
		}

		
		// Array to store Horizontal compression values
		short[] compressedImageHorizontal = new short[(compressedImageHorizontalList.size()*2)+4];
		
		// by default values to be stored
		compressedImageHorizontal[0] = (short) imageWidth; // width, total columns
		compressedImageHorizontal[1] = (short)  imageLength; // length, total rows	
		compressedImageHorizontal[2] = (short) imageMaximumValue  ; // store maximum values
	    compressedImageHorizontal[3] = 1; // signature for HORIZONTAL COMPRESSION 
		
		int count =  4; // 0, 1, 2, 3 already occupied
	    
		// assign values from arrayList to 1D array
	    for (storeHorizontal accessStoreHorizontal : compressedImageHorizontalList) {
	    	compressedImageHorizontal[count] = (short) accessStoreHorizontal.getCounterHorizontal();
	        count++;
	        compressedImageHorizontal[count] = (short) accessStoreHorizontal.getOldValueHorizontal();
	        count++;			
	    }
	      
	    // end of Horizontal Redundancy
	   		     
	    
	    /*
	     * For Vertical Redundancy   
	    */
		ArrayList<Short> compressedImageVList = new ArrayList<Short>();    
   
		// store values into arrayList from 2D array image
		for (int i=0;i<imageWidth; i++){
			for (int j=0;j<imageLength; j++){
				compressedImageVList.add(image[j][i]);
			}
		}
       
	       
		short[][] transposeImage = new short[imageWidth][imageLength];
		
		int increment = 0;
		// assign from arrayList to transposeImage array after +90 transpose
		for (int i = 0; i<imageWidth; i++){
			for(int j = 0; j<imageLength; j++){
				transposeImage[i][j]= compressedImageVList.get(increment);
				increment += 1;
			}
		}	
		
		// array List to store for vertical redundancy
		ArrayList<storeVertical> compressedImageVerticalList = new ArrayList<storeVertical>();
		short oldValueVertical;
	    short counterVertical;
		
	    // for each
		for (short[] iterateVertical: transposeImage) {
			oldValueVertical = iterateVertical[0];
			counterVertical = 0;
			
			for (int i = 0; i < iterateVertical.length; i++) {
				
				if ((i+1) == iterateVertical.length) {  
					
					if (iterateVertical[i] == oldValueVertical) {
						counterVertical++;
						compressedImageVerticalList.add(new storeVertical(counterVertical, oldValueVertical));
					}
					else
					{
						compressedImageVerticalList.add(new storeVertical(counterVertical, oldValueVertical));
						compressedImageVerticalList.add(new storeVertical((short)1, iterateVertical[i]));						
					}
				}

				else if (iterateVertical[i] == oldValueVertical) {
					counterVertical++;
				}
				else
				{
					compressedImageVerticalList.add(new storeVertical(counterVertical, oldValueVertical));
					counterVertical = 1 ;
					oldValueVertical= iterateVertical[i];
				}
			}
		}

		
		// Array to store Vertical compression values
		short[] compressedImageVertical= new short[compressedImageVerticalList.size()*2+4];
		
		// by default values to be stored
		compressedImageVertical[0] = (short) imageWidth; // width, total columns
		compressedImageVertical[1] = (short) imageLength; // length, total rows	
		compressedImageVertical[2] = (short) imageMaximumValue  ; // store maximum values
	    compressedImageVertical[3] = 2; // signature for VERTICAL COMPRESSION 
		
		int countVertical =  4; // 0, 1, 2, 3 already occupied
	      
	    for (storeVertical accessStoreVertical: compressedImageVerticalList) {
	    	compressedImageVertical[countVertical] = (short) accessStoreVertical.getCounterVertical();
	    	countVertical++;
	        compressedImageVertical[countVertical] = (short) accessStoreVertical.getOldValueVertical();
	        countVertical++;			
	    }
	      
	    // end of Vertical Redundancy
	    
	    System.out.println(" ");
	    // Return statements based on conditions
	    if ( compressedImageHorizontal.length < compressedImageVertical.length ){  
	    	System.out.println("Horizontal Compression was done!");
	    	return compressedImageHorizontal ; // return horizontal compression
	    
	    }
	    else{
	    	System.out.println("Vertical compression was done!");
	    	return compressedImageVertical ; // return Vertical compressedImage
	    }   
	
	
	
	}// end of compress method
	
	
	
	
	
	// Write Method
	public static void write(short[] compressedImage,String imageTextFileName){
	
		BufferedWriter out;
	    // if file not found ( I/O exception )  
			try {
				out = new BufferedWriter(new FileWriter(imageTextFileName+".compressed"));
				System.out.println("File created and Arrays being stored........");
				for (short values : compressedImage) {
		           out.write(values + " ");	
		        }
		     
		        out.close( );
		    } 
		    catch (IOException e) {
		    	System.out.println("File not found. Check file. " + e.getMessage());
		    }
		
	} // end of write method
	
	
	
	
	
	
	// Read Method
	@SuppressWarnings("resource")
	public static short[] read(String fileName){
			
		Scanner myScan = null;
		try{
	         myScan = new Scanner(new File(fileName));
	      }
		catch(FileNotFoundException e){
			System.out.println("The File was not found: " + fileName);
			System.exit(1);
		}
		        
		ArrayList<Short> pixelValues = new ArrayList<Short>();
		   	
	    while (myScan.hasNextInt()) {
	        pixelValues.add(myScan.nextShort());			
	    }
		   	
	    // store arrays values from .compressed file
	    short[] restoredImage = new short[pixelValues.size()];
		   	
	    for (int i = 0; i < restoredImage.length; i++) {
	   	  restoredImage[i]  = pixelValues.get(i);
	    }
		 
	    
	    return restoredImage; // return image
		  
	
	} // end of read method
	
	
	
	
	
	
	// decompress method
	public static short[][] decompress(short image[]){
		
		int arrayLength = image[1]; // row length
		int arrayWidth = image[0];	// column length	
		
		// array to store and return decompressed image
		short[][] decompressedImage = new short[arrayLength][arrayWidth]; 
		
		int rowCounter = 0;
		int colCounter = 0;
		
		short counter; // Repetition
		short value; // Compressed values
		
		/* 
		 *  Make sure that the method checks for vertical and horizontal compression
		 *  counter stores total repetition at even places (4+2n)
		 *  value stores the repeated values at odd places (5+2n)
		 */
		
		if (image [3] == 1){ // 1 is the signature for horizontal compression
			
			for (int i = 4; i < image.length; i++) { // image stores values from 4 and until image length
				counter = image[i]; // even 
				value = image[++i]; // odd
			
				for (int j = 0; j < counter; j++) { // loop until counter
				
					decompressedImage[rowCounter][colCounter] = value; // assign horizontally
								
					colCounter++; // increment column
				
					if (colCounter == arrayWidth) {
					
						if(rowCounter != arrayLength) {
							colCounter = 0;
							rowCounter++; // increment row
						}
						
					}
				
				}
			
			}// for loop
			
			return decompressedImage;  
		
		} // end if
		
		
		else{ // if not horizontal compression then vertical
			
			for (int i = 4; i < image.length; i++) { // image stores values from 4 and until image length
				counter = image[i]; // even
				value = image[++i]; // odd
			
				for (int j = 0; j < counter; j++) { // assign vertically
				
					decompressedImage[colCounter][rowCounter] = value;  // assign vertically
								
					colCounter++; // increment row
				
					if (colCounter == arrayLength) {
					
						if(rowCounter != arrayWidth) {
							colCounter = 0;
							rowCounter++; // increment column
						}
						
					}
				
				}
			
			} // for loop
			
			return decompressedImage; // return image  
		} // end else
		
		
	
	} // end of decompress method





}     // end of  CompressImage class        