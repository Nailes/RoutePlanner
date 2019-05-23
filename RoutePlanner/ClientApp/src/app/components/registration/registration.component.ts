import { Component, OnInit } from '@angular/core';

export class User{
  name: string;
  email: string;
  password: string;
}

let users = [];

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }
  
  user: User = new User();

  addUser(){
    users.push(this.user);
    console.log(users);
  }

}

console.log(users[0]);