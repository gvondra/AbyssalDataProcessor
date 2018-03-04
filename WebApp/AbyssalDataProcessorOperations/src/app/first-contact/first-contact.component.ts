import { Component, OnInit } from '@angular/core';
import { FirstContactService } from '../first-contact.service';
@Component({
  selector: 'app-first-contact',
  templateUrl: './first-contact.component.html',
  styleUrls: ['./first-contact.component.css'],
  providers: [FirstContactService]
})
export class FirstContactComponent implements OnInit {

  Code: string = null;

  constructor(private firstContactService: FirstContactService) { }

  ngOnInit() {
  }

  Submit() {
    if (this.Code && this.Code != "") {
      this.firstContactService.firstContact(this.Code)
      //.then()
      //.catch(ex => console.error(ex.message))
    }
    
  }
}
