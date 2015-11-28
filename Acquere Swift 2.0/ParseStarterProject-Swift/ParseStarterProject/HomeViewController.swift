/**
* Copyright (c) 2015-present, Parse, LLC.
* All rights reserved.
*
* This source code is licensed under the BSD-style license found in the
* LICENSE file in the root directory of this source tree. An additional grant
* of patent rights can be found in the PATENTS file in the same directory.
*/

import UIKit
import Parse
import MediaPlayer

class ViewController: UIViewController {

    @IBOutlet weak var signUpButton: UIButton!
    @IBOutlet weak var logInButton: UIButton!
    @IBOutlet weak var lookAroundButton: UIButton!
   
    var backGroundPlayer: MPMoviePlayerController!
    
    override func viewDidLoad() {
        
        super.viewDidLoad()
        //self.navigationController!.navigationBar.hidden=true
    
        
        // Load the video from the app bundle.
        let videoURL: NSURL = NSBundle.mainBundle().URLForResource("video2", withExtension: "mp4")!
        
        // Create and configure the movie player.
        self.backGroundPlayer = MPMoviePlayerController(contentURL: videoURL)
        
        self.backGroundPlayer.controlStyle = MPMovieControlStyle.None
        self.backGroundPlayer.scalingMode = MPMovieScalingMode.AspectFill
        
        self.backGroundPlayer.view.frame = self.view.frame
        self.view .insertSubview(self.backGroundPlayer.view, atIndex: 0)
        
        self.backGroundPlayer.play()
        
        // Loop video.
        NSNotificationCenter.defaultCenter().addObserver(self, selector: "loopVideo", name: MPMoviePlayerPlaybackDidFinishNotification, object: self.backGroundPlayer)
        
        // UI changes to Sign Up Button
        signUpButton.layer.cornerRadius = 12
        signUpButton.backgroundColor = UIColor(red: 1, green: 0.541, blue: 0.396, alpha: 1.0)
        
        // working out the horizontal and vertical lines
        let bounds = UIScreen.mainScreen().bounds
        let width = bounds.size.width
        let height = bounds.size.height
        
        let horizontalLine : CGRect = CGRectMake(0, height-45, width, 1)
        let verticalLine : CGRect = CGRectMake(width/2, height-45, 1, height)
        
        let horizontalLineView = UIView(frame: horizontalLine)
        let verticalLineView = UIView(frame: verticalLine)
        
        horizontalLineView.backgroundColor = UIColor.whiteColor()
        verticalLineView.backgroundColor = UIColor.whiteColor()
        
        self.view.addSubview(horizontalLineView)
        self.view.addSubview(verticalLineView)
      
    }
    
    func loopVideo() {
        self.backGroundPlayer.play()
    }
    
    override func viewWillAppear(animated: Bool) {
        //self.navigationController!.navigationBar.hidden=true
    }
    
    

   
}
