//
//  EmailLogInViewController.swift
//  ParseStarterProject-Swift
//
//  Created by Rahul Shrestha on 9/24/15.
//  Copyright (c) 2015 Parse. All rights reserved.
//

import UIKit
import Parse
import DTIActivityIndicator

class EmailLogInViewController: UIViewController {

    @IBOutlet weak var emailAddressTextField: UITextField!
    @IBOutlet weak var logInButton: UIButton!
    @IBOutlet weak var passwordTextField: UITextField!
    @IBOutlet weak var forgotPassword: UIButton!
    override func viewDidLoad() {
        super.viewDidLoad()

        decorateButton(logInButton, color: UIColor(red: 0.259, green: 0.647, blue: 0.961, alpha: 1))
        
        
    }

    private func decorateButton(button: UIButton, color: UIColor) {
        
        button.setTitleColor(UIColor.whiteColor(), forState: UIControlState.Normal)
        button.layer.borderColor = color.CGColor
        button.backgroundColor = color
        button.layer.borderWidth = 2
        button.layer.cornerRadius = 7
    }

    @IBAction func forgotPasswordButtonTapped(sender: AnyObject) {
        if self.emailAddressTextField.text=="" {
            let blankFieldAlert = UIAlertController(title: "Empty Form Field Error", message: "Enter an email address to reset Password.", preferredStyle: UIAlertControllerStyle.Alert)
            blankFieldAlert.addAction(UIAlertAction(title: "OK", style: .Default, handler: nil))
            self.presentViewController(blankFieldAlert, animated: true, completion: nil);
        }else {
            PFUser.requestPasswordResetForEmailInBackground(self.emailAddressTextField.text!)
            let passwordChangeAlert = UIAlertController(title: "Email Sent", message: "Check your email for further instruction.", preferredStyle: UIAlertControllerStyle.Alert)
            passwordChangeAlert.addAction(UIAlertAction(title: "OK", style: .Default, handler: nil))
            self.presentViewController(passwordChangeAlert, animated: true, completion: nil);
        }
    }
    @IBAction func logInButtonTapped(sender: AnyObject) {
        
        let passwordString:String = passwordTextField.text!
        let passwordCount = passwordString.characters.count
   
        
        if self.emailAddressTextField.text=="" || self.passwordTextField.text==""  {
            let blankFieldAlert = UIAlertController(title: "Empty Form Field Error", message: "All the Entries are required.", preferredStyle: UIAlertControllerStyle.Alert)
            blankFieldAlert.addAction(UIAlertAction(title: "OK", style: .Default, handler: nil))
            self.presentViewController(blankFieldAlert, animated: true, completion: nil);
        }
        else if !isValidEmail(emailAddressTextField.text!){
            let emailErrorAlert = UIAlertController(title: "Incorrect Email Format", message: "Please Enter a Valid Email Address.", preferredStyle: UIAlertControllerStyle.Alert)
            emailErrorAlert.addAction(UIAlertAction(title: "OK", style: .Default, handler: nil))
            self.presentViewController(emailErrorAlert, animated: true, completion: nil);
        }
        else if passwordCount < 6 {
            let passwordLengthErrorAlert = UIAlertController(title: "Short Password", message: "Your Password was more than 5 characters.", preferredStyle: UIAlertControllerStyle.Alert)
            passwordLengthErrorAlert.addAction(UIAlertAction(title: "Gotcha!", style: .Default, handler: nil))
            self.presentViewController(passwordLengthErrorAlert, animated: true, completion: nil);
        }
        else {
            PFUser.logInWithUsernameInBackground(emailAddressTextField.text!, password:passwordTextField.text!) {
                (user: PFUser?, error: NSError?) -> Void in
               
                if error != nil{
                    let combo = UIAlertController(title: "Combo", message: "Email and Pass Combo problem.", preferredStyle: UIAlertControllerStyle.Alert)
                    combo.addAction(UIAlertAction(title: "Gotcha!", style: .Default, handler: nil))
                    self.presentViewController(combo, animated: true, completion: nil);
                }
                 else if user!["emailVerified"] as! Bool == false {
                    let loginFail = UIAlertController(title: "LogIn Fail", message: "Something wrong with email verification.", preferredStyle: UIAlertControllerStyle.Alert)
                    loginFail.addAction(UIAlertAction(title: "Gotcha!", style: .Default, handler: nil))
                    self.presentViewController(loginFail, animated: true, completion: nil);
                }else{
                    
                    let bounds = UIScreen.mainScreen().bounds
                    let myActivityIndicatorView: DTIActivityIndicatorView = DTIActivityIndicatorView(frame: CGRect(x:bounds.size.width/2-60, y:bounds.size.height/2-128, width:120.0, height:120.0))
                    self.view.addSubview(myActivityIndicatorView)
                    myActivityIndicatorView.indicatorColor = UIColor(red: 0.259, green: 0.647, blue: 0.961, alpha: 1)
                    
                    myActivityIndicatorView.indicatorStyle = DTIIndicatorStyle.convInv(.chasingDots)
                    myActivityIndicatorView.startActivity()
                    
                    self.view?.backgroundColor = UIColor(white: 1, alpha: 0.8)
                    UIApplication.sharedApplication().beginIgnoringInteractionEvents()
                    
                    
                    let activitySeconds = 5.0
                    let activityDelay = activitySeconds * Double(NSEC_PER_SEC)  // nanoseconds per seconds
                    let activityDispatchTime = dispatch_time(DISPATCH_TIME_NOW, Int64(activityDelay))
                    // present this after //seconds//
                    dispatch_after(activityDispatchTime, dispatch_get_main_queue(), {
                        myActivityIndicatorView.stopActivity()
                        UIApplication.sharedApplication().endIgnoringInteractionEvents()
                        self.view?.backgroundColor = UIColor(red: 0, green: 0.749, blue: 0.647, alpha: 1)
                        
                        var currentUser = PFUser.currentUser()
                        
                        if currentUser != nil { 
                            // if the user is login for the first time
                            if currentUser?["firstTimeLoggingIn"] as! Bool == true{
                                // go to post sign up for the first login
                                currentUser?["firstTimeLoggingIn"]=false
                                currentUser?.saveInBackground()
                                self.performSegueWithIdentifier("firstLoginToSignUp", sender: nil)
                            // if previosuly logged in
                            } else{
                                
                                self.performSegueWithIdentifier("afterVerified", sender: nil)
                                
//                                // TEMPORARY: Show alert view controller
//                                let loginSuccess = UIAlertController(title: "Successful LogIn", message: "All good!!", preferredStyle: UIAlertControllerStyle.Alert)
//                                
//                                loginSuccess.addAction(UIAlertAction(title: "OK", style: .Default, handler: { (action: UIAlertAction!) -> () in
//                                                        }))
//                                
//                                self.presentViewController(loginSuccess, animated: true, completion: nil);
                            }
                        } else {
                            print("No user found")
                        }
                        
                    })
                    
                }
            }
        }
       
    }
    
    func isValidEmail(testStr:String) -> Bool {
        
        let emailRegEx = "[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,4}"
        let range = testStr.rangeOfString(emailRegEx, options:.RegularExpressionSearch)
        let result = range != nil ? true : false
        return result
        
    }
}
