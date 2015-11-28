//
//  EmailSignUpViewController.swift
//  ParseStarterProject-Swift
//
//  Created by Rahul Shrestha on 9/18/15.
//  Copyright (c) 2015 Parse. All rights reserved.
//

import UIKit
//import SwiftValidator
import Parse
import DTIActivityIndicator


class EmailSignUpViewController: UIViewController{
    @IBOutlet weak var signUpButton: UIButton!

    @IBOutlet weak var firstNameTextField: UITextField!
    @IBOutlet weak var lastNameTextField: UITextField!
    @IBOutlet weak var emailAddressTextField: UITextField!
    @IBOutlet weak var passwordTextField: UITextField!
    @IBOutlet weak var confirmPasswordTextField: UITextField!
    
    @IBOutlet weak var signUpActivityIndicator: UIActivityIndicatorView!
    //let validator = Validator()
    override func viewDidLoad() {
        super.viewDidLoad()
       
        decorateButton(signUpButton, color: UIColor(red: 0.114, green: 0.914, blue: 0.714, alpha: 1))
        signUpActivityIndicator.hidden=true
        
        
    }
    
    private func decorateButton(button: UIButton, color: UIColor) {
        
        button.setTitleColor(UIColor.whiteColor(), forState: UIControlState.Normal)
        button.layer.borderColor = color.CGColor
        button.backgroundColor = color
        button.layer.borderWidth = 2
        button.layer.cornerRadius = 7
    }
    
    @IBAction func signUpButtonTapped(sender: AnyObject) {
        
        let passwordString:String = passwordTextField.text!
        let passwordCount = passwordString.characters.count
        
        if firstNameTextField.text=="" || lastNameTextField.text=="" || emailAddressTextField.text=="" || passwordTextField.text=="" || confirmPasswordTextField.text==""{
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
            let passwordLengthErrorAlert = UIAlertController(title: "Weak Password", message: "Password must be greater than 5 characters. Try Again?", preferredStyle: UIAlertControllerStyle.Alert)
            passwordLengthErrorAlert.addAction(UIAlertAction(title: "OK", style: .Default, handler: nil))
            self.presentViewController(passwordLengthErrorAlert, animated: true, completion: nil);
        }
        else if passwordTextField.text != confirmPasswordTextField.text{
            let passwordErrorAlert = UIAlertController(title: "Incorrect Password Combination", message: "Passwords do not match. Try Again?", preferredStyle: UIAlertControllerStyle.Alert)
            passwordErrorAlert.addAction(UIAlertAction(title: "OK", style: .Default, handler: nil))
            self.presentViewController(passwordErrorAlert, animated: true, completion: nil);
        }
        else{
            let bounds = UIScreen.mainScreen().bounds
            let myActivityIndicatorView: DTIActivityIndicatorView = DTIActivityIndicatorView(frame: CGRect(x:bounds.size.width/2-60, y:bounds.size.height/2-10, width:120.0, height:120.0))
            self.view.addSubview(myActivityIndicatorView)
            myActivityIndicatorView.indicatorColor = UIColor(red: 0.114, green: 0.914, blue: 0.714, alpha: 1)
            
            myActivityIndicatorView.indicatorStyle = DTIIndicatorStyle.convInv(.chasingDots)
            myActivityIndicatorView.startActivity()

            UIApplication.sharedApplication().beginIgnoringInteractionEvents()
            view?.backgroundColor = UIColor(white: 1, alpha: 0.8)
            var user = PFUser()
            user.email = emailAddressTextField.text
            user.password = passwordTextField.text
            user.username = emailAddressTextField.text
            user["firstName"] = firstNameTextField.text
            user["lastName"] = lastNameTextField.text
            user["firstTimeLoggingIn"] = true
            user["hasHashTagUsername"] = false
            user.signUpInBackgroundWithBlock({ (success: Bool, error: NSError?) -> Void in
                if error != nil{
                    let error = UIAlertController(title: "Multiple Account", message: "Account Alreday Registered with this Email.", preferredStyle: UIAlertControllerStyle.Alert)
                    error.addAction(UIAlertAction(title: "OK", style: .Default, handler: { (action: UIAlertAction!) -> () in
                        self.performSegueWithIdentifier("signUpByEmailToMainSignUp", sender: nil)
                    }))
                    self.presentViewController(error, animated: true, completion: nil);
                    self.view?.backgroundColor = UIColor(red: 0.957, green: 0.263, blue: 0.212, alpha: 1)
                    UIApplication.sharedApplication().endIgnoringInteractionEvents()
                    myActivityIndicatorView.stopActivity()
                }
                else{
                    let activitySeconds = 5.0
                    let activityDelay = activitySeconds * Double(NSEC_PER_SEC)  // nanoseconds per seconds
                    let activityDispatchTime = dispatch_time(DISPATCH_TIME_NOW, Int64(activityDelay))
                    // present this after //seconds//
                    dispatch_after(activityDispatchTime, dispatch_get_main_queue(), {
                        myActivityIndicatorView.stopActivity()
                        UIApplication.sharedApplication().endIgnoringInteractionEvents()
                        self.view?.backgroundColor = UIColor(red: 0.302, green: 0.714, blue: 0.675, alpha: 1)
                        let success = UIAlertController(title: "Congratulations!", message: "Please Check your email for account verification.", preferredStyle: UIAlertControllerStyle.Alert)
                        success.addAction(UIAlertAction(title: "OK", style: .Default, handler: { (action: UIAlertAction!) -> () in
                            self.performSegueWithIdentifier("signUpByEmailToLogIn", sender: nil)
                            PFUser.logOut()
                        }))
                        self.presentViewController(success, animated: true, completion: nil);
                        
                    })
                    
                  
                }
            })
            
        }
        
    }
    
    func isValidEmail(testStr:String) -> Bool {
        
        let emailRegEx = "[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,4}"
        let range = testStr.rangeOfString(emailRegEx, options:.RegularExpressionSearch)
        let result = range != nil ? true : false
        return result
        
    }
    
    
}
