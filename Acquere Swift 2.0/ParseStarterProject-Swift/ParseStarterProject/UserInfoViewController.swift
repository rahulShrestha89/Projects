//
//  UserInfoViewController.swift
//  ParseStarterProject-Swift
//
//  Created by Rahul Shrestha on 11/14/15.
//  Copyright © 2015 Parse. All rights reserved.
//

import UIKit
import Parse

class UserInfoViewController: UIViewController,UINavigationControllerDelegate,UIImagePickerControllerDelegate {

    @IBOutlet weak var showRealName: UILabel!
    @IBOutlet weak var numberOfFollowers: UILabel!
    @IBOutlet weak var followingNumber: UILabel!
    @IBOutlet weak var userProfileImage: UIImageView!
    @IBOutlet weak var userEmailAddress: UILabel!
    
    @IBOutlet weak var showUsername: UIButton!
    var currentUser = PFUser.currentUser()
   
    override func viewDidLoad() {
        super.viewDidLoad()

        decorateLabel(showRealName, color: UIColor(red: 0.506, green: 0.831, blue: 0.98, alpha: 1))
        userEmailAddress.text = currentUser!["email"] as! String
        
        showRealName.text = " "+(currentUser!["firstName"] as? String)! + " " +
            (currentUser!["lastName"] as? String)!+"  "
    
        if(currentUser!["hasHashTagUsername"] as! Bool  == false){
            showUsername.setTitle("Add @UserName..", forState: .Normal)
        } else{ // TODO : provide the username here
            decorateButton(showUsername, color: UIColor(red: 0.957, green: 0.263, blue: 0.212, alpha: 1))
            var finalUsername = "@" + (currentUser!["hashtagUsername"] as! String)
            showUsername.setTitle(finalUsername, forState: .Normal)
        }
    }

    override func viewWillAppear(animated: Bool) {
        var finalUsername = "@" + (currentUser!["hashtagUsername"] as! String)
        showUsername.setTitle(finalUsername, forState: .Normal)    }
    
    @IBAction func addUsername(sender: AnyObject) {
        
        // button action when there is no username
        if(currentUser!["hasHashTagUsername"] as! Bool  == false){
            
            currentUser!["hasHashTagUsername"]  = true
            currentUser?.saveInBackground()
            
            var alert = UIAlertController(title: "Add a unique username!!!", message: "", preferredStyle: UIAlertControllerStyle.Alert)
            
            alert.addTextFieldWithConfigurationHandler(configurationTextField)
            alert.addAction(UIAlertAction(title: "Cancel", style: UIAlertActionStyle.Cancel,handler: { (action: UIAlertAction!) in
                print("Handle Cancel Logic here")
            }))
            alert.addAction(UIAlertAction(title: "Done", style: UIAlertActionStyle.Default, handler:{ (UIAlertAction)in
                print("Done !!")
                self.currentUser!["hashtagUsername"] =  (self.tField.text)
                self.currentUser?.saveInBackground()
                print("Item : \(self.tField.text)")
                self.viewWillAppear(true)
            }))
            self.presentViewController(alert, animated: true, completion: {
                print("completion block")
            })
            
        } else{ // button action when the user name already exists
            print("Alreday have a user name!")
        }
    }
    
    var tField: UITextField!
    
    func configurationTextField(textField: UITextField!)
    {
        print("generating the TextField")
        textField.placeholder = "Enter Username"
        tField = textField
    }
    
    @IBAction func goBack(sender: AnyObject) {
        self.performSegueWithIdentifier("backToTabbedViewFromUserInfo", sender: self)
    }

    func imagePickerController(picker: UIImagePickerController, didFinishPickingImage image: UIImage, editingInfo: [String : AnyObject]?) {
        self.dismissViewControllerAnimated(true, completion: nil)
        userProfileImage.image = image
        
        let imageData = UIImagePNGRepresentation(userProfileImage.image!)
        
        var imageGUID = NSUUID().UUIDString + (PFUser.currentUser()?.objectId)! as String + (PFUser.currentUser()?.username)! as String + ".jpg"
        
        let imageFile = PFFile(name: imageGUID, data: imageData!)
        
        currentUser!["profileImage"] = imageFile

        currentUser?.saveInBackgroundWithBlock{(success,error) -> Void in
            
            if error==nil{
                print("Image saved successfully as " + imageGUID)
            } else {
                print("Problem with Image file size!!!!")
            }
        }
    }
 
    @IBAction func changeImage(sender: AnyObject) {
        
        var image = UIImagePickerController()
        image.delegate = self
        image.sourceType = UIImagePickerControllerSourceType.PhotoLibrary
        image.allowsEditing = false
        self.presentViewController(image, animated: true, completion: nil)
    }
    
    private func decorateLabel(label: UILabel, color: UIColor) {
        
        label.layer.borderColor = color.CGColor
        label.backgroundColor = color
        label.layer.borderWidth = 2
        label.layer.cornerRadius = 7
    }
    
    private func decorateButton(button: UIButton, color: UIColor) {
        
        button.setTitleColor(UIColor.whiteColor(), forState: UIControlState.Normal)
        button.layer.borderColor = color.CGColor
        button.backgroundColor = color
        button.layer.borderWidth = 2
        button.layer.cornerRadius = 7
    }
    

}
