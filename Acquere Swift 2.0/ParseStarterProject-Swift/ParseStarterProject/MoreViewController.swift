//
//  MoreViewController.swift
//  ParseStarterProject-Swift
//
//  Created by Rahul Shrestha on 11/5/15.
//  Copyright © 2015 Parse. All rights reserved.
//

import UIKit
import Parse

class MoreViewController: UIViewController,UITableViewDataSource,UITableViewDelegate {

    @IBOutlet weak var tableView: UITableView!

    var currentUser = PFUser.currentUser()
    
    // provides the number of section header titles
    let headerTitlesForSection = ["User", " "," "," "]
    let rowData = [[" "], ["Projects","Expertise"],["Privacy Settings", "Info Settings", "Notification Settings"] , ["Log Out"]]
    
    override func viewDidLoad() {
        super.viewDidLoad()

        self.tableView.dataSource=self
        self.tableView.delegate=self
    }


    
    
    
    // provide the number of section to the table
    func numberOfSectionsInTableView(tableView: UITableView) -> Int {
        return headerTitlesForSection.count
    }

    // provide the number of rows in each section
    func tableView(tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        
        return rowData[section].count
    }
    
    func tableView(tableView: UITableView, titleForHeaderInSection section: Int) -> String? {
        if section < headerTitlesForSection.count {
            return headerTitlesForSection[section]
        }
        
        return nil
    }
    
    // provide data for each section
    //checks to see there's a title for that section and returns it, otherwise nil is returned. There won't be a title if the number of titles in headerTitlesForSection is smaller than the number of arrays in rowData
    func tableView(tableView: UITableView, cellForRowAtIndexPath indexPath: NSIndexPath) -> UITableViewCell {
        
        var cell = UITableViewCell()
        
        if(indexPath.section==0 && indexPath.row==0){
            cell.textLabel?.text = (currentUser!["firstName"] as? String)! + " " +
                (currentUser!["lastName"] as? String)!
        } else {
            cell.textLabel?.text = rowData[indexPath.section][indexPath.row]
        }
        
        cell.textLabel?.font = UIFont (name: "Avenir Book", size: 18)
        
        return cell
    }
    
    func tableView(tableView: UITableView, didSelectRowAtIndexPath indexPath: NSIndexPath) {
        
        if(indexPath.section==0 && indexPath.row==0){
            
           self.performSegueWithIdentifier("showUserDetails", sender: self)
            
        } else if(indexPath.section==3 && indexPath.row==0){
            
            PFUser.logOut()
            print(PFUser.currentUser())
            self.performSegueWithIdentifier("afterLogout", sender:nil )
            
        } else if (indexPath.section==1 && indexPath.row==0){
            
            self.performSegueWithIdentifier("showProjects", sender: self)
            
        } else {
        
        }
        
    }

}
