//
//  PostSignUpViewController.swift
//  ParseStarterProject-Swift
//
//  Created by Rahul Shrestha on 10/8/15.
//  Copyright © 2015 Parse. All rights reserved.
//

import UIKit

class PostSignUpViewController: UIViewController {


    @IBOutlet weak var discoverySwitch: UISwitch!
    @IBOutlet weak var followersSwitch: UISwitch!
    @IBOutlet weak var rookieSwitch: UISwitch!
    @IBOutlet weak var projectExperienceSwitch: UISwitch!
    @IBOutlet weak var goToNextViewController: UIButton!
    override func viewDidLoad() {
        super.viewDidLoad()

    }

    @IBAction func doNext(sender: AnyObject) {
        
        self.performSegueWithIdentifier("toTabbedPage", sender: nil)
    }
    


}
