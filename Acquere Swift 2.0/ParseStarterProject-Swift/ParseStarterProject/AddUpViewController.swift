//
//  AddUpViewController.swift
//  ParseStarterProject-Swift
//
//  Created by Rahul Shrestha on 11/15/15.
//  Copyright © 2015 Parse. All rights reserved.
//

import UIKit
import Parse
import DTIActivityIndicator

class AddUpViewController: UIViewController,UITextViewDelegate,UIPickerViewDataSource,UIPickerViewDelegate {

    @IBOutlet weak var addPreExistingMembers: UITextField!
    @IBOutlet weak var createProjectButton: UIButton!
    @IBOutlet weak var languageTextField: UITextField!
    @IBOutlet weak var projectTitleTextField: UITextField!
    @IBOutlet weak var descriptionTextView: UITextView!
    let placeHoldertext = "Your Project Description...(Optional)"
    
    @IBOutlet weak var projectTypePicker: UITextField!
    @IBOutlet weak var numberPicker: UIPickerView!
    let pickerData = ["1","2","3","4",">5"]
    var projectPickOption = ["Mobile App", "Desktop App", "Web App", "API", "Framework","WebSite","Research Paper","Other"]
    
    override func viewDidLoad() {
        super.viewDidLoad()
        descriptionTextView?.delegate = self
        applyPlaceholderStyle(descriptionTextView!, placeholderText: placeHoldertext)
        
        numberPicker.dataSource = self
        numberPicker.delegate = self
        
        numberPicker.selectRow(2, inComponent: 0, animated: true)
        
        decorateButton(createProjectButton, color: UIColor(red: 0.259, green: 0.647, blue: 0.961, alpha: 1))
        
        
        var projectPickerView = UIPickerView()
        
        projectPickerView.delegate = self
        
        projectTypePicker.inputView = projectPickerView
    }
    
    
    @IBAction func tapCreateButton(sender: AnyObject) {
        
        if(projectTitleTextField.text=="" || languageTextField=="" || projectTypePicker.text==""){
            
            let blankFieldAlert = UIAlertController(title: "Empty Form Field Error", message: "Please enter all required data.", preferredStyle: UIAlertControllerStyle.Alert)
            blankFieldAlert.addAction(UIAlertAction(title: "OK", style: .Default, handler: nil))
            self.presentViewController(blankFieldAlert, animated: true, completion: nil);
        } else {
            
            let bounds = UIScreen.mainScreen().bounds
            let myActivityIndicatorView: DTIActivityIndicatorView = DTIActivityIndicatorView(frame: CGRect(x:bounds.size.width/2-60, y:bounds.size.height/2-28, width:120.0, height:120.0))
            self.view.addSubview(myActivityIndicatorView)
            myActivityIndicatorView.indicatorColor = UIColor(red: 0.259, green: 0.647, blue: 0.961, alpha: 1)
            
            myActivityIndicatorView.indicatorStyle = DTIIndicatorStyle.convInv(.wp8)
            myActivityIndicatorView.startActivity()
            
            UIApplication.sharedApplication().beginIgnoringInteractionEvents()
        
            let activitySeconds = 9.0
            let activityDelay = activitySeconds * Double(NSEC_PER_SEC)  // nanoseconds per seconds
            let activityDispatchTime = dispatch_time(DISPATCH_TIME_NOW, Int64(activityDelay))
            // present this after //seconds//
            dispatch_after(activityDispatchTime, dispatch_get_main_queue(), {
                myActivityIndicatorView.stopActivity()
                UIApplication.sharedApplication().endIgnoringInteractionEvents()
                
                var newProject = PFObject(className: "SharedProjects")
                newProject["projectTitle"] = self.projectTitleTextField.text
                newProject["projectDescription"] = self.descriptionTextView.text
                newProject["projectType"] = self.projectTypePicker.text
                newProject["projectLanguages"] = self.languageTextField.text
                newProject["numberOfProjectMembersRequired"] = self.numberPicker.selectedRowInComponent(0)+1
                //print(numberPicker.selectedRowInComponent(0)+1)
                newProject["projectPreExistingMembers"] = self.addPreExistingMembers.text
                newProject["userId"] = PFUser.currentUser()?.objectId
                newProject.saveInBackgroundWithBlock {
                    (success: Bool, error: NSError?) -> Void in
                    if (success) {
                        self.performSegueWithIdentifier("showProjectsTableOnSuccess", sender: nil)
                        print("done saving the project on parse...")
                    } else {
                        print("error saving the project on parse...")
                    }
                }
            })
            
        }
        
    }
    
    
    func numberOfComponentsInPickerView(pickerView: UIPickerView) -> Int {
        return 1
    }
    
    func pickerView(pickerView: UIPickerView, numberOfRowsInComponent component: Int) -> Int {
        if pickerView == numberPicker {
             return pickerData.count;
        } else { // if it's the second picker view
            return projectPickOption.count
        }
    }
    
    func pickerView(pickerView: UIPickerView, titleForRow row: Int, forComponent component: Int) -> String! {
        
        if pickerView == numberPicker {
           return pickerData[row]
        } else { // if it's the second picker view
            return projectPickOption[row]
        }
    }
    
    func pickerView(pickerView: UIPickerView, didSelectRow row: Int, inComponent component: Int) {
        
        if pickerView != numberPicker {
            projectTypePicker.text = projectPickOption[row]
        }
    }
    
    
    
    func applyPlaceholderStyle(aTextview: UITextView, placeholderText: String)
    {
        // make it look (initially) like a placeholder
        aTextview.textColor = UIColor.lightGrayColor()
        aTextview.text = placeholderText
    }
    
    func applyNonPlaceholderStyle(aTextview: UITextView)
    {
        // make it look like normal text instead of a placeholder
        aTextview.textColor = UIColor.darkTextColor()
        aTextview.alpha = 1.0
    }
   
    func textViewShouldBeginEditing(aTextView: UITextView) -> Bool
    {
        if aTextView == descriptionTextView && aTextView.text == placeHoldertext
        {
            // move cursor to start
            moveCursorToStart(aTextView)
        }
        return true
    }
    
    func moveCursorToStart(aTextView: UITextView)
    {
        dispatch_async(dispatch_get_main_queue(), {
            aTextView.selectedRange = NSMakeRange(0, 0);
        })
    }
    
   
    func textView(textView: UITextView, shouldChangeTextInRange range: NSRange, replacementText text: String) -> Bool {
        // remove the placeholder text when they start typing
        // first, see if the field is empty
        // if it's not empty, then the text should be black and not italic
        // BUT, we also need to remove the placeholder text if that's the only text
        // if it is empty, then the text should be the placeholder
        let newLength = textView.text.utf16.count + text.utf16.count - range.length
        if newLength > 0 // have text, so don't show the placeholder
        {
            // check if the only text is the placeholder and remove it if needed
            // unless they've hit the delete button with the placeholder displayed
            if textView == textView && textView.text == placeHoldertext
            {
                if text.utf16.count == 0 // they hit the back button
                {
                    return false // ignore it
                }
                applyNonPlaceholderStyle(textView)
                textView.text = ""
            }
            return true
        }
        else  // no text, so show the placeholder
        {
            applyPlaceholderStyle(textView, placeholderText: placeHoldertext)
            moveCursorToStart(textView)
            return false
        }
    }

    private func decorateButton(button: UIButton, color: UIColor) {
        
        button.setTitleColor(UIColor.whiteColor(), forState: UIControlState.Normal)
        button.layer.borderColor = color.CGColor
        button.backgroundColor = color
        button.layer.borderWidth = 2
        button.layer.cornerRadius = 7
    }

}
