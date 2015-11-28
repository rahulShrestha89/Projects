//
//  ProjectsViewController.swift
//  ParseStarterProject-Swift
//
//  Created by Rahul Shrestha on 11/17/15.
//  Copyright © 2015 Parse. All rights reserved.
//

import UIKit
import Parse

class ProjectsViewController: UIViewController,UITableViewDataSource,UITableViewDelegate
{

    
    var projectTitles = [""]
    
    @IBOutlet weak var tableView: UITableView!
    override func viewDidLoad() {
        super.viewDidLoad()
       
        self.tableView.dataSource=self
        self.tableView.delegate=self
        
        let projectsQuery = PFQuery(className:"SharedProjects")
        if let user = PFUser.currentUser() {
            projectsQuery.whereKey("userId", equalTo: user.objectId!)
        }
        
        projectsQuery.findObjectsInBackgroundWithBlock {
            (objects: [PFObject]?, error: NSError?) -> Void in
            
            if error == nil {
                // The find succeeded.
                //print("Successfully retrieved \(objects!.count) projects.")
                
                // remove the unnecessary empty strings
                self.projectTitles.removeAll(keepCapacity: true)
                
                for object in objects! {
                    
                    if let allProjects = object as? PFObject! {
           
                        self.projectTitles.append(allProjects["projectTitle"] as! String)
                    }
                }
                 print(self.projectTitles)
          
            } else {
                // Log details of the failure
                print("Error: \(error!) \(error!.userInfo)")
            }
        }
 
    }

    func tableView(tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return projectTitles.count
    }
    
    func tableView(tableView: UITableView, cellForRowAtIndexPath indexPath: NSIndexPath) -> UITableViewCell {
        
        var cell = UITableViewCell()
        cell.textLabel?.text = projectTitles[indexPath.row]
        return cell
    }
    
    func tableView(tableView: UITableView, didSelectRowAtIndexPath indexPath: NSIndexPath) {
        
    }
    
    
    @IBAction func onDoneButtonTapped(sender: AnyObject) {
        self.performSegueWithIdentifier("backToTabbedViewFromProjects", sender: nil)
    }
    
   
   
}
