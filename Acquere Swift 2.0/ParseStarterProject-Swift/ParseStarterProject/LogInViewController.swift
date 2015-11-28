//
//  LogInViewController.swift
//  ParseStarterProject-Swift
//
//  Created by Rahul Shrestha on 9/12/15.
//  Copyright (c) 2015 Parse. All rights reserved.
//

import UIKit
import ParseFacebookUtilsV4

class LogInViewController: UIViewController {

    @IBOutlet weak var signUpThroughLoginView: UIButton!
    @IBOutlet weak var emailLogInButton: UIButton!
    @IBOutlet weak var linkedInLogInButton: UIButton!
   @IBOutlet weak var facebookLogInButton: UIButton!
    @IBOutlet weak var twitterLogInButton: UIButton!
    override func viewDidLoad() {
        super.viewDidLoad()
        
       // self.navigationController!.navigationBar.hidden=false
        
    
        decorateButton(facebookLogInButton,color: UIColor(red: 0.231, green: 0.349, blue: 0.596, alpha: 1.0) )
         decorateButton(twitterLogInButton, color: UIColor(red: 0.333, green: 0.675, blue: 0.933, alpha: 1))
         decorateButton(linkedInLogInButton, color: UIColor(red: 0, green: 0.467, blue: 0.71, alpha: 1))
        decorateButton(emailLogInButton, color: UIColor(red: 0.961, green: 0.231, blue: 0.231, alpha: 1))
        
        decorateButton(signUpThroughLoginView, color: UIColor.blackColor())
        
    }
    
    override func viewWillAppear(animated: Bool) {
       // self.navigationController!.navigationBar.hidden=false
    }

    
    private func decorateButton(button: UIButton, color: UIColor) {
        
        button.setTitleColor(color, forState: UIControlState.Normal)
        button.layer.borderColor = color.CGColor
        button.backgroundColor = UIColor.clearColor()
        button.layer.borderWidth = 2
        button.layer.cornerRadius = 7
    }
    
    @IBAction func facebookLogInTapped(sender: AnyObject) {
        
        PFUser.logOut()
        let permissions=["email","public_profile"]
        PFFacebookUtils.logInInBackgroundWithReadPermissions(permissions) {
            (user: PFUser?, error: NSError?) -> Void in
            if let user = user {
                if user.isNew {
                    let newUser = UIAlertController(title: "Congrats!", message: "You have been Signed up and Logged in through Facebook.", preferredStyle: UIAlertControllerStyle.Alert)
                    newUser.addAction(UIAlertAction(title: "OK", style: .Default, handler: nil))
                    self.presentViewController(newUser, animated: true, completion: nil);
                } else {
                    let logInSuccess = UIAlertController(title: "Success", message: "You have been Logged In successfully.", preferredStyle: UIAlertControllerStyle.Alert)
                    logInSuccess.addAction(UIAlertAction(title: "OK", style: .Default, handler: nil))
                    self.presentViewController(logInSuccess, animated: true, completion: nil);
                }
            } else {
                let logInCancel = UIAlertController(title: "Failed", message: "Uh oh. The user cancelled the Facebook Log In!", preferredStyle: UIAlertControllerStyle.Alert)
                logInCancel.addAction(UIAlertAction(title: "OK", style: .Default, handler: nil))
                self.presentViewController(logInCancel, animated: true, completion: nil);
            }
        }
    }
    


}
