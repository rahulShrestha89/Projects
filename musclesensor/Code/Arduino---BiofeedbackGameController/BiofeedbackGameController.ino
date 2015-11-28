#include "Arduino.h"
#include "MuscleSensor.h"

 /***********************************************
 Biofeedback Game Controller
 Sends game control keys to the host PC

	/* Keyboard report buffer */
uint8_t buf[8] = { 0 };
void setup();
void loop();
boolean upTriggered = false;
boolean downTriggered = false;

/***********************************************
Keyboard HID values
************************************************/ 
//const int X = 27;       
const int Z = 29;     
//const int ENTER = 40;   
const int RIGHT = 79;  
//const int LEFT = 80;    
//const int DOWN = 81;  
const int UP = 82;    
//const int CTRL = 224;  

/***********************************************
Define Muscle Sensors 
args:
- pin = analog pin number
- threshold value = min. ADC value to indicate
  the sensor has been triggered
************************************************/ 
MuscleSensor rBicep(2, 300);	//Right Bicep 
MuscleSensor lBicep(3, 300);	//Left Bicep 

void setup()
{
    Serial.begin(9600);
    delay(200);
}

void loop()
{
   /**********************************************
   Read Sensor Values	
   ***********************************************/ 
  rBicep.update();
  lBicep.update();
  
   /**********************************************
   Check for Combo Buttons	
   ***********************************************/ 
   //note - delete this if you aren't mapping multiple muscles to a single button
   		
  if(	rBicep.isTriggered() && lBicep.isTriggered())
		downTriggered = true;
  else
		downTriggered = false;
	
  /***********************************************
  Function Buttons	
  ************************************************/    
  if(!downTriggered)
  {
	 
	  if(lBicep.isTriggered() &&!rBicep.isTriggered())	
                {buf[4] = UP;}
	  else 				
                {buf[4] = 0;}
  }
  else
  {	   
          if(downTriggered && !upTriggered)	
                {buf[5] = Z;}
          else if(!downTriggered && upTriggered)
        	{buf[5] = 0;}
	  else 				
                {buf[5] = 0;}
  }
  
  /***********************************************
   Send to CPU	
   ***********************************************/  
  Serial.write(buf, 8);	// Send keypress
  delay(100);
}
