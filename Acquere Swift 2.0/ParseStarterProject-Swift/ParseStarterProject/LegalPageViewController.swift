//
//  LegalPageViewController.swift
//  ParseStarterProject-Swift
//
//  Created by Rahul Shrestha on 9/19/15.
//  Copyright (c) 2015 Parse. All rights reserved.
//

import UIKit

class LegalPageViewController: UIViewController {

  
    @IBOutlet weak var firstView: UIView!
    
    
    @IBOutlet weak var secondView: UIView!
    
    @IBOutlet weak var thirdView: UIView!
    @IBOutlet weak var segmentedControl: UISegmentedControl!
    override func viewDidLoad() {
        super.viewDidLoad()
        
        firstView.hidden = false
        secondView.hidden = true
        thirdView.hidden = true

    
    }
    
    @IBAction func segmentChanged(sender: UISegmentedControl) {
        switch segmentedControl.selectedSegmentIndex
        {
        case 0:
            firstView.hidden = false
            secondView.hidden = true
            thirdView.hidden = true
        case 1:
            firstView.hidden = true
            secondView.hidden = false
            thirdView.hidden = true
        case 2:
            firstView.hidden = true
            secondView.hidden = true
            thirdView.hidden = false
        default:
            break;
        }
        
      
    }

    

   

}
